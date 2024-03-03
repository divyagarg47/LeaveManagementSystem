Imports MySql.Data.MySqlClient

Public Class LeaveHistoryForm

    Private WithEvents logoutButton As New Button()
    'buttons
    Private WithEvents btnLeaveHistory As New Button()
    Private WithEvents btnNewLeave As New Button()
    Private WithEvents btnApproveLeave As New Button()
    Private Sub LeaveHistoryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LeaveHistory_UI()
        FetchLeaveHistory()
    End Sub
    Private Sub LeaveHistory_UI()
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

    Private Sub FetchLeaveHistory()
        Dim connectionString As String = "server=172.16.114.188;uid=santhosh;database=leavemanagement;"
        'connection As New MySqlConnection(connectionString)

        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "SELECT Start_Date, End_Date, Applied_On, Number_Of_Leaves, Type_Of_Leave, Status_Of_Leave, Reason  FROM leave_table WHERE IITG_ID={GlobalVariables.Email}"
                Dim cmd As New MySqlCommand(query, connection)
                Dim da As New MySqlDataAdapter(cmd)
                Dim table As New DataTable()
                da.Fill(table)

                'dgvLeaveHistory is DataGridView's name
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

            Catch ex As Exception
                MessageBox.Show("An error occurred: {ex.Message}")
            Finally
                connection.Close()
            End Try
        End Using
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLeaveHistory.CellContentClick

    End Sub

    
End Class
