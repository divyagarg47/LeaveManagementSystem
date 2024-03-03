<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LeaveHistoryForm
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
        Me.dgvLeaveHistory = New System.Windows.Forms.DataGridView()
        CType(Me.dgvLeaveHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvLeaveHistory
        '
        Me.dgvLeaveHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLeaveHistory.Location = New System.Drawing.Point(49, 60)
        Me.dgvLeaveHistory.Name = "dgvLeaveHistory"
        Me.dgvLeaveHistory.ReadOnly = True
        Me.dgvLeaveHistory.RowTemplate.Height = 24
        Me.dgvLeaveHistory.Size = New System.Drawing.Size(1196, 398)
        Me.dgvLeaveHistory.TabIndex = 0
        '
        'LeaveHistoryForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(952, 497)
        Me.Controls.Add(Me.dgvLeaveHistory)
        Me.Name = "LeaveHistoryForm"
        Me.Text = "Leave_History"
        CType(Me.dgvLeaveHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvLeaveHistory As System.Windows.Forms.DataGridView
End Class
