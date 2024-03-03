Imports MySql.Data.MySqlClient

Public Class LeaveHistoryForm
    Private connectionString As String = "server=172.16.114.188;uid=santhosh;database=leavemanagement;"
    Private connection As New MySqlConnection(connectionString)

    'UI buttons
    Private WithEvents btnLeaveHistory As New Button()
    Private WithEvents btnNewLeave As New Button()
    Private WithEvents btnApproveLeave As New Button()


    Private Sub LeaveHistoryForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
      

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
        Me.Text = "Leave History"
        Me.Size = New Size(1000, 1000)
        LeaveHistory_UI()
        FetchLeaveHistory()
        ' Set background color programmatically
        Me.BackColor = Color.AliceBlue ' Set your desired background color here
    End Sub

    Private Sub LeaveHistory_UI()
        Me.WindowState = FormWindowState.Maximized
        ' Add the menu panel
        Dim menuPanel As New Panel()
        menuPanel.BackColor = Color.Black
        menuPanel.Size = New Size(150, Me.Size.Height) ' Adjust height to fit below the logo
        menuPanel.Location = New Point(0, 150) ' Position below the logo
        Me.Controls.Add(menuPanel)

        ' Add buttons inside the menu panel for navigation
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

        dgvLeaveHistory.Location = New Point(dgvLeaveHistory.Width + (Me.Width - menuPanel.Size.Width - dgvLeaveHistory.Size.Width - 2500) / 8, (Me.Height - 150 - dgvLeaveHistory.Size.Height) / 2)



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

    Private Sub FetchLeaveHistory()
        Try
            Dim query As String = "SELECT Start_Date, End_Date, Applied_On, Number_Of_Leaves, Type_Of_Leave, Status_Of_Leave, Reason FROM leave_table WHERE IITG_ID = @Email"

            Using connection As New MySqlConnection(connectionString)
                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@Email", GlobalVariables.Email)

                    Dim da As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    da.Fill(table)

                    If table.Rows.Count > 0 Then
                        dgvLeaveHistory.DataSource = table
                        With dgvLeaveHistory
                            .Columns("Start_Date").HeaderText = "Start Date"
                            .Columns("End_Date").HeaderText = "End Date"
                            .Columns("Applied_On").HeaderText = "Applied On"
                            .Columns("Number_Of_Leaves").HeaderText = "Number of Leaves"
                            .Columns("Type_Of_Leave").HeaderText = "Type of Leave"
                            .Columns("Status_Of_Leave").HeaderText = "Approval Status"
                            .Columns("Reason").HeaderText = "Reason"
                        End With
                    Else
                        dgvLeaveHistory.DataSource = GetNoHistoryDataTable()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while fetching leave history: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetNoHistoryDataTable() As DataTable
        Dim table As New DataTable()
        table.Columns.Add("Message")
        table.Rows.Add("No leave history found for the current user.")
        Return table
    End Function



    ' Event handler method for the logout button click event
    Public Sub LogoutButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Add logout functionality here
        GlobalVariables.Email = ""
        GlobalVariables.Designation = ""

        Me.Hide()

        LoginForm.Show()
        MessageBox.Show("Logged out successfully!", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLeaveHistory.CellContentClick

    End Sub
End Class