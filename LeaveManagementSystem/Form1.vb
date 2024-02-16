Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        'Center the login page
        Panel_Login.Location = New Point((Me.Size.Width - Panel_Login.Size.Width) / 2, (Me.Size.Height - Panel_Login.Size.Height) / 2)
    End Sub

    Private Sub Panel_Login_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel_Login.Paint

    End Sub
End Class
