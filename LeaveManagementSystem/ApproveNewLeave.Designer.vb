<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ApproveNewLeave
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dataGridViewLeaveRequests = New System.Windows.Forms.DataGridView()
        CType(Me.dataGridViewLeaveRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dataGridViewLeaveRequests
        '
        Me.dataGridViewLeaveRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridViewLeaveRequests.Location = New System.Drawing.Point(12, 12)
        Me.dataGridViewLeaveRequests.Name = "dataGridViewLeaveRequests"
        Me.dataGridViewLeaveRequests.RowTemplate.Height = 28
        Me.dataGridViewLeaveRequests.Size = New System.Drawing.Size(549, 438)
        Me.dataGridViewLeaveRequests.TabIndex = 0
        '
        'ApproveNewLeave
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 462)
        Me.Controls.Add(Me.dataGridViewLeaveRequests)
        Me.Name = "ApproveNewLeave"
        Me.Text = "ApproveNewLeave"
        CType(Me.dataGridViewLeaveRequests, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dataGridViewLeaveRequests As System.Windows.Forms.DataGridView
End Class
