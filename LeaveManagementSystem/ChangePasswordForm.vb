Imports MySql.Data.MySqlClient

Public Class ChangePasswordForm

    ' Connection string for your MySQL database
    Dim connectionString As String = "server=127.0.0.1;uid=root;password=;database=leavemanagement;"

    Private Sub btnChangePassword_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangePassword.Click
        Dim userEmail As String = txtEmail.Text
        Dim oldPassword As String = txtOldPassword.Text
        Dim newPassword As String = txtNewPassword.Text

        ' Check if the user exists in the lookup table
        Dim designation As String = GetUserDesignation(userEmail)

        If designation IsNot Nothing Then
            ' User exists in lookup table, now check the old password in the corresponding table
            If CheckOldPassword(userEmail, oldPassword, designation) Then
                ' Update the password
                If UpdatePassword(userEmail, newPassword, designation) Then
                    MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Failed to update password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Incorrect Old Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("User does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Function to get the user's designation from the lookup table
    Private Function GetUserDesignation(ByVal userEmail As String) As String
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim query As String = "SELECT Designation FROM lookup_table WHERE IITG_ID = @UserEmail"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserEmail", userEmail)
                Return TryCast(command.ExecuteScalar(), String)
            End Using
        End Using
    End Function

    ' Function to check if the entered old password is correct based on the designation
    Private Function CheckOldPassword(ByVal userEmail As String, ByVal oldPassword As String, ByVal designation As String) As Boolean
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim query As String = "SELECT Password FROM " & designation & " WHERE IITG_ID = @UserEmail"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserEmail", userEmail)
                Dim storedPassword As String = Convert.ToString(command.ExecuteScalar())
                Return oldPassword = storedPassword
            End Using
        End Using
    End Function

    ' Function to update the password based on the designation
    Private Function UpdatePassword(ByVal userEmail As String, ByVal newPassword As String, ByVal designation As String) As Boolean
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim query As String = "UPDATE " & designation & " SET password = @NewPassword WHERE IITG_ID = @UserEmail"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserEmail", userEmail)
                command.Parameters.AddWithValue("@NewPassword", newPassword)
                Return command.ExecuteNonQuery() > 0
            End Using
        End Using
    End Function

    Private Sub ChangePasswordForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        GroupBox1.Location = New Point((Me.Size.Width - GroupBox1.Size.Width) / 2, (Me.Size.Height - GroupBox1.Size.Height) / 2)

    End Sub

    Private Sub txtEmail_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtEmail.TextChanged

    End Sub
End Class