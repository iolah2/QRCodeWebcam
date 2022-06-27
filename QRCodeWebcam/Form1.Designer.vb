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
        Me.components = New System.ComponentModel.Container()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboDevice = New System.Windows.Forms.ComboBox()
        Me.pictureBox = New System.Windows.Forms.PictureBox()
        Me.txtQRCode = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.Color.Lime
        Me.btnStart.Location = New System.Drawing.Point(457, 243)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(208, 108)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Camera:"
        '
        'cboDevice
        '
        Me.cboDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDevice.FormattingEnabled = True
        Me.cboDevice.Location = New System.Drawing.Point(74, 13)
        Me.cboDevice.Name = "cboDevice"
        Me.cboDevice.Size = New System.Drawing.Size(356, 21)
        Me.cboDevice.TabIndex = 2
        '
        'pictureBox
        '
        Me.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictureBox.Location = New System.Drawing.Point(16, 57)
        Me.pictureBox.Name = "pictureBox"
        Me.pictureBox.Size = New System.Drawing.Size(435, 291)
        Me.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBox.TabIndex = 3
        Me.pictureBox.TabStop = False
        '
        'txtQRCode
        '
        Me.txtQRCode.Location = New System.Drawing.Point(457, 57)
        Me.txtQRCode.Multiline = True
        Me.txtQRCode.Name = "txtQRCode"
        Me.txtQRCode.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtQRCode.Size = New System.Drawing.Size(208, 180)
        Me.txtQRCode.TabIndex = 4
        '
        'Timer1
        '
        Me.Timer1.Interval = 2
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(679, 363)
        Me.Controls.Add(Me.txtQRCode)
        Me.Controls.Add(Me.pictureBox)
        Me.Controls.Add(Me.cboDevice)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnStart)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "QR Code"
        CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnStart As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cboDevice As ComboBox
    Friend WithEvents pictureBox As PictureBox
    Friend WithEvents txtQRCode As TextBox
    Friend WithEvents Timer1 As Timer
End Class
