Imports MySql.Data.MySqlClient

Public Class ApproveNewLeave

    Dim connectionString As String = "server=172.16.114.188;uid=santhosh;database=leavemanagement;"
    Dim leaveRequestsTable As New DataTable("LeaveRequests")

    Private Sub LeaveApprovalForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        
    End Sub
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Set up form properties
        Me.Text = "Apply Leave"
        InitializeDataGridView()
        LoadLeaveRequestsData()
        ' Set background color programmatically
        Me.BackColor = Color.AliceBlue ' Set your desired background color here
    End Sub

    Private Sub InitializeDataGridView()

        ' Add columns to the DataGridView
        leaveRequestsTable.Columns.Add("Leave_ID", GetType(String))
        leaveRequestsTable.Columns.Add("IITG_ID", GetType(String))
        leaveRequestsTable.Columns.Add("Start_Date", GetType(Date))
        leaveRequestsTable.Columns.Add("End_Date", GetType(Date))
        leaveRequestsTable.Columns.Add("Applied_On", GetType(Date))
        leaveRequestsTable.Columns.Add("Number_Of_Leaves", GetType(Integer))
        leaveRequestsTable.Columns.Add("Type_Of_Leave", GetType(String))
        leaveRequestsTable.Columns.Add("Reason", GetType(String))
        ' Add more columns as needed

        ' Set the DataGridView's DataSource to the DataTable
        dataGridViewLeaveRequests.DataSource = leaveRequestsTable

        ' Add Accept and Reject buttons to the DataGridView
        Dim acceptButtonColumn As New DataGridViewButtonColumn()
        acceptButtonColumn.Name = "AcceptButton"
        acceptButtonColumn.Text = "Accept"
        acceptButtonColumn.UseColumnTextForButtonValue = True
        dataGridViewLeaveRequests.Columns.Add(acceptButtonColumn)

        Dim rejectButtonColumn As New DataGridViewButtonColumn()
        rejectButtonColumn.Name = "RejectButton"
        rejectButtonColumn.Text = "Reject"
        rejectButtonColumn.UseColumnTextForButtonValue = True
        dataGridViewLeaveRequests.Columns.Add(rejectButtonColumn)

    End Sub

    Private Sub LoadLeaveRequestsData()
        ' Fetch leave requests data from the database based on the logged-in approver ID
        leaveRequestsTable.Rows.Clear()
        Dim approverID As String = GlobalVariables.Email ' Replace with your actual method to get the logged-in approver ID
        Dim query As String = "SELECT * FROM leave_table WHERE Approver_ID = @approverID AND Status_Of_Leave = 'Pending'"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                Try
                    command.Parameters.AddWithValue("@approverID", approverID)
                    connection.Open()
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    While reader.Read()
                        leaveRequestsTable.Rows.Add(reader("Leave_ID"), reader("IITG_ID"), reader("Start_Date"), reader("End_Date"), reader("Applied_On"), reader("Number_Of_Leaves"), reader("Type_Of_Leave"), reader("Reason"))
                    End While

                   
                Catch ex As Exception
                    MessageBox.Show("Error" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub dataGridViewLeaveRequests_CellContentClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dataGridViewLeaveRequests.CellContentClick
        If e.RowIndex >= 0 AndAlso (dataGridViewLeaveRequests.Columns(e.ColumnIndex).Name = "AcceptButton" OrElse dataGridViewLeaveRequests.Columns(e.ColumnIndex).Name = "RejectButton") Then
            ' Access the clicked row
            Dim clickedRow As DataGridViewRow = dataGridViewLeaveRequests.Rows(e.RowIndex)
            Dim selectedLeaveID As String = CInt(clickedRow.Cells("Leave_ID").Value)
            Dim selectedEmailID As String = clickedRow.Cells("IITG_ID").Value.ToString()

            If dataGridViewLeaveRequests.Columns(e.ColumnIndex).Name = "AcceptButton" Then
                UpdateLeaveStatus(selectedLeaveID, selectedEmailID, "Accepted")
            ElseIf dataGridViewLeaveRequests.Columns(e.ColumnIndex).Name = "RejectButton" Then
                UpdateLeaveStatus(selectedLeaveID, selectedEmailID, "Rejected")
            End If
        End If
    End Sub

    Private Sub UpdateLeaveStatus(ByVal LeaveID As String, ByVal EmailId As String, ByVal status As String)
        ' Update the status in the database
        Dim updateQuery As String = "UPDATE leave_table SET Status_Of_Leave = @status WHERE Leave_ID = @LeaveID AND IITG_ID = @EmailId"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(updateQuery, connection)
                Try
                    command.Parameters.AddWithValue("@status", status)
                    command.Parameters.AddWithValue("@LeaveID", LeaveID)
                    command.Parameters.AddWithValue("@EmailId", EmailId)
                    connection.Open()
                    command.ExecuteNonQuery()

                    MessageBox.Show("Leave " + status + " successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    LoadLeaveRequestsData()

                Catch ex As Exception
                    MessageBox.Show("Error" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub
End Class
