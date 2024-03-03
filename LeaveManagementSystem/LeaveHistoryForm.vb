Imports MySql.Data.MySqlClient

Public Class LeaveHistoryForm
    Private connectionString As String = "server=172.16.114.188;uid=santhosh;database=leavemanagement;"
    Private connection As New MySqlConnection(connectionString)

    Private Sub LeaveHistoryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FetchLeaveHistory()
    End Sub

    Private Sub FetchLeaveHistory()
        Try
            Dim query As String = "SELECT Start_Date, End_Date, Applied_On, Number_Of_Leaves, Type_Of_Leave, Status_Of_Leave, Reason FROM leave_table WHERE IITG_ID = @Email"

            Using connection As New MySqlConnection(connectionString)
                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@Email", GlobalVariables.Email)

                    Dim da As New MySqlDataAdapter(cmd)
                    Dim table As New DataTable()
                    da.Fill(table)

                    If table.Rows.Count > 0 Then
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
                    Else
                        dgvLeaveHistory.DataSource = GetNoHistoryDataTable()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while fetching leave history: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetNoHistoryDataTable() As DataTable
        Dim table As New DataTable()
        table.Columns.Add("Message")
        table.Rows.Add("No leave history found for the current user.")
        Return table
    End Function




    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvLeaveHistory.CellContentClick

    End Sub
End Class