<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Panel_Login = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel_Login.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Login
        '
        Me.Panel_Login.AccessibleRole = System.Windows.Forms.AccessibleRole.HotkeyField
        Me.Panel_Login.BackColor = System.Drawing.Color.White
        Me.Panel_Login.Controls.Add(Me.Panel1)
        Me.Panel_Login.Location = New System.Drawing.Point(436, 174)
        Me.Panel_Login.Name = "Panel_Login"
        Me.Panel_Login.Size = New System.Drawing.Size(357, 360)
        Me.Panel_Login.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.LeaveManagementSystem.My.Resources.Resources.IITG_logo
        Me.Panel1.Location = New System.Drawing.Point(78, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(168, 174)
        Me.Panel1.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.ClientSize = New System.Drawing.Size(1228, 709)
        Me.Controls.Add(Me.Panel_Login)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.Panel_Login.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Login As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
