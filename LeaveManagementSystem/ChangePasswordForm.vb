Imports MySql.Data.MySqlClient

Public Class ChangePasswordForm

    ' Connection string for your MySQL database
    Dim connectionString As String = "server=172.16.114.188;uid=santhosh;database=leavemanagement;"

    Private Sub btnChangePassword_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangePassword.Click
        Dim userEmail As String = txtEmail.Text
        Dim oldPassword As String = txtOldPassword.Text
        Dim newPassword As String = txtNewPassword.Text

        If String.IsNullOrEmpty(userEmail) OrElse String.IsNullOrEmpty(oldPassword) OrElse String.IsNullOrEmpty(newPassword) Then
            MessageBox.Show("Please enter all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Exit the event, preventing further code execution
            Exit Sub
        End If

        If newPassword.Equals(oldPassword) Then
            MessageBox.Show("New password can not be same as old passsword.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Exit the event, preventing further code execution
            Exit Sub
        End If

        If Not ValidateNewPassword(newPassword) Then
            MessageBox.Show("New password should have atlease 1 capital letter, 1 numeric value and 1 special character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Exit the event, preventing further code execution
            Exit Sub
        End If

        ' Check if the user exists in the lookup table
        Dim designation As String = GetUserDesignation(userEmail)

        If designation IsNot Nothing Then
            ' User exists in lookup table, now check the old password in the corresponding table
            If CheckOldPassword(userEmail, oldPassword, designation) Then
                ' Update the password
                If UpdatePassword(userEmail, newPassword, designation) Then
                    txtEmail.Text = ""
                    txtOldPassword.Text = ""
                    txtNewPassword.Text = ""
                    Me.Hide()
                    LoginForm.Show()
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

    Private Function ValidateNewPassword(ByVal newPassword As String) As Boolean
        ' Check if the new password has at least one capital letter, one number, and one special character
        Return System.Text.RegularExpressions.Regex.IsMatch(newPassword, "(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9])")
    End Function

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

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        txtEmail.Text = ""
        txtOldPassword.Text = ""
        txtNewPassword.Text = ""
        Me.Hide()
        LoginForm.Show()
    End Sub

    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class