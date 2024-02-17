Imports MySql.Data.MySqlClient

Public Class LeaveHistoryForm

    Private Sub LeaveHistoryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FetchLeaveHistory()
    End Sub

    Private Sub FetchLeaveHistory()
        Dim connectionString As String = "server=127.0.0.1;uid=root;password=;database=leavemanagement"
        Dim connection As New MySqlConnection(connectionString)

        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "SELECT Start_Date, End_Date, Applied_On, Number_Of_Leaves, Type_Of_Leave, Status_Of_Leave, Reason  FROM leave_table"
                Dim cmd As New MySqlCommand(query, connection)
                Dim da As New MySqlDataAdapter(cmd)
                Dim table As New DataTable()
                da.Fill(table)

                'dgvLeaveHistory is DataGridView's name
                dgvLeaveHistory.DataSource = table

                With dgvLeaveHistory
                    .Columns("Start_Date").HeaderText = "Start Date"
                    .Columns("End_Date").HeaderText = "End Date"
                    .Columns("Applied_On").HeaderText = "Applied On"
                    .Columns("Number_Of_Leaves").HeaderText = "Number of Leaves"
                    .Columns("Type_Of_Leave").HeaderText = "Type of Leave"
                    .Columns("Status_Of_Leave").HeaderText = "Approval Status"
                    .Columns("Reason").HeaderText = "Reason"
                End With

            Catch ex As Exception
                MessageBox.Show($"An error occurred: {ex.Message}")
            Finally
                connection.Close()
            End Try
        End Using
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLeaveHistory.CellContentClick

    End Sub
End Class
