<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HomePageForm
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
        Me.lblUserEmail = New System.Windows.Forms.Label()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.btnLeaveHistory = New System.Windows.Forms.Button()
        Me.btnNewLeave = New System.Windows.Forms.Button()
        Me.btnApproveLeave = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblUserEmail
        '
        Me.lblUserEmail.AutoSize = True
        Me.lblUserEmail.Location = New System.Drawing.Point(35, 39)
        Me.lblUserEmail.Name = "lblUserEmail"
        Me.lblUserEmail.Size = New System.Drawing.Size(51, 17)
        Me.lblUserEmail.TabIndex = 0
        Me.lblUserEmail.Text = "Label1"
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Location = New System.Drawing.Point(34, 95)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(51, 17)
        Me.lblUserName.TabIndex = 1
        Me.lblUserName.Text = "Label2"
        '
        'btnLeaveHistory
        '
        Me.btnLeaveHistory.Location = New System.Drawing.Point(186, 81)
        Me.btnLeaveHistory.Name = "btnLeaveHistory"
        Me.btnLeaveHistory.Size = New System.Drawing.Size(75, 23)
        Me.btnLeaveHistory.TabIndex = 2
        Me.btnLeaveHistory.Text = "Button1"
        Me.btnLeaveHistory.UseVisualStyleBackColor = True
        '
        'btnNewLeave
        '
        Me.btnNewLeave.Location = New System.Drawing.Point(165, 139)
        Me.btnNewLeave.Name = "btnNewLeave"
        Me.btnNewLeave.Size = New System.Drawing.Size(75, 23)
        Me.btnNewLeave.TabIndex = 3
        Me.btnNewLeave.Text = "Button2"
        Me.btnNewLeave.UseVisualStyleBackColor = True
        '
        'btnApproveLeave
        '
        Me.btnApproveLeave.Location = New System.Drawing.Point(98, 186)
        Me.btnApproveLeave.Name = "btnApproveLeave"
        Me.btnApproveLeave.Size = New System.Drawing.Size(75, 23)
        Me.btnApproveLeave.TabIndex = 4
        Me.btnApproveLeave.Text = "Button3"
        Me.btnApproveLeave.UseVisualStyleBackColor = True
        '
        'HomePageForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(282, 253)
        Me.Controls.Add(Me.btnApproveLeave)
        Me.Controls.Add(Me.btnNewLeave)
        Me.Controls.Add(Me.btnLeaveHistory)
        Me.Controls.Add(Me.lblUserName)
        Me.Controls.Add(Me.lblUserEmail)
        Me.Name = "HomePageForm"
        Me.Text = "HomePageForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUserEmail As System.Windows.Forms.Label
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents btnLeaveHistory As System.Windows.Forms.Button
    Friend WithEvents btnNewLeave As System.Windows.Forms.Button
    Friend WithEvents btnApproveLeave As System.Windows.Forms.Button
End Class
