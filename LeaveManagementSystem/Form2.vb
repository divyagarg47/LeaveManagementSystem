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
    Private Sub InsertRecord(ByVal rollNumber As Integer, ByVal iitgId As String, ByVal password As String, ByVal name As String, ByVal department As String, ByVal programme As String, ByVal onLeave As Boolean)
        Dim query As String = "INSERT INTO student (Roll_Number, IITG_ID, Password, Name, Department, Programme, On_Leave) VALUES (@rollNumber, @iitgId, @password, @name, @department, @programme, @onLeave)"

        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@rollNumber", rollNumber)
            command.Parameters.AddWithValue("@iitgId", iitgId)
            command.Parameters.AddWithValue("@password", password)
            command.Parameters.AddWithValue("@name", name)
            command.Parameters.AddWithValue("@department", department)
            command.Parameters.AddWithValue("@programme", programme)
            command.Parameters.AddWithValue("@onLeave", onLeave)
            command.ExecuteNonQuery()
            MessageBox.Show("Record inserted successfully.")
        Catch ex As Exception
            MessageBox.Show("Error inserting record: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        InsertRecord(210101033, "s.bussa", "wer", "Santhosh", "CSE", "B.Tech", False)
    End Sub

    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
