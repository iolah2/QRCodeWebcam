Imports AForge.Video
Imports AForge.Video.DirectShow
Imports ZXing
Imports ZXing.Common

'https://foxlearn.com/windows-forms/qr-code-scanner-using-camera-in-csharp-380.html
'https://www.codeproject.com/Questions/535124/connectingplusapluswebcamplususingplusaforge-netpl
'https://laptrinhvb.net/index.php?/bai-viet/chuyen-de-vb-net/Huong-dan-lap-trinh-ung-dung-doc-QR-(Scan-QR-)--code-su-dung-thu-vien-Zxing--bang-VB-NET/14750387ccc5e533.html

Public Class Form1

    Dim filterInfoCollection As FilterInfoCollection
    Dim videoCaptureDevice As VideoCaptureDevice
    Dim frmt As New List(Of BarcodeFormat) From {BarcodeFormat.CODE_39, BarcodeFormat.CODE_128, BarcodeFormat.QR_CODE}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        filterInfoCollection = New FilterInfoCollection(FilterCategory.VideoInputDevice)
        For Each filterInfo In filterInfoCollection
            cboDevice.Items.Add(filterInfo.Name)
        Next
        cboDevice.SelectedIndex = 0
        videoCaptureDevice = New VideoCaptureDevice()
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If btnStart.Text Is "Stop" Then
            btnStart.Text = "Start"
            btnStart.BackColor = Color.Lime
            If Timer1.Enabled Then
                Timer1.Stop()
            End If
            If videoCaptureDevice.IsRunning() Then
                videoCaptureDevice.Stop()
            End If
            pictureBox.Image = Nothing
            Return
        End If

        btnStart.Text = "Stop"
        btnStart.BackColor = Color.Crimson
        pictureBox.Image = Nothing
        videoCaptureDevice = New VideoCaptureDevice(filterInfoCollection(cboDevice.SelectedIndex).MonikerString)

        If Not cboDevice.SelectedValue Like "*Front*" Then 'the below code isn't working with a tablets Front camera
            Dim videoCapabilities() As VideoCapabilities 'this variable was created to handle Logitech C920s webcams. They don't have a default resolution so this variable is used to collect available resolutions then select the first. https://stackoverflow.com/questions/50302574/usb-cam-feed-not-displaying-in-picturebox-using-c-sharp-and-aforge
            videoCapabilities = videoCaptureDevice.VideoCapabilities
            videoCaptureDevice.VideoResolution = videoCapabilities(0)
        End If

        AddHandler videoCaptureDevice.NewFrame, AddressOf FinalFrame_NewFrame
        videoCaptureDevice.Start()
        Timer1.Start()
    End Sub

    Private Sub FinalFrame_NewFrame(sender As Object, e As NewFrameEventArgs)
        pictureBox.Image = CType(e.Frame.Clone(), Bitmap)
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Timer1.Enabled Then
            Timer1.Stop()
        End If
        If videoCaptureDevice.IsRunning() Then
            videoCaptureDevice.Stop()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If pictureBox.Image IsNot Nothing Then
            Dim barcodeReader As New BarcodeReader()
            barcodeReader.AutoRotate = True

            Dim options As New DecodingOptions()
            options.TryInverted = True
            options.PossibleFormats = frmt
            options.TryHarder = True
            options.ReturnCodabarStartEnd = True
            options.PureBarcode = False

            barcodeReader.Options = options

            Dim result As Result = barcodeReader.Decode(CType(pictureBox.Image, Bitmap))
            Try
                If result IsNot Nothing Then
                    If Timer1.Enabled Then
                        Timer1.Stop()
                    End If
                    If videoCaptureDevice.IsRunning() Then
                        videoCaptureDevice.Stop()
                    End If
                    btnStart.Text = "Start"
                    btnStart.BackColor = Color.Lime

                    Dim decoded As String = txtQRCode.Text & Environment.NewLine & "Result: " & result.ToString().Trim() & Environment.NewLine & "BarcodeFormat: " & result.BarcodeFormat & Environment.NewLine & Environment.NewLine
                    txtQRCode.Text = decoded
                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub cboDevice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDevice.SelectedIndexChanged
        btnStart.Text = "Start"
        btnStart.BackColor = Color.Lime
        If Timer1.Enabled Then
            Timer1.Stop()
        End If
        If videoCaptureDevice IsNot Nothing Then
            If videoCaptureDevice.IsRunning() Then
                videoCaptureDevice.Stop()
                btnStart_Click(sender, e)
            End If
        End If
        pictureBox.Image = Nothing
    End Sub

End Class
