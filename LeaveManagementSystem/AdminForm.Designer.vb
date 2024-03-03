<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminForm
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
        Me.ComboBoxTables = New System.Windows.Forms.ComboBox()
        Me.ComboBoxActions = New System.Windows.Forms.ComboBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TextBoxValues = New System.Windows.Forms.TextBox()
        Me.ButtonExecute = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBoxTables
        '
        Me.ComboBoxTables.FormattingEnabled = True
        Me.ComboBoxTables.Location = New System.Drawing.Point(198, 60)
        Me.ComboBoxTables.Name = "ComboBoxTables"
        Me.ComboBoxTables.Size = New System.Drawing.Size(121, 24)
        Me.ComboBoxTables.TabIndex = 0
        '
        'ComboBoxActions
        '
        Me.ComboBoxActions.FormattingEnabled = True
        Me.ComboBoxActions.Location = New System.Drawing.Point(198, 116)
        Me.ComboBoxActions.Name = "ComboBoxActions"
        Me.ComboBoxActions.Size = New System.Drawing.Size(121, 24)
        Me.ComboBoxActions.TabIndex = 1
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(198, 177)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(733, 325)
        Me.DataGridView1.TabIndex = 2
        '
        'TextBoxValues
        '
        Me.TextBoxValues.Location = New System.Drawing.Point(208, 541)
        Me.TextBoxValues.Name = "TextBoxValues"
        Me.TextBoxValues.Size = New System.Drawing.Size(100, 22)
        Me.TextBoxValues.TabIndex = 3
        '
        'ButtonExecute
        '
        Me.ButtonExecute.Location = New System.Drawing.Point(377, 541)
        Me.ButtonExecute.Name = "ButtonExecute"
        Me.ButtonExecute.Size = New System.Drawing.Size(75, 23)
        Me.ButtonExecute.TabIndex = 4
        Me.ButtonExecute.Text = "Execute"
        Me.ButtonExecute.UseVisualStyleBackColor = True
        '
        'AdminForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1037, 595)
        Me.Controls.Add(Me.ButtonExecute)
        Me.Controls.Add(Me.TextBoxValues)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ComboBoxActions)
        Me.Controls.Add(Me.ComboBoxTables)
        Me.Name = "AdminForm"
        Me.Text = "AdminForm"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBoxTables As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxActions As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBoxValues As System.Windows.Forms.TextBox
    Friend WithEvents ButtonExecute As System.Windows.Forms.Button
End Class
