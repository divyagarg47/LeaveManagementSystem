Imports MySql.Data.MySqlClient

Public Class LoginForm

    ' Connection string for your MySQL database
    Dim connectionString As String = "server=172.16.114.188;uid=santhosh;database=leavemanagement;"

    ' Event handler for the Login button click
    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        ' Get user input from the textboxes
        Dim userId As String = txtUserID.Text
        Dim password As String = txtPassword.Text

        ' Check if both fields are filled
        If String.IsNullOrEmpty(userId) OrElse String.IsNullOrEmpty(password) Then
            MessageBox.Show("Please enter both email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Exit the event, preventing further code execution
            Exit Sub
        End If

        ' Check if the user exists in the lookup table
        Dim designation As String = GetUserDesignation(userId)

        If designation IsNot Nothing Then
            ' User exists in lookup table, now check the password in the corresponding table
            If CheckPassword(userId, password, designation) Then
                ' Set global variables for user details
                GlobalVariables.Email = userId
                If designation = "hod" OrElse designation = "dean" OrElse designation = "director" Then
                    designation = "faculty"
                    GlobalVariables.Approver = "1"
                ElseIf designation = "supervisor" OrElse designation = "head_supervisor" Then
                    designation = "staff"
                    GlobalVariables.Approver = "1"
                End If
                GlobalVariables.Designation = designation

                ' Clear textboxes
                txtUserID.Text = ""
                txtPassword.Text = ""

                ' Hide the login form and show the appropriate form based on designation
                Me.Hide()
                Dim homePageForm As New HomePageForm()
                Dim adminPageForm As New AdminForm()
                If designation = "admin" Then
                    adminPageForm.Show()
                Else
                    homePageForm.Show()
                End If
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
             Else
                MessageBox.Show("Incorrect Password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("User does not exist!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Function to get the user's designation from the lookup table
    Private Function GetUserDesignation(ByVal userId As String) As String
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim query As String = "SELECT designation FROM lookup_table WHERE IITG_ID = @UserID"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserID", userId)
                Return TryCast(command.ExecuteScalar(), String)
            End Using
        End Using
    End Function

    ' Function to check if the entered password is correct based on the designation
    Private Function CheckPassword(ByVal userId As String, ByVal password As String, ByVal designation As String) As Boolean
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            GlobalVariables.Approver = "0"

            ' if the user is an approver set him as approver
            If designation = "hod" OrElse designation = "dean" OrElse designation = "director" Then
                designation = "faculty"
                GlobalVariables.Approver = "1"
            ElseIf designation = "supervisor" OrElse designation = "head_supervisor" Then
                designation = "staff"
                GlobalVariables.Approver = "1"
            End If

            Dim query As String = "SELECT password FROM " & designation & " WHERE IITG_ID = @UserID"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserID", userId)
                Dim storedPassword As String = Convert.ToString(command.ExecuteScalar())
                ' match password with the stored password
                Return password = storedPassword
            End Using
        End Using
    End Function

    ' Event handler for the Login form load event
    Private Sub LoginForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the form state to maximized
        Me.WindowState = FormWindowState.Maximized

        ' Center the login page
        GroupBox1.Location = New Point((Me.Size.Width - GroupBox1.Size.Width) / 2, (Me.Size.Height - GroupBox1.Size.Height) / 2)

        ' Add a PictureBox for the IITG logo
        Dim iitgLogoPictureBox As New PictureBox()
        Try
            ' Load the image file from the same directory as the application executable
            iitgLogoPictureBox.Image = LeaveManagementSystem.My.Resources.IITG_logo

            ' Set PictureBox properties
            iitgLogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage
            iitgLogoPictureBox.Size = New Size(100, 100)
            iitgLogoPictureBox.Location = New Point(GroupBox1.Location.X + ((GroupBox1.Size.Width - iitgLogoPictureBox.Size.Width) / 2), GroupBox1.Location.Y + 20)

            ' Add the PictureBox to the form
            Me.Controls.Add(iitgLogoPictureBox)
            iitgLogoPictureBox.BringToFront()
        Catch ex As Exception
            ' Handle file loading errors
            MessageBox.Show("An error occurred while loading the image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Event handler for the Change Password button click
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Clear textboxes
        txtUserID.Text = ""
        txtPassword.Text = ""

        ' Hide the login form and show the Change Password form
        Me.Hide()
        ChangePasswordForm.Show()
    End Sub

    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class
