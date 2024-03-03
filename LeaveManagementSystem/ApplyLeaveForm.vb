Imports MySql.Data.MySqlClient

Public Class ApplyLeaveForm

    Private connectionString As String = "server=172.16.114.188;uid=santhosh;database=leavemanagement;"
    Private connection As New MySqlConnection(connectionString)
    Private WithEvents logoutButton As New Button()
    'buttons
    Private WithEvents btnLeaveHistory As New Button()
    Private WithEvents btnNewLeave As New Button()
    Private WithEvents btnApproveLeave As New Button()

    Private Sub ApplyLeaveForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ApplyLeave_UI()
    End Sub
    Private Sub ApplyLeave_UI()
        ' Set background color programmatically
        Me.BackColor = Color.AliceBlue ' Set your desired background color here


        Me.WindowState = FormWindowState.Maximized
        ' Add the menu panel
        Dim menuPanel As New Panel()
        menuPanel.BackColor = Color.Black
        menuPanel.Size = New Size(150, 400) ' Adjust height to fit below the logo
        menuPanel.Location = New Point(0, 90) ' Position below the logo
        Me.Controls.Add(menuPanel)

        ' Add buttons inside the menu panel for navigation
        btnLeaveHistory.Text = "Leave History"
        btnLeaveHistory.Size = New Size(120, 30)
        btnLeaveHistory.Location = New Point(15, 50)
        btnLeaveHistory.ForeColor = Color.White ' Set font color
        menuPanel.Controls.Add(btnLeaveHistory)

        btnNewLeave.Text = "Apply New Leave"
        btnNewLeave.Size = New Size(120, 30)
        btnNewLeave.Location = New Point(15, 100)
        btnNewLeave.ForeColor = Color.White ' Set font color
        menuPanel.Controls.Add(btnNewLeave)


        ' Check if the user's designation is not student, faculty, or staff
        If Not (HomePageForm.designationLabel.Text = "Designation: student" Or HomePageForm.designationLabel.Text = "Designation: faculty" Or HomePageForm.designationLabel.Text = "Designation: staff" Or HomePageForm.designationLabel.Text = "Designation: admin") Then
            ' Add "Approve Leave" button
            btnApproveLeave.Text = "Approve Leave"
            btnApproveLeave.Size = New Size(120, 30)
            btnApproveLeave.Location = New Point(15, 200)
            btnApproveLeave.ForeColor = Color.White ' Set font color
            menuPanel.Controls.Add(btnApproveLeave)
        End If

        ' Add a PictureBox for the IITG logo
        Dim iitgLogoPictureBox As New PictureBox()
        Try
            ' Load the image file from the same directory as the application executable
            iitgLogoPictureBox.Image = LeaveManagementSystem.My.Resources.IITG_logo

            ' Set PictureBox properties
            iitgLogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage
            iitgLogoPictureBox.Size = New Size(50, 50) ' Set the size of the logo PictureBox
            iitgLogoPictureBox.Location = New Point(20, 20) ' Position the logo at the top left corner
            Me.Controls.Add(iitgLogoPictureBox)
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
        AddHandler logoutButton.Click, AddressOf HomePageForm.LogoutButton_Click
    End Sub
    Private Sub SubmitButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SubmitButton.Click
        ' Perform validation before submitting
        Dim start As Date = StartDate.Value
        Dim endd As Date = EndDate.Value
        If ValidateInput(start, endd) Then
            ' Submit leave application

            InsertLeaveApplication(start, endd)
            ShowConfirmationModal(start, endd)
        End If
    End Sub

    Private Function ValidateInput(ByVal start As Date, ByVal endd As Date) As Boolean
        ' Perform validation checks
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
        Dim confirmationMessage As String = "Leave Application Summary:" & vbCrLf &
                                            "Start Date: " & start.ToString("MM/dd/yyyy") & vbCrLf &
                                            "End Date: " & endd.ToString("MM/dd/yyyy") & vbCrLf &
                                            "Leave Type: " & LeaveTypeComboBox.SelectedItem.ToString() & vbCrLf &
                                            "Reason: " & ReasonTextBox.Text

        ' Display confirmation modal
        MessageBox.Show(confirmationMessage, "Leave Application Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub InsertLeaveApplication(ByVal start As Date, ByVal endd As Date)
        Dim leaveId As Integer = GetNextLeaveId()
        Dim iitgId As String = GlobalVariables.Email
        Dim startDate As Date = start
        Dim endDate As Date = endd
        Dim appliedOn As Date = Date.Today
        Dim numberOfLeaves As Integer = (endd - start).Days
        Dim typeOfLeave As String = LeaveTypeComboBox.SelectedItem.ToString()
        Dim statusOfLeave As String = "Pending" ' Assuming the status starts as pending
        Dim reason As String = ReasonTextBox.Text
        Dim approverId As String = "" ' You need to set this to the approver's ID

        Dim query As String = "INSERT INTO leave_table (Leave_ID, IITG_ID, Start_Date, End_Date, Applied_On, Number_Of_Leaves, Type_Of_Leave, Status_Of_Leave, Reason, Approver_ID) " &
                              "VALUES (@leaveId, @iitgId, @startDate, @endDate, @appliedOn, @numberOfLeaves, @typeOfLeave, @statusOfLeave, @reason, @approverId)"

        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@leaveId", leaveId)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            command.Parameters.AddWithValue("@startDate", StartDate)
            command.Parameters.AddWithValue("@endDate", EndDate)
            command.Parameters.AddWithValue("@appliedOn", appliedOn)
            command.Parameters.AddWithValue("@numberOfLeaves", numberOfLeaves)
            command.Parameters.AddWithValue("@typeOfLeave", typeOfLeave)
            command.Parameters.AddWithValue("@statusOfLeave", statusOfLeave)
            command.Parameters.AddWithValue("@reason", reason)
            command.Parameters.AddWithValue("@approverId", approverId)
            command.ExecuteNonQuery()
            MessageBox.Show(iitgId)
        Catch ex As Exception
            MessageBox.Show("Error inserting leave application: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

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

    
End Class
