Imports MySql.Data.MySqlClient

Public Class ApproveLeaveForm
    Dim connectionString As String = "server=127.0.0.1;uid=root;password=;database=leavemanagement"
    Dim connection As New MySqlConnection(connectionString)
    Private approverEmail As String = GlobalVariables.Email ' This should be dynamically set based on the logged-in user

    Private Sub ApproveLeaveForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ConfigureDataGridView()
        FetchLeaveRequestsForApprover()
    End Sub

    Private Sub ConfigureDataGridView()
        With dgvLeaveRequests
            .Columns.Clear()
            .Columns.Add("Name", "User Name")
            .Columns.Add("Email", "Email")
            .Columns.Add("Start_Date", "Start Date")
            .Columns.Add("End_Date", "End Date")
            .Columns.Add("Leave_Type", "Leave Type")
            .Columns.Add("Reason", "Reason")
            .Columns.Add("Leaves_Left", "Leaves Left")

            Dim acceptButtonColumn As New DataGridViewButtonColumn()
            acceptButtonColumn.Name = "AcceptButton"
            acceptButtonColumn.HeaderText = "Accept"
            acceptButtonColumn.Text = "Accept"
            acceptButtonColumn.UseColumnTextForButtonValue = True
            .Columns.Add(acceptButtonColumn)

            Dim rejectButtonColumn As New DataGridViewButtonColumn()
            rejectButtonColumn.Name = "RejectButton"
            rejectButtonColumn.HeaderText = "Reject"
            rejectButtonColumn.Text = "Reject"
            rejectButtonColumn.UseColumnTextForButtonValue = True
            .Columns.Add(rejectButtonColumn)
        End With
    End Sub

    Private Sub FetchLeaveRequestsForApprover()
        Dim query As String = $"SELECT lt.Email, lt.Start_Date, lt.End_Date, lt.Leave_Type, lt.Applied_On, lt.Reason, lt.Leaves_Left, 
                               COALESCE(st.Name, ft.Name, sft.Name) AS Name
                               FROM leave_table lt
                               LEFT JOIN lookup_table lkt ON lt.Email = lkt.Email
                               LEFT JOIN student_table st ON lkt.Email = st.Email AND lkt.Designation = 'Student'
                               LEFT JOIN faculty_table ft ON lkt.Email = ft.Email AND lkt.Designation = 'Faculty' OR 'Dean' OR 'HoD'
                               LEFT JOIN staff_table sft ON lkt.Email = sft.Email AND lkt.Designation = 'Staff' OR 'Supervisor' OR 'Head_Supervisor'
                               WHERE lt.Status = 'Pending' AND lt.Approver_Id = '{approverEmail}'"

        Using connection As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, connection)
                Try
                    connection.Open()
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        Dim dt As New DataTable
                        dt.Load(reader)
                        dgvLeaveRequests.DataSource = dt
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Failed to fetch leave requests: " & {ex.Message})
                End Try
            End Using
        End Using
    End Sub

    Private Sub dgvLeaveRequests_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dgvLeaveRequests.CellClick
        If e.RowIndex >= 0 Then
            If dgvLeaveRequests.Columns(e.ColumnIndex).Name = "AcceptButton" OrElse dgvLeaveRequests.Columns(e.ColumnIndex).Name = "RejectButton" Then
                Dim userEmail As String = dgvLeaveRequests.Rows(e.RowIndex).Cells("Email").Value.ToString()
                Dim status As String = If(dgvLeaveRequests.Columns(e.ColumnIndex).Name = "AcceptButton", "Accepted", "Rejected")
                UpdateLeaveStatus(userEmail, status)
                If status = "Accepted" Then
                    UpdateLeavesLeft(userEmail)
                End If
                FetchLeaveRequestsForApprover() ' Refresh the data grid view
            End If
        End If
    End Sub

    Private Sub UpdateLeaveStatus(ByVal email As String, ByVal status As String)
        Dim query As String = $"UPDATE leave_table SET Status = '{status}' WHERE Email = '{email}' AND Approver_Id = '{approverEmail}'"
        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show($"Leave has been {status} for {email}.")
                Catch ex As Exception
                    MessageBox.Show($"Error updating leave status: {ex.Message}")
                End Try
            End Using
        End Using
    End Sub

    Private Sub UpdateLeavesLeft(ByVal email As String)
        Dim query As String = $"UPDATE leave_table SET LeavesLeft = LeavesLeft - 1 WHERE Email = '{email}' AND Status = 'Accepted'"
        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MessageBox.Show($"Error updating leaves left: {ex.Message}")
                End Try
            End Using
        End Using
    End Sub
End Class