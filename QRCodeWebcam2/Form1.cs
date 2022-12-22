using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Common;

//https://foxlearn.com/windows-forms/qr-code-scanner-using-camera-in-csharp-380.html

namespace QRCodeWebcam2
{
    public partial class Form1 : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        private Color colorBackStart;
        private Color colorBackScan;
        private static readonly List<BarcodeFormat> Fmts = new List<BarcodeFormat> { BarcodeFormat.CODE_39, BarcodeFormat.CODE_128, BarcodeFormat.QR_CODE };

        public bool IsCameraActive { get; private set; }
        public bool IsScanActive { get; private set; }

        public Form1()
        {
            InitializeComponent();
            startBtn.BackColor = Color.WhiteSmoke;
            scanBtn.BackColor = Color.WhiteSmoke;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo Device in filterInfoCollection)
                    cboDevice.Items.Add(Device.Name);
                videoCaptureDevice = new VideoCaptureDevice();
                cboDevice.SelectedIndex = 0;
                pictureBox.Image = null;
            }
            catch (Exception)
            {
                Program.CameraMistake();
            }
        }

        private void CameraStart_Click(object sender, EventArgs e)
        {

            try
            {
                
                if (IsCameraActive)
                {
                    videoCaptureDevice.Stop();
                    pictureBox.Image = null;
                    startBtn.BackColor = Color.WhiteSmoke;//.LightGray;
                    timer1.Stop();
                    scanBtn.BackColor = Color.WhiteSmoke;
                    IsCameraActive = false;
                }
                else
                {
                    pictureBox.Image = null;
                    videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
                    videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
                    videoCaptureDevice.Start();
                    //colorBackStart = startBtn.BackColor;
                    startBtn.BackColor = Color.Green;
                    IsCameraActive = true;
                }
            }
            catch (Exception)
            {
                Program.CameraMistake();
            }
        }

        private void Scan_click(object sender, EventArgs e)
        {
            if (IsScanActive)
            {
                timer1.Stop();
                scanBtn.BackColor = Color.WhiteSmoke;
                IsScanActive = false;
            }
            else
            {
                if (!IsCameraActive)
                    CameraStart_Click(null,null);
                timer1.Start();
                colorBackScan = scanBtn.BackColor;
                scanBtn.BackColor = Color.Green;
                IsScanActive = true;
            }
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.Stop();
                }
            }
            catch (Exception)
            {
                Program.CameraMistake();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                BarcodeReader barcodeReader = new BarcodeReader
                {
                    AutoRotate = true,
                    Options = {
                        TryInverted = true,
                        PossibleFormats = Fmts,
                        TryHarder = true,
                        ReturnCodabarStartEnd = true,
                        PureBarcode = false
                    }
                };
                Bitmap myClone = ((Bitmap)pictureBox.Image).Clone(new Rectangle(0, 0, ((Bitmap)pictureBox.Image).Width, ((Bitmap)pictureBox.Image).Height), PixelFormat.Format24bppRgb);

                Result result = barcodeReader.Decode(myClone);
                if (result != null)
                {                    
                    //MessageBox.Show(result.ToString());
                    //txtQRCode.Text = $"{((!string.IsNullOrWhiteSpace(txtQRCode.Text)) ? (txtQRCode.Text + Environment.NewLine) : "")} result: {result.ToString()} {Environment.NewLine} BarcodeFormat: {result.BarcodeFormat} {Environment.NewLine}";
                    //timer1.Stop();

                    try
                    {
                        txtQRCode.Text = /*$"{((!string.IsNullOrWhiteSpace(txtQRCode.Text)) ? (txtQRCode.Text + Environment.NewLine) : "")}*/$"Fájl elérése:\n{result}";
                        Process.Start(result.ToString());// @"C:\Users\User\Source\Repos\QRCoder\license.txt");//result.ToString());
                        //MessageBox.Show(result.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hiba a ({result}) fájl megnyitásakor!\n\n"
                            + ex.Message);
                        //Logger need: ex.Message into a text file for later can debugging
                    }
                }
            }
        }

        private void TxtQRCode_TextChanged(object sender, EventArgs e)
        {
            Scan_click(null, null);//pictureBox.Image = null;
            //timer1.Stop();
            //try
            //{
            //    //if (videoCaptureDevice.IsRunning)
            //    //{
            //    //    videoCaptureDevice.Stop();
            //    //    pictureBox.Image = null;       //btnStart_Click(null, null);
            //    //}
            //}
            //catch (Exception)
            //{
            //    Program.CameraMistake();
            //}
        }

        private void cboDevice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
