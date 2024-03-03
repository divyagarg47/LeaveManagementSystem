<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ApplyLeaveForm
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
        Me.StartDate = New System.Windows.Forms.DateTimePicker()
        Me.EndDate = New System.Windows.Forms.DateTimePicker()
        Me.LeaveTypeComboBox = New System.Windows.Forms.ComboBox()
        Me.ReasonTextBox = New System.Windows.Forms.TextBox()
        Me.SubmitButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'StartDate
        '
        Me.StartDate.Location = New System.Drawing.Point(36, 65)
        Me.StartDate.Name = "StartDate"
        Me.StartDate.Size = New System.Drawing.Size(200, 22)
        Me.StartDate.TabIndex = 0
        '
        'EndDate
        '
        Me.EndDate.Location = New System.Drawing.Point(295, 65)
        Me.EndDate.Name = "EndDate"
        Me.EndDate.Size = New System.Drawing.Size(200, 22)
        Me.EndDate.TabIndex = 1
        '
        'LeaveTypeComboBox
        '
        Me.LeaveTypeComboBox.FormattingEnabled = True
        Me.LeaveTypeComboBox.Location = New System.Drawing.Point(180, 119)
        Me.LeaveTypeComboBox.Name = "LeaveTypeComboBox"
        Me.LeaveTypeComboBox.Size = New System.Drawing.Size(156, 24)
        Me.LeaveTypeComboBox.TabIndex = 2
        Me.LeaveTypeComboBox.Text = "Select Leave Type"
        '
        'ReasonTextBox
        '
        Me.ReasonTextBox.Location = New System.Drawing.Point(170, 161)
        Me.ReasonTextBox.Name = "ReasonTextBox"
        Me.ReasonTextBox.Size = New System.Drawing.Size(177, 22)
        Me.ReasonTextBox.TabIndex = 3
        Me.ReasonTextBox.Text = "Reason(Optional)"
        '
        'SubmitButton
        '
        Me.SubmitButton.Location = New System.Drawing.Point(212, 214)
        Me.SubmitButton.Name = "SubmitButton"
        Me.SubmitButton.Size = New System.Drawing.Size(89, 36)
        Me.SubmitButton.TabIndex = 4
        Me.SubmitButton.Text = "Submit"
        Me.SubmitButton.UseVisualStyleBackColor = True
        '
        'ApplyLeaveForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 338)
        Me.Controls.Add(Me.SubmitButton)
        Me.Controls.Add(Me.ReasonTextBox)
        Me.Controls.Add(Me.LeaveTypeComboBox)
        Me.Controls.Add(Me.EndDate)
        Me.Controls.Add(Me.StartDate)
        Me.Name = "ApplyLeaveForm"
        Me.Text = "NewLeaveForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents EndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents LeaveTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ReasonTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SubmitButton As System.Windows.Forms.Button
End Class
