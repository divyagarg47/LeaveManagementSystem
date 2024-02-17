Imports MySql.Data.MySqlClient

Public Class HomePageForm

    ' Connection string for your MySQL database
    Dim connectionString As String = "server=127.0.0.1;uid=root;password=;database=leavemanagement;"

    Private Sub HomePageForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Load user information when the form loads
        LoadUserInfo()
    End Sub

    ' Function to load user information
    Private Sub LoadUserInfo()
        Dim userEmail As String = GlobalVariables.Email

        ' Check if the user exists in the lookup table
        Dim designation As String = GetUserDesignation(userEmail)

        If designation IsNot Nothing Then
            ' User exists in lookup table, now get the user's name
            Dim userName As String = GetUserName(userEmail, designation)

            ' Display user information
            lblUserEmail.Text = "Email: " & userEmail
            lblUserName.Text = "Name: " & userName

            ' Show or hide the approve leave button based on designation
            btnApproveLeave.Visible = (designation = "HoD" OrElse designation = "Supervisor") ' Change this condition based on your business logic
        Else
            MessageBox.Show("User information not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub btnLeaveHistory_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLeaveHistory.Click
        ' Code to open leave history form

    End Sub

    Private Sub btnNewLeave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNewLeave.Click

    End Sub

    Private Sub btnApproveLeave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApproveLeave.Click

    End Sub

End Class