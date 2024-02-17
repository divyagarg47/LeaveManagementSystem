Imports MySql.Data.MySqlClient

Public Class LoginForm

    ' Connection string for your MySQL database
    Dim connectionString As String = "server=127.0.0.1;uid=root;password=;database=leavemanagement;"

    Private Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        Dim userId As String = txtUserID.Text
        Dim password As String = txtPassword.Text

        ' Check if the user exists in the lookup table
        Dim designation As String = GetUserDesignation(userId)

        If designation IsNot Nothing Then
            ' User exists in lookup table, now check the password in the corresponding table
            If CheckPassword(userId, password, designation) Then
                GlobalVariables.Email = userId
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

    End Sub
End Class