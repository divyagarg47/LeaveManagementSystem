Imports MySql.Data.MySqlClient

Public Class LoginForm

    ' Connection string for your MySQL database
    Dim connectionString As String = "server=172.16.114.188;uid=santhosh;database=leavemanagement;"

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        Dim userId As String = txtUserID.Text
        Dim password As String = txtPassword.Text

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
                GlobalVariables.Email = userId
                GlobalVariables.Designation = designation
                txtUserID.Text = ""
                txtPassword.Text = ""
                Me.Hide()
                HomePageForm.Show()
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
            Dim query As String = "SELECT password FROM " & designation & " WHERE IITG_ID = @UserID"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserID", userId)
                Dim storedPassword As String = Convert.ToString(command.ExecuteScalar())
                Return password = storedPassword
            End Using
        End Using
    End Function

    Private Sub LoginForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        'Center the login page
        GroupBox1.Location = New Point((Me.Size.Width - GroupBox1.Size.Width) / 2, (Me.Size.Height - GroupBox1.Size.Height) / 2)
        ' Add a PictureBox for the IITG logo
        Dim iitgLogoPictureBox As New PictureBox()
        Try
            ' Load the image file from the same directory as the application executable
            Dim imagePath As String = "C:\Users\akach\Desktop\6th sem\course\se\LeaveManagementSystem\Pics\IITG_logo.png"
            If System.IO.File.Exists(imagePath) Then
                iitgLogoPictureBox.Image = Image.FromFile(imagePath)

                ' Set PictureBox properties
                iitgLogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage
                iitgLogoPictureBox.Size = New Size(100, 50) ' Set the size of the logo PictureBox
                iitgLogoPictureBox.Location = New Point((Me.Size.Width - GroupBox1.Size.Width) / 2 + 20, (Me.Size.Height - GroupBox1.Size.Height) / 2 + 10) ' Position the logo at the top left corner
                Me.Controls.Add(iitgLogoPictureBox)
            Else
                MessageBox.Show("The image file '" & imagePath & "' could not be found.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ' Handle file loading errors
            MessageBox.Show("An error occurred while loading the image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtUserID.Text = ""
        txtPassword.Text = ""
        Me.Hide()
        ChangePasswordForm.Show()
    End Sub

End Class