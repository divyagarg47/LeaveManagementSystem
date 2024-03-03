Imports MySql.Data.MySqlClient

Public Class HomePageForm

    Inherits Form
    Dim userEmail As String
    Dim userName As String
    Public designationLabel As New Label()
    Public iitgIdLabel As New Label()
    Public rollNoLabel As New Label()
    Public programLabel As New Label()
    Public departmentLabel As New Label()
    Public typeofstafflabel As New Label()

    'buttons
    Private WithEvents btnLeaveHistory As New Button()
    Private WithEvents btnNewLeave As New Button()
    Private WithEvents btnApproveLeave As New Button()
    ' Connection string for your MySQL database
    Dim connectionString As String = "server=172.16.114.188;uid=santhosh;database=leavemanagement;"

    'Button connections
    Private Sub btnLeaveHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLeaveHistory.Click
        Me.Hide()

        Dim leaveHistoryForm As New LeaveHistoryForm()
        leaveHistoryForm.Show()
    End Sub
    Private Sub btnNewLeave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewLeave.Click
        Me.Hide()
        Dim applyLeaveForm As New ApplyLeaveForm()
        applyLeaveForm.Show()
    End Sub
    Private Sub btnApproveLeave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApproveLeave.Click

        Me.Hide()
        Dim approveNewForm As New ApproveNewLeave()
        approveNewForm.Show()
    End Sub

    Private Sub HomePageForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    End Sub
    ' Function to load user information
    Private Sub LoadUserInfo()
        userEmail = GlobalVariables.Email
        Dim designation As String = GlobalVariables.Designation

        ' Check if the user exists in the lookup table
        If designation IsNot Nothing Then
            ' User exists in lookup table, now get the user's name
            userName = GetUserName(userEmail, designation)
            designationLabel.Text = "Designation: " + designation
            GetLabels(userEmail, designation)
        Else
            MessageBox.Show("User information not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Function GetElement(ByVal userEmail As String, ByVal table As String, ByVal item As String) As String
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim query As String = "SELECT " + item + " FROM " + table + " WHERE IITG_ID = @UserEmail"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserEmail", userEmail)
                Return TryCast(command.ExecuteScalar(), String)
            End Using
        End Using
    End Function

    Private Sub GetLabels(ByVal userEmail As String, ByVal designation As String)
        If designation = "student" Then
            'get roll no
            rollNoLabel.Text = GetElement(userEmail, designation, "Roll_Number")

            'get program label
            programLabel.Text = GetElement(userEmail, designation, "Programme")

            'get department
            departmentLabel.Text = GetElement(userEmail, designation, "Department")
        ElseIf designation = "faculty" Then
            'get department
            departmentLabel.Text = GetElement(userEmail, designation, "Department")
            'get department
        ElseIf designation = "HoD" Then
            'get department
            departmentLabel.Text = GetElement(userEmail, designation, "Department")
        ElseIf designation = "staff" Then

            'get type of staff
            typeofstafflabel.Text = GetElement(userEmail, designation, "Department")
        End If
    End Sub

    ' Function to get the user's name from the corresponding designation table
    Private Function GetUserName(ByVal userEmail As String, ByVal designation As String) As String
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim query As String = "SELECT Name FROM " & designation & " WHERE IITG_ID = @UserEmail"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserEmail", userEmail)
                Return TryCast(command.ExecuteScalar(), String)
            End Using
        End Using

    End Function
    ' Front end

    ' Declare the button with WithEvents keyword
    Private WithEvents logoutButton As New Button()

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Set up form properties
        Me.Text = "Home Page"
        Me.Size = New Size(1000, 1000)

        LoadUserInfo()
        Load_UI()
        ' Load user information when the form loads
        ' Set background color programmatically
        Me.BackColor = Color.AliceBlue ' Set your desired background color here
    End Sub

    Private Sub Load_UI()
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

        btnNewLeave.Text = "Apply New Leave"
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
        headerLabel.Location = New Point((Me.ClientSize.Width - headerLabel.Width) \ 3, 20)
        Me.Controls.Add(headerLabel)



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

        ' Add labels for user information
        Dim nameLabel As New Label()
        nameLabel.Text = "Name: " + userName
        nameLabel.AutoSize = True
        nameLabel.Font = New Font("Arial", 14, FontStyle.Regular)
        nameLabel.Location = New Point((Me.ClientSize.Width - nameLabel.Width) \ 2, 200) ' Center horizontally
        Me.Controls.Add(nameLabel)


        iitgIdLabel.Text = "IITG ID: " + userEmail
        iitgIdLabel.AutoSize = True
        iitgIdLabel.Font = New Font("Arial", 14, FontStyle.Regular)
        iitgIdLabel.Location = New Point((Me.ClientSize.Width - iitgIdLabel.Width) \ 2, 240) ' Center horizontally
        Me.Controls.Add(iitgIdLabel)



        designationLabel.AutoSize = True
        designationLabel.Font = New Font("Arial", 14, FontStyle.Regular)
        designationLabel.Location = New Point((Me.ClientSize.Width - designationLabel.Width) \ 2, 280) ' Center horizontally
        Me.Controls.Add(designationLabel)

        If designationLabel.Text = "Designation: student" Then
            rollNoLabel.Text = "Roll No.: " + rollNoLabel.Text
            rollNoLabel.AutoSize = True
            rollNoLabel.Font = New Font("Arial", 14, FontStyle.Regular)
            rollNoLabel.Location = New Point((Me.ClientSize.Width - rollNoLabel.Width) \ 2, 320) ' Center horizontally
            Me.Controls.Add(rollNoLabel)


            programLabel.Text = "Program: " + programLabel.Text
            programLabel.AutoSize = True
            programLabel.Font = New Font("Arial", 14, FontStyle.Regular)
            programLabel.Location = New Point((Me.ClientSize.Width - programLabel.Width) \ 2, 360) ' Center horizontally
            Me.Controls.Add(programLabel)



            departmentLabel.Text = "Department: " + departmentLabel.Text
            departmentLabel.AutoSize = True
            departmentLabel.Font = New Font("Arial", 14, FontStyle.Regular)
            departmentLabel.Location = New Point((Me.ClientSize.Width - departmentLabel.Width) \ 2, 400) ' Center horizontally
            Me.Controls.Add(departmentLabel)

        End If

        If designationLabel.Text = "Designation: faculty" Or designationLabel.Text = "Designation: HoD" Then


            departmentLabel.Text = "Department: " + departmentLabel.Text
            departmentLabel.AutoSize = True
            departmentLabel.Font = New Font("Arial", 14, FontStyle.Regular)
            departmentLabel.Location = New Point((Me.ClientSize.Width - departmentLabel.Width) \ 2, 320) ' Center horizontally
            Me.Controls.Add(departmentLabel)

        End If

        If designationLabel.Text = "Designation: staff" Or designationLabel.Text = "Designation: supervisor" Then
            typeofstafflabel.Text = "Staff Type: " + typeofstafflabel.Text
            typeofstafflabel.AutoSize = True
            typeofstafflabel.Font = New Font("Arial", 14, FontStyle.Regular)
            typeofstafflabel.Location = New Point((Me.ClientSize.Width - typeofstafflabel.Width) \ 2, 320) ' Center horizontally
            Me.Controls.Add(typeofstafflabel)

        End If
    End Sub
    ' Event handler method for the logout button click event
    Public Sub LogoutButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Add logout functionality here
        GlobalVariables.Email = ""
        GlobalVariables.Designation = ""

        userEmail = ""
        userName = ""
        designationLabel.Text = ""
        iitgIdLabel.Text = ""
        rollNoLabel.Text = ""
        programLabel.Text = ""
        departmentLabel.Text = ""
        typeofstafflabel.Text = ""
        Me.Hide()

        LoginForm.Show()
        MessageBox.Show("Logged out successfully!", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class