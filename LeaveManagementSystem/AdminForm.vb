Imports MySql.Data.MySqlClient

Public Class AdminForm

    ' Connection string to your database
    Private connectionString As String = "server=172.16.114.188;uid=santhosh;database=leavemanagement;"
    Private connection As New MySqlConnection(connectionString)

    Private Sub AdminForm_Load(ByVal sender As Object, ByVal e As EventArgs)
       
    End Sub

    ' Declare the button with WithEvents keyword
    Private WithEvents logoutButton As New Button()

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Set up form properties
        Me.Text = "Admin Form"
        Me.Size = New Size(800, 600)
        ' Add table names to the dropdown
        AdminForm_UI()
        ComboBoxTables.Items.Add("student")
        ComboBoxTables.Items.Add("faculty")
        ComboBoxTables.Items.Add("hod")
        ComboBoxTables.Items.Add("dean")
        ComboBoxTables.Items.Add("director")
        ComboBoxTables.Items.Add("staff")
        ComboBoxTables.Items.Add("supervisor")
        ComboBoxTables.Items.Add("head_supervisor")
        ComboBoxTables.Items.Add("admin")

        ' Add actions to the actions dropdown
        ComboBoxActions.Items.Add("insert")
        ComboBoxActions.Items.Add("delete")
        ComboBoxActions.Items.Add("update")
        ' Set background color programmatically
        Me.BackColor = Color.AliceBlue ' Set your desired background color here
    End Sub
    Private Sub AdminForm_UI()
        Me.WindowState = FormWindowState.Maximized
        DataGridView1.Location = New Point((Me.Size.Width - DataGridView1.Size.Width) / 2, (Me.Size.Height - DataGridView1.Size.Height) / 2)
        ' Add a PictureBox for the IITG logo
        Dim iitgLogoPictureBox As New PictureBox()
        Try
            ' Load the image file from the same directory as the application executable
            iitgLogoPictureBox.Image = LeaveManagementSystem.My.Resources.IITG_logo

            ' Set PictureBox properties
            iitgLogoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage
            iitgLogoPictureBox.Size = New Size(100, 100) ' Set the size of the logo PictureBox
            iitgLogoPictureBox.Location = New Point((150 - iitgLogoPictureBox.Size.Width) / 2, 20) ' Position the logo at the top left corner
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
        logoutButton.Location = New Point(Me.Size.Width, 20)
        Me.Controls.Add(logoutButton) ' Add the button to the form

        ' Attach the event handler using AddHandler
        AddHandler logoutButton.Click, AddressOf HomePageForm.LogoutButton_Click


    End Sub
    Private Function ConstructInsertQuery(ByVal table As String, ByVal values As String) As String
        ' Parse the comma-separated values entered by the user
        Dim parts() As String = values.Split(","c)

        ' Construct the insert query dynamically
        Dim query As String = "INSERT INTO " & table & " VALUES ("
        For i As Integer = 0 To parts.Length - 1
            If i > 0 Then
                query &= ", "
            End If
            query &= "@param" & i
        Next
        query &= ")"

        Return query
    End Function

    Private Function ConstructDeleteQuery(ByVal table As String, ByVal selectedRow As DataGridViewRow) As String
        Dim query As String = "DELETE FROM " & table

        If selectedRow IsNot Nothing AndAlso selectedRow.Cells.Count > 0 Then
            ' Construct the WHERE clause dynamically
            query &= " WHERE "
            For Each column As DataGridViewColumn In DataGridView1.Columns
                Dim columnName As String = column.Name

                Dim cellValue As String = selectedRow.Cells(column.Index).Value.ToString()
                query &= columnName & " = '" & cellValue & "' AND "
            Next
            query = query.TrimEnd("AND ".ToCharArray()) ' Remove the trailing "AND"
        End If

        Return query
    End Function

    Private Sub ButtonExecute_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButtonExecute.Click
        ' Execute selected SQL query
        Dim selectedTable As String = ComboBoxTables.SelectedItem.ToString()
        Dim selectedAction As String = ComboBoxActions.SelectedItem.ToString()
        Dim values As String = TextBoxValues.Text

        If Not String.IsNullOrEmpty(values) Then
            Dim query As String = ""

            If selectedAction.ToLower() = "insert" Then
                ' Construct the insert query dynamically
                query = ConstructInsertQuery(selectedTable, values)
            ElseIf selectedAction.ToLower() = "delete" Then
                ' Retrieve the selected row from the DataGridView
                Dim selectedRow As DataGridViewRow = DataGridView1.CurrentRow
                ' Construct the delete query dynamically
                query = ConstructDeleteQuery(selectedTable, selectedRow)
            ElseIf selectedAction.ToLower() = "update" Then
                ' Retrieve the selected row from the DataGridView
                Dim selectedRow As DataGridViewRow = DataGridView1.CurrentRow
                ' Construct the delete query dynamically
                query = ConstructDeleteQuery(selectedTable, selectedRow)

                ' Construct the insert query dynamically
                query &= ";" & ConstructInsertQuery(selectedTable, values)
                'MessageBox.Show(query)
            End If

            Try
                connection.Open()
                Dim command As New MySqlCommand(query, connection)

                ' Add parameters for insert query
                If Not selectedAction.ToLower() = "delete" Then
                    Dim parts() As String = values.Split(","c)
                    For i As Integer = 0 To parts.Length - 1
                        command.Parameters.AddWithValue("@param" & i, parts(i))
                    Next
                End If

                command.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show("Error executing query: " & ex.Message)
            Finally
                connection.Close()
            End Try

            ' Reload data in DataGridView
            ComboBoxTables_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub ComboBoxTables_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxTables.SelectedIndexChanged
        ' Display data of selected table
        Dim selectedTable As String = ComboBoxTables.SelectedItem.ToString()
        Dim query As String = "SELECT * FROM " & selectedTable

        Try
            connection.Open()
            Dim command As New MySqlCommand(query, connection)
            Dim adapter As New MySqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView1.DataSource = table
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub AdminForm_Load_1(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class