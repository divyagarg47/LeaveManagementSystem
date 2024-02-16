Imports MySql.Data.MySqlClient

Public Class Form2
    Dim connectionString As String = "server=127.0.0.1;uid=root;password=;database=leavemanagement"
    Dim connection As New MySqlConnection(connectionString)

    Private Sub ConnectToMySQL()
        Try
            connection.Open()
            MessageBox.Show("Connected to MySQL Server")
            ' You're connected, you can perform database operations here
        Catch ex As Exception
            MessageBox.Show("Error connecting to MySQL Server: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        ConnectToMySQL()
    End Sub
End Class
