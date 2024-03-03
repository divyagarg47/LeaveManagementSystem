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
        Me.PanelInput = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PanelInput.SuspendLayout()
        Me.SuspendLayout()
        '
        'StartDate
        '
        Me.StartDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StartDate.Location = New System.Drawing.Point(27, 149)
        Me.StartDate.Name = "StartDate"
        Me.StartDate.Size = New System.Drawing.Size(200, 26)
        Me.StartDate.TabIndex = 0
        '
        'EndDate
        '
        Me.EndDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EndDate.Location = New System.Drawing.Point(284, 149)
        Me.EndDate.Name = "EndDate"
        Me.EndDate.Size = New System.Drawing.Size(200, 26)
        Me.EndDate.TabIndex = 1
        '
        'LeaveTypeComboBox
        '
        Me.LeaveTypeComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LeaveTypeComboBox.FormattingEnabled = True
        Me.LeaveTypeComboBox.Location = New System.Drawing.Point(142, 199)
        Me.LeaveTypeComboBox.Name = "LeaveTypeComboBox"
        Me.LeaveTypeComboBox.Size = New System.Drawing.Size(214, 28)
        Me.LeaveTypeComboBox.TabIndex = 2
        Me.LeaveTypeComboBox.Text = "Select Leave Type"
        '
        'ReasonTextBox
        '
        Me.ReasonTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReasonTextBox.Location = New System.Drawing.Point(27, 283)
        Me.ReasonTextBox.Name = "ReasonTextBox"
        Me.ReasonTextBox.Size = New System.Drawing.Size(457, 26)
        Me.ReasonTextBox.TabIndex = 3
        '
        'SubmitButton
        '
        Me.SubmitButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubmitButton.Location = New System.Drawing.Point(207, 324)
        Me.SubmitButton.Name = "SubmitButton"
        Me.SubmitButton.Size = New System.Drawing.Size(89, 36)
        Me.SubmitButton.TabIndex = 4
        Me.SubmitButton.Text = "Submit"
        Me.SubmitButton.UseVisualStyleBackColor = True
        '
        'PanelInput
        '
        Me.PanelInput.BackColor = System.Drawing.Color.White
        Me.PanelInput.Controls.Add(Me.Label3)
        Me.PanelInput.Controls.Add(Me.Label2)
        Me.PanelInput.Controls.Add(Me.Label1)
        Me.PanelInput.Controls.Add(Me.SubmitButton)
        Me.PanelInput.Controls.Add(Me.ReasonTextBox)
        Me.PanelInput.Controls.Add(Me.LeaveTypeComboBox)
        Me.PanelInput.Controls.Add(Me.EndDate)
        Me.PanelInput.Controls.Add(Me.StartDate)
        Me.PanelInput.ForeColor = System.Drawing.Color.Black
        Me.PanelInput.Location = New System.Drawing.Point(16, 39)
        Me.PanelInput.Name = "PanelInput"
        Me.PanelInput.Size = New System.Drawing.Size(508, 426)
        Me.PanelInput.TabIndex = 5
        Me.PanelInput.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(334, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 25)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "End Date:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(69, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 25)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Start Date:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(22, 246)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(166, 25)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Reason(Optional)"
        '
        'ApplyLeaveForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(665, 600)
        Me.Controls.Add(Me.PanelInput)
        Me.Name = "ApplyLeaveForm"
        Me.Text = "NewLeaveForm"
        Me.PanelInput.ResumeLayout(False)
        Me.PanelInput.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents EndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents LeaveTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ReasonTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SubmitButton As System.Windows.Forms.Button
    Friend WithEvents PanelInput As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
