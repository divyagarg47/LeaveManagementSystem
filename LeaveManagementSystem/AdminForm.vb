Public Class AdminForm
    Private Sub AdminForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Load user profiles into DataGridView
        LoadUserProfiles()
    End Sub

    Private Sub LoadUserProfiles()
        ' Retrieve user profiles from the database and bind them to DataGridView
        ' Example: Execute a SELECT query to fetch user profiles from the database
        ' Populate DataGridView with the results
    End Sub

    Private Sub AddButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddButton.Click
        ' Add new user profile
        ' Example: Execute an INSERT query to add a new user profile to the database
        ' Refresh DataGridView to display the updated list of user profiles
        LoadUserProfiles()
    End Sub

    Private Sub UpdateButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles UpdateButton.Click
        ' Update selected user profile
        ' Example: Execute an UPDATE query to modify the selected user profile in the database
        ' Refresh DataGridView to reflect the changes
        LoadUserProfiles()
    End Sub

    Private Sub DeleteButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles DeleteButton.Click
        ' Delete selected user profile
        ' Example: Execute a DELETE query to remove the selected user profile from the database
        ' Refresh DataGridView to update the list of user profiles
        LoadUserProfiles()
    End Sub
End Class
