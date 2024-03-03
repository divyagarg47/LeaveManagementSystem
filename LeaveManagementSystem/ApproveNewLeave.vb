Imports MySql.Data.MySqlClient

Public Class ApproveNewLeave

    Dim connectionString As String = "server=172.16.114.188;uid=santhosh;database=leavemanagement;"
    Dim leaveRequestsTable As New DataTable("LeaveRequests")

    Private WithEvents btnLeaveHistory As New Button()
    Private WithEvents btnNewLeave As New Button()
    Private WithEvents btnApproveLeave As New Button()

    Private Sub LeaveApprovalForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    End Sub
   

    Private Sub InitializeDataGridView()

        ' Add columns to the DataGridView
        leaveRequestsTable.Columns.Add("Leave_ID", GetType(String))
        leaveRequestsTable.Columns.Add("IITG_ID", GetType(String))
        leaveRequestsTable.Columns.Add("Start_Date", GetType(Date))
        leaveRequestsTable.Columns.Add("End_Date", GetType(Date))
        leaveRequestsTable.Columns.Add("Applied_On", GetType(Date))
        leaveRequestsTable.Columns.Add("Number_Of_Leaves", GetType(Integer))
        leaveRequestsTable.Columns.Add("Type_Of_Leave", GetType(String))
        leaveRequestsTable.Columns.Add("Reason", GetType(String))
        ' Add more columns as needed

        ' Set the DataGridView's DataSource to the DataTable
        dataGridViewLeaveRequests.DataSource = leaveRequestsTable

        ' Add Accept and Reject buttons to the DataGridView
        Dim acceptButtonColumn As New DataGridViewButtonColumn()
        acceptButtonColumn.Name = "AcceptButton"
        acceptButtonColumn.Text = "Accept"
        acceptButtonColumn.UseColumnTextForButtonValue = True
        dataGridViewLeaveRequests.Columns.Add(acceptButtonColumn)

        Dim rejectButtonColumn As New DataGridViewButtonColumn()
        rejectButtonColumn.Name = "RejectButton"
        rejectButtonColumn.Text = "Reject"
        rejectButtonColumn.UseColumnTextForButtonValue = True
        dataGridViewLeaveRequests.Columns.Add(rejectButtonColumn)

    End Sub

    Private Sub LoadLeaveRequestsData()
        ' Fetch leave requests data from the database based on the logged-in approver ID
        leaveRequestsTable.Rows.Clear()
        Dim approverID As String = GlobalVariables.Email ' Replace with your actual method to get the logged-in approver ID
        Dim query As String = "SELECT * FROM leave_table WHERE Approver_ID = @approverID AND Status_Of_Leave = 'Pending'"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                Try
                    command.Parameters.AddWithValue("@approverID", approverID)
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    While reader.Read()
                        leaveRequestsTable.Rows.Add(reader("Leave_ID"), reader("IITG_ID"), reader("Start_Date"), reader("End_Date"), reader("Applied_On"), reader("Number_Of_Leaves"), reader("Type_Of_Leave"), reader("Reason"))
                    End While

                   
                Catch ex As Exception
                    MessageBox.Show("Error" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub dataGridViewLeaveRequests_CellContentClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dataGridViewLeaveRequests.CellContentClick
        If e.RowIndex >= 0 AndAlso (dataGridViewLeaveRequests.Columns(e.ColumnIndex).Name = "AcceptButton" OrElse dataGridViewLeaveRequests.Columns(e.ColumnIndex).Name = "RejectButton") Then
            ' Access the clicked row
            Dim clickedRow As DataGridViewRow = dataGridViewLeaveRequests.Rows(e.RowIndex)
            Dim selectedLeaveID As String = CInt(clickedRow.Cells("Leave_ID").Value)
            Dim selectedEmailID As String = clickedRow.Cells("IITG_ID").Value.ToString()

            If dataGridViewLeaveRequests.Columns(e.ColumnIndex).Name = "AcceptButton" Then
                UpdateLeaveStatus(selectedLeaveID, selectedEmailID, "Accepted")
            ElseIf dataGridViewLeaveRequests.Columns(e.ColumnIndex).Name = "RejectButton" Then
                UpdateLeaveStatus(selectedLeaveID, selectedEmailID, "Rejected")
            End If
        End If
    End Sub

    Private Sub UpdateLeaveStatus(ByVal LeaveID As String, ByVal EmailId As String, ByVal status As String)
        ' Update the status in the database
        Dim updateQuery As String = "UPDATE leave_table SET Status_Of_Leave = @status WHERE Leave_ID = @LeaveID AND IITG_ID = @EmailId"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(updateQuery, connection)
                Try
                    command.Parameters.AddWithValue("@status", status)
                    command.Parameters.AddWithValue("@LeaveID", LeaveID)
                    command.Parameters.AddWithValue("@EmailId", EmailId)
                    connection.Open()
                    command.ExecuteNonQuery()

                    MessageBox.Show("Leave " + status + " successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    LoadLeaveRequestsData()

                Catch ex As Exception
                    MessageBox.Show("Error" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    'Button connections
    Private Sub btnLeaveHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLeaveHistory.Click
        Me.Hide()

        Dim applyLeaveForm As New ApplyLeaveForm()
        applyLeaveForm.Show()
    End Sub
    Private Sub btnNewLeave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewLeave.Click
        'work as home page
        Me.Hide()
        Dim homePageForm As New HomePageForm()
        homePageForm.Show()
    End Sub


    Private Sub btnApproveLeave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApproveLeave.Click
        'work as home page
        Me.Hide()
        Dim leaveHistoryForm As New LeaveHistoryForm()
        leaveHistoryForm.Show()
    End Sub
    ' Declare the button with WithEvents keyword
    Private WithEvents logoutButton As New Button()

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Set up form properties
        Me.Text = "Approve Leave"
        Me.Size = New Size(1000, 1000)

        InitializeDataGridView()
        LoadLeaveRequestsData()
        ApproveNewLeave_UI()
        ' Set background color programmatically
        Me.BackColor = Color.AliceBlue ' Set your desired background color here
    End Sub

    Private Sub ApproveNewLeave_UI()
        Me.WindowState = FormWindowState.Maximized
        ' Add the menu panel
        Dim menuPanel As New Panel()
        menuPanel.BackColor = Color.Black
        menuPanel.Size = New Size(150, Me.Size.Height) ' Adjust height to fit below the logo
        menuPanel.Location = New Point(0, 150) ' Position below the logo
        Me.Controls.Add(menuPanel)

        ' Add buttons inside the menu panel for navigation%
        btnLeaveHistory.Text = "Apply Leave"
        btnLeaveHistory.Size = New Size(120, 30)
        btnLeaveHistory.Location = New Point(15, 50)
        btnLeaveHistory.ForeColor = Color.White ' Set font color
        menuPanel.Controls.Add(btnLeaveHistory)

        btnNewLeave.Text = "Home Page"
        btnNewLeave.Size = New Size(120, 30)
        btnNewLeave.Location = New Point(15, 100)
        btnNewLeave.ForeColor = Color.White ' Set font color
        menuPanel.Controls.Add(btnNewLeave)

        btnLeaveHistory.Text = "Apply Leave"
        btnLeaveHistory.Size = New Size(120, 30)
        btnLeaveHistory.Location = New Point(15, 150)
        btnLeaveHistory.ForeColor = Color.White ' Set font color
        menuPanel.Controls.Add(btnApproveLeave)



        ' Check if the user's designation is not student, faculty, or staff
        If GlobalVariables.Approver = "1" Then
            ' Add "Approve Leave" button
            btnApproveLeave.Visible = True
            btnApproveLeave.Enabled = True
            btnApproveLeave.Text = "Leave History"
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

        dataGridViewLeaveRequests.Location = New Point(dataGridViewLeaveRequests.Width + (Me.Width - menuPanel.Size.Width - dataGridViewLeaveRequests.Size.Width - 2500) / 8, (Me.Height - 150 - dataGridViewLeaveRequests.Size.Height) / 2)



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
End Class
