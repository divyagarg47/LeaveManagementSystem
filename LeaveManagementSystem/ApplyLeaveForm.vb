Imports MySql.Data.MySqlClient

Public Class ApplyLeaveForm

    Private connectionString As String = "server=172.16.114.188;uid=santhosh;database=leavemanagement;"
    Private connection As New MySqlConnection(connectionString)

    'UI buttons
    Private WithEvents btnLeaveHistory As New Button()
    Private WithEvents btnNewLeave As New Button()
    Private WithEvents btnApproveLeave As New Button()


    Private Sub ApplyLeaveForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    End Sub

    'Button connections
    Private Sub btnLeaveHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLeaveHistory.Click

        Me.Hide()
        Dim leaveHistoryForm As New LeaveHistoryForm()
        leaveHistoryForm.Show()
    End Sub
    Private Sub btnNewLeave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewLeave.Click
        'work as home page
        Me.Hide()
        Dim homePageForm As New HomePageForm()
        homePageForm.Show()
    End Sub
    Private Sub btnApproveLeave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApproveLeave.Click
        Me.Hide()
        Dim approveNewForm As New ApproveNewLeave()
        approveNewForm.Show()
    End Sub
    ' Declare the button with WithEvents keyword
    Private WithEvents logoutButton As New Button()

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Set up form properties
        Me.Text = "Apply Leave"
        Me.Size = New Size(1000, 1000)

        ApplyLeave_UI()
        LeaveTypeComboBox.Items.Add("Medical")
        LeaveTypeComboBox.Items.Add("Vacation")
        LeaveTypeComboBox.Items.Add("Casual")
        ' Set background color programmatically
        Me.BackColor = Color.AliceBlue ' Set your desired background color here
    End Sub

    Private Sub ApplyLeave_UI()
        Me.WindowState = FormWindowState.Maximized
        ' Add the menu panel
        Dim menuPanel As New Panel()
        menuPanel.BackColor = Color.Black
        menuPanel.Size = New Size(150, Me.Size.Height) ' Adjust height to fit below the logo
        menuPanel.Location = New Point(0, 150) ' Position below the logo
        Me.Controls.Add(menuPanel)

        ' Add buttons inside the menu panel for navigation
        btnLeaveHistory.Text = "Leave History"
        btnLeaveHistory.Size = New Size(120, 30)
        btnLeaveHistory.Location = New Point(15, 50)
        btnLeaveHistory.ForeColor = Color.White ' Set font color
        menuPanel.Controls.Add(btnLeaveHistory)

        btnNewLeave.Text = "Home Page"
        btnNewLeave.Size = New Size(120, 30)
        btnNewLeave.Location = New Point(15, 100)
        btnNewLeave.ForeColor = Color.White ' Set font color
        menuPanel.Controls.Add(btnNewLeave)


        ' Check if the user's designation is not student, faculty, or staff
        If GlobalVariables.Approver = "1" Then
            ' Add "Approve Leave" button
            btnApproveLeave.Visible = True
            btnApproveLeave.Enabled = True
            btnApproveLeave.Text = "Approve Leave"
            btnApproveLeave.Size = New Size(120, 30)
            btnApproveLeave.Location = New Point(15, 200)
            btnApproveLeave.ForeColor = Color.White ' Set font color
            menuPanel.Controls.Add(btnApproveLeave)
        Else
            btnApproveLeave.Visible = False
            btnApproveLeave.Enabled = False
        End If



        ' Add a header label
        Dim headerLabel As New Label()
        headerLabel.Text = "IITG Leave Management System"
        headerLabel.AutoSize = True
        headerLabel.Font = New Font("Arial", 24, FontStyle.Bold) ' Increase font size
        headerLabel.Location = New Point((Me.ClientSize.Width - 100 - headerLabel.Width) \ 2 - 50, 20)
        Me.Controls.Add(headerLabel)

        PanelInput.Location = New Point(menuPanel.Width + (Me.Width - menuPanel.Size.Width - PanelInput.Size.Width) / 2, (Me.Height - 150 - PanelInput.Size.Height) / 2)



        ' Add a PictureBox for the IITG logo
        Dim iitgLogoPictureBox As New PictureBox()
        Try
            ' Load the image file from the same directory as the application executable
            iitgLogoPictureBox.Image = LeaveManagementSystem.My.Resources.IITG_logo

            ' Set PictureBox properties
            iitgLogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage
            iitgLogoPictureBox.Size = New Size(100, 100) ' Set the size of the logo PictureBox
            iitgLogoPictureBox.Location = New Point((menuPanel.Size.Width - iitgLogoPictureBox.Size.Width) / 2, 20) ' Position the logo at the top left corner
            Me.Controls.Add(iitgLogoPictureBox)
            iitgLogoPictureBox.BringToFront()
        Catch ex As Exception
            ' Handle file loading errors
            MessageBox.Show("An error occurred while loading the image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Add a logout button
        logoutButton.Text = "Logout"
        logoutButton.AutoSize = True
        logoutButton.Font = New Font("Arial", 12, FontStyle.Regular)
        ' Set the location of the logout button at the top right of the form
        logoutButton.Location = New Point(Me.ClientSize.Width - logoutButton.Width - 40, 20)
        Me.Controls.Add(logoutButton) ' Add the button to the form

        ' Attach the event handler using AddHandler
        AddHandler logoutButton.Click, AddressOf LogoutButton_Click


    End Sub

    ' Event handler method for the logout button click event
    Public Sub LogoutButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Add logout functionality here
        GlobalVariables.Email = ""
        GlobalVariables.Designation = ""

        Me.Hide()

        LoginForm.Show()
        MessageBox.Show("Logged out successfully!", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub SubmitButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SubmitButton.Click
        ' Perform validation before submitting
        Dim start As Date = StartDate.Value
        Dim endd As Date = EndDate.Value
        If ValidateInput(start, endd) Then
            ' Calculate number of leaves excluding holidays
            Dim numberOfLeaves As Integer = CalculateNumberOfLeaves(start, endd)

            ' If no leaves are left after excluding holidays, do not submit the leave application
            If numberOfLeaves = 0 Then
                MessageBox.Show("All days between the start and end dates are holidays.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub ' Exit the subprocedure as no leave needs to be submitted
            End If

            ' Submit leave application
            InsertLeaveApplication(start, endd, numberOfLeaves)
            ShowConfirmationModal(start, endd)
        End If
    End Sub

    Private Function ValidateInput(ByVal start As Date, ByVal endd As Date) As Boolean
        ' Perform validation checks
        If start <= Date.Today Then
            MessageBox.Show("Start date must be after today's date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If start >= endd Then
            MessageBox.Show("End date must be after start date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If LeaveTypeComboBox.SelectedIndex = -1 Then
            MessageBox.Show("Please select a leave type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        ' Additional validation checks can be added for other fields

        Return True
    End Function

    Private Sub ShowConfirmationModal(ByVal start As Date, ByVal endd As Date)
        ' Construct confirmation message
        ' Construct confirmation message
        Dim confirmationMessage As String = "Leave Application Summary:" & vbCrLf &
                                            "Start Date: " & start.ToString("MM/dd/yyyy") & vbCrLf &
                                            "End Date: " & endd.ToString("MM/dd/yyyy") & vbCrLf &
                                            "Leave Type: " & LeaveTypeComboBox.SelectedItem.ToString() & vbCrLf &
                                            "Number of Days: " & CalculateNumberOfLeaves(start, endd) & vbCrLf &
                                            "Reason: " & ReasonTextBox.Text

        ' Display confirmation modal
        MessageBox.Show(confirmationMessage, "Leave Application Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


    Private Sub InsertLeaveApplication(ByVal start As Date, ByVal endd As Date, ByVal numberOfLeaves As Integer)
        Dim leaveId As Integer = GetNextLeaveId()
        Dim iitgId As String = GlobalVariables.Email
        Dim appliedOn As Date = Date.Today
        Dim typeOfLeave As String = LeaveTypeComboBox.SelectedItem.ToString()
        Dim statusOfLeave As String = "Pending" ' Assuming the status starts as pending
        Dim reason As String = ReasonTextBox.Text
        Dim approverId As String = GetApproverId(iitgId)

        ' Construct SQL query
        Dim query As String = "INSERT INTO leave_table (Leave_ID, IITG_ID, Start_Date, End_Date, Applied_On, Number_Of_Leaves, Type_Of_Leave, Status_Of_Leave, Reason, Approver_ID) " &
                              "VALUES (@leaveId, @iitgId, @startDate, @endDate, @appliedOn, @numberOfLeaves, @typeOfLeave, @statusOfLeave, @reason, @approverId)"

        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)

            ' Add parameters
            command.Parameters.AddWithValue("@leaveId", leaveId)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            command.Parameters.AddWithValue("@startDate", start)
            command.Parameters.AddWithValue("@endDate", endd)
            command.Parameters.AddWithValue("@appliedOn", appliedOn)
            command.Parameters.AddWithValue("@numberOfLeaves", numberOfLeaves)
            command.Parameters.AddWithValue("@typeOfLeave", typeOfLeave)

            ' Check if the approver is the director and if the director is on leave
            If approverId = "director" Then
                Dim directorLeaveQuery As String = "SELECT On_Leave FROM director WHERE IITG_ID = 'director'"
                Dim directorLeaveCommand As New MySqlCommand(directorLeaveQuery, connection)
                'directorLeaveCommand.Parameters.AddWithValue("@directorId", approverId)
                Dim directorLeaveResult = directorLeaveCommand.ExecuteScalar()
                'MessageBox.Show(directorLeaveResult)
                If directorLeaveResult IsNot Nothing AndAlso directorLeaveResult.ToString = "True" Then
                    ' Director is on leave, accept the leave application
                    statusOfLeave = "Accepted"
                    MessageBox.Show("Everyone up the hierarchy is on leave, so leave is accepted.")
                End If
            End If

            command.Parameters.AddWithValue("@statusOfLeave", statusOfLeave)
            command.Parameters.AddWithValue("@reason", reason)
            command.Parameters.AddWithValue("@approverId", approverId)

            command.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error inserting leave application: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Function CalculateNumberOfLeaves(ByVal start As Date, ByVal endd As Date) As Integer
        Dim numberOfLeaves As Integer = (endd - start).Days + 1

        ' Query the holidays table to count the number of holidays falling within the specified range
        Dim query As String = "SELECT COUNT(*) FROM holidays WHERE date >= @startDate AND date <= @endDate"
        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@startDate", start.Date)
            command.Parameters.AddWithValue("@endDate", endd.Date)
            Dim result = command.ExecuteScalar()
            If Not IsDBNull(result) Then
                Dim holidaysCount As Integer = Convert.ToInt32(result)
                numberOfLeaves -= holidaysCount
            End If
        Catch ex As Exception
            MessageBox.Show("Error counting holidays: " & ex.Message)
        Finally
            connection.Close()
        End Try

        Return numberOfLeaves
    End Function

    Private Function GetApproverId(ByVal iitgId As String) As String
        Dim designation As String = GetDesignation(iitgId)

        Select Case designation
            Case "student"
                Return GetStudentApproverId(iitgId)
            Case "faculty"
                Return GetFacultyApproverId(iitgId)
            Case "hod"
                Return GetHODApproverId(iitgId)
            Case "dean"
                Return GetDeanApproverId(iitgId)
            Case "director"
                Return GetDirectorApproverId(iitgId)
            Case "staff"
                Return GetStaffApproverId(iitgId)
            Case "supervisor"
                Return GetSupervisorApproverId(iitgId)
            Case "head_supervisor"
                Return GetHeadSupervisorApproverId(iitgId)
            Case Else
                Return "" ' Default value if designation is not recognized
        End Select
    End Function

    Private Function GetDesignation(ByVal iitgId As String) As String
        Dim designation As String = ""

        ' Query the lookup_table to get the designation based on IITG_ID
        Dim query As String = "SELECT Designation FROM lookup_table WHERE IITG_ID = @iitgId"
        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            Dim result = command.ExecuteScalar()
            If Not IsDBNull(result) Then
                designation = result.ToString()
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving designation: " & ex.Message)
        Finally
            connection.Close()
        End Try

        Return designation
    End Function

    Private Function GetStudentApproverId(ByVal iitgId As String) As String
        Dim approverId As String = ""
        Dim val As Integer = 0
        ' Query the faculty table to get the Approver_ID based on IITG_ID
        Dim query As String = "SELECT Approver_ID FROM student WHERE IITG_ID = @iitgId"

        ' Execute query and retrieve Approver_ID
        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            Dim result = command.ExecuteScalar()

            If Not IsDBNull(result) Then
                approverId = result.ToString()
                'MessageBox.Show(approverId)
                ' Check if the approver is on leave
                Dim leaveQuery As String = "SELECT On_Leave FROM hod WHERE IITG_ID = @approverId"
                Dim leaveCommand As New MySqlCommand(leaveQuery, connection)
                leaveCommand.Parameters.AddWithValue("@approverId", approverId)
                Dim leaveResult = leaveCommand.ExecuteScalar()
                'MessageBox.Show(leaveResult)
                If leaveResult IsNot Nothing AndAlso leaveResult.ToString() = "True" Then
                    ' Update the approverId to 'doaa'
                    val = 1
                    'approverId = GetHODApproverId(approverId)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving approverId: " & ex.Message)
        Finally
            connection.Close()
        End Try
        If val = 1 Then
            approverId = GetHODApproverId(approverId)
        End If
        Return approverId
    End Function

    Private Function GetFacultyApproverId(ByVal iitgId As String) As String
        Dim approverId As String = ""
        Dim val As Integer = 0
        ' Query the faculty table to get the Approver_ID based on IITG_ID
        Dim query As String = "SELECT Approver_ID FROM faculty WHERE IITG_ID = @iitgId"

        ' Execute query and retrieve Approver_ID
        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            Dim result = command.ExecuteScalar()

            If Not IsDBNull(result) Then
                approverId = result.ToString()
                'MessageBox.Show(approverId)
                ' Check if the approver is on leave
                Dim leaveQuery As String = "SELECT On_Leave FROM hod WHERE IITG_ID = @approverId"
                Dim leaveCommand As New MySqlCommand(leaveQuery, connection)
                leaveCommand.Parameters.AddWithValue("@approverId", approverId)
                Dim leaveResult = leaveCommand.ExecuteScalar()
                'MessageBox.Show(leaveResult)
                If leaveResult IsNot Nothing AndAlso leaveResult.ToString() = "True" Then
                    ' Update the approverId to 'doaa'
                    val = 1
                    'approverId = GetHODApproverId(approverId)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving approverId: " & ex.Message)
        Finally
            connection.Close()
        End Try
        If val = 1 Then
            approverId = GetHODApproverId(approverId)
        End If
        Return approverId
    End Function

    Private Function GetHODApproverId(ByVal iitgId As String) As String
        Dim approverId As String = ""
        Dim val As Integer = 0
        ' Query the faculty table to get the Approver_ID based on IITG_ID
        Dim query As String = "SELECT Approver_ID FROM hod WHERE IITG_ID = @iitgId"

        ' Execute query and retrieve Approver_ID
        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            Dim result = command.ExecuteScalar()

            If Not IsDBNull(result) Then
                approverId = result.ToString()
                'MessageBox.Show(approverId)
                ' Check if the approver is on leave
                Dim leaveQuery As String = "SELECT On_Leave FROM dean WHERE IITG_ID = @approverId"
                Dim leaveCommand As New MySqlCommand(leaveQuery, connection)
                leaveCommand.Parameters.AddWithValue("@approverId", approverId)
                Dim leaveResult = leaveCommand.ExecuteScalar()
                'MessageBox.Show(leaveResult)
                If leaveResult IsNot Nothing AndAlso leaveResult.ToString() = "True" Then
                    ' Update the approverId to 'doaa'
                    val = 1
                    'approverId = GetDeanApproverId(approverId)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving approverId: " & ex.Message)
        Finally
            connection.Close()
        End Try
        If val = 1 Then
            approverId = GetDeanApproverId(approverId)
        End If
        Return approverId
    End Function

    Private Function GetDeanApproverId(ByVal iitgId As String) As String
        Dim approverId As String = ""
        Dim val As Integer = 0
        ' Query the faculty table to get the Approver_ID based on IITG_ID
        Dim query As String = "SELECT Approver_ID FROM dean WHERE IITG_ID = @iitgId"

        ' Execute query and retrieve Approver_ID
        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            Dim result = command.ExecuteScalar()

            If Not IsDBNull(result) Then
                approverId = result.ToString()
                'MessageBox.Show(approverId)
                ' Check if the approver is on leave
                Dim leaveQuery As String = "SELECT On_Leave FROM director WHERE IITG_ID = @approverId"
                Dim leaveCommand As New MySqlCommand(leaveQuery, connection)
                leaveCommand.Parameters.AddWithValue("@approverId", approverId)
                Dim leaveResult = leaveCommand.ExecuteScalar()
                'MessageBox.Show(leaveResult)
                If leaveResult IsNot Nothing AndAlso leaveResult.ToString() = "True" Then
                    ' Update the approverId to 'doaa'
                    val = 1
                    'approverId = GetDirectorApproverId(approverId)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving approverId: " & ex.Message)
        Finally
            connection.Close()
        End Try
        If val = 1 Then
            approverId = GetDirectorApproverId(approverId)
        End If
        Return approverId
    End Function

    Private Function GetDirectorApproverId(ByVal iitgId As String) As String
        Dim approverId As String = ""
        'Dim val As Integer = 0
        ' Query the staff table to get the Approver_ID based on IITG_ID
        Dim query As String = "SELECT Approver_ID FROM director WHERE IITG_ID = @iitgId"
        ' Execute query and retrieve Approver_ID
        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            Dim result = command.ExecuteScalar()
            If Not IsDBNull(result) Then
                approverId = result.ToString()
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving approverId: " & ex.Message)
        Finally
            connection.Close()
        End Try
        Return approverId
    End Function

    Private Function GetStaffApproverId(ByVal iitgId As String) As String
        Dim approverId As String = ""
        Dim val As Integer = 0
        ' Query the faculty table to get the Approver_ID based on IITG_ID
        Dim query As String = "SELECT Approver_ID FROM staff WHERE IITG_ID = @iitgId"

        ' Execute query and retrieve Approver_ID
        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            Dim result = command.ExecuteScalar()

            If Not IsDBNull(result) Then
                approverId = result.ToString()
                'MessageBox.Show(approverId)
                ' Check if the approver is on leave
                Dim leaveQuery As String = "SELECT On_Leave FROM supervisor WHERE IITG_ID = @approverId"
                Dim leaveCommand As New MySqlCommand(leaveQuery, connection)
                leaveCommand.Parameters.AddWithValue("@approverId", approverId)
                Dim leaveResult = leaveCommand.ExecuteScalar()
                'MessageBox.Show(leaveResult)
                If leaveResult IsNot Nothing AndAlso leaveResult.ToString() = "True" Then
                    ' Update the approverId to 'doaa'
                    val = 1
                    'approverId = GetSupervisorApproverId(approverId)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving approverId: " & ex.Message)
        Finally
            connection.Close()
        End Try
        If val = 1 Then
            approverId = GetSupervisorApproverId(approverId)
        End If
        Return approverId
    End Function



    Private Function GetSupervisorApproverId(ByVal iitgId As String) As String
        Dim approverId As String = ""
        Dim val As Integer = 0
        ' Query the faculty table to get the Approver_ID based on IITG_ID
        Dim query As String = "SELECT Approver_ID FROM supervisor WHERE IITG_ID = @iitgId"

        ' Execute query and retrieve Approver_ID
        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            Dim result = command.ExecuteScalar()

            If Not IsDBNull(result) Then
                approverId = result.ToString()
                'MessageBox.Show(approverId)
                ' Check if the approver is on leave
                Dim leaveQuery As String = "SELECT On_Leave FROM head_supervisor WHERE IITG_ID = @approverId"
                Dim leaveCommand As New MySqlCommand(leaveQuery, connection)
                leaveCommand.Parameters.AddWithValue("@approverId", approverId)
                Dim leaveResult = leaveCommand.ExecuteScalar()
                'MessageBox.Show(leaveResult)
                If leaveResult IsNot Nothing AndAlso leaveResult.ToString() = "True" Then
                    ' Update the approverId to 'doaa'
                    val = 1
                    'approverId = GetHeadSupervisorApproverId(approverId)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving approverId: " & ex.Message)
        Finally
            connection.Close()
        End Try
        If val = 1 Then
            approverId = GetHeadSupervisorApproverId(approverId)
        End If
        Return approverId
    End Function


    Private Function GetHeadSupervisorApproverId(ByVal iitgId As String) As String
        Dim approverId As String = ""
        Dim val As Integer = 0
        ' Query the faculty table to get the Approver_ID based on IITG_ID
        Dim query As String = "SELECT Approver_ID FROM head_supervisor WHERE IITG_ID = @iitgId"

        ' Execute query and retrieve Approver_ID
        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            Dim result = command.ExecuteScalar()

            If Not IsDBNull(result) Then
                approverId = result.ToString()
                'MessageBox.Show(approverId)
                ' Check if the approver is on leave
                Dim leaveQuery As String = "SELECT On_Leave FROM director WHERE IITG_ID = @approverId"
                Dim leaveCommand As New MySqlCommand(leaveQuery, connection)
                leaveCommand.Parameters.AddWithValue("@approverId", approverId)
                Dim leaveResult = leaveCommand.ExecuteScalar()
                'MessageBox.Show(leaveResult)
                If leaveResult IsNot Nothing AndAlso leaveResult.ToString() = "True" Then
                    ' Update the approverId to 'doaa'
                    val = 1
                    'approverId = GetDirectorApproverId(approverId)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving approverId: " & ex.Message)
        Finally
            connection.Close()
        End Try
        If val = 1 Then
            approverId = GetDirectorApproverId(approverId)
        End If
        Return approverId
    End Function

    Private Function GetNextLeaveId() As Integer
        Dim nextLeaveId As Integer = 1

        Dim query As String = "SELECT MAX(Leave_ID) FROM leave_table WHERE IITG_ID = @iitgId"
        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@iitgId", GlobalVariables.Email)
            Dim result = command.ExecuteScalar()
            If Not IsDBNull(result) Then
                nextLeaveId = Convert.ToInt32(result) + 1
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving leave ID: " & ex.Message)
        Finally
            connection.Close()
        End Try

        Return nextLeaveId
    End Function


    Private Function GetApproverName(ByVal iitgId As String) As String
        Dim approverName As String = ""

        ' Query the users table to get the name of the approver based on IITG_ID
        Dim query As String = "SELECT Name FROM users WHERE IITG_ID = @iitgId"
        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            Dim result = command.ExecuteScalar()
            If Not IsDBNull(result) Then
                approverName = result.ToString()
            End If
        Catch ex As Exception
            MessageBox.Show("Error retrieving approver name: " & ex.Message)
        Finally
            connection.Close()
        End Try

        Return approverName
    End Function

    Private Sub LeaveTypeComboBox_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles LeaveTypeComboBox.SelectedIndexChanged

    End Sub

    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles PanelInput.Enter

    End Sub


End Class