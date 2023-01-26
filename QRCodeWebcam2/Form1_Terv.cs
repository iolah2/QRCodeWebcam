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
    public partial class FormTerv : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        int width = 220;
        int height = 220;
        //private Color colorBackStart;
        private Color colorBackScan;
        //Timer timer2;
        private static readonly List<BarcodeFormat> Fmts = new List<BarcodeFormat> { /*BarcodeFormat.CODE_39, BarcodeFormat.CODE_128,*/ BarcodeFormat.QR_CODE };
        // private readonly List<LinkLocal> LinkLocals;

        public bool IsCameraActive { get; private set; }
        public bool IsScanActive { get; private set; }
        //private int linePosition;
        private int szamlaloLink;
        //private BarcodeReader barcodeReader;

        public FormTerv()
        {
            Width = 1920; Height = 1280;
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            //this.WindowState = FormWindowState.Maximized;
            //startBtn.BackColor = Color.WhiteSmoke;
            scanBtn.BackColor = Color.WhiteSmoke;
            //linePosition = 0;
            szamlaloLink = 1;
            pictureBox.Paint += PictureBox_Paint;
            
            //  LinkLocals = new List<LinkLocal>();
            //  this.Controls.Remove(pictureBox1);
            //flowLayoutPanel1 = new FlowLayoutPanel
            //{
            //    AutoScroll = true,
            //    FlowDirection = FlowDirection.TopDown,
            //    WrapContents = false
            //};            
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            var rect = new Rectangle((pictureBox.Width - width) / 2, (pictureBox.Height - height) / 2, width, height);
            var p = new Pen(Brushes.White, 2);
            e.Graphics.DrawRectangle(p, rect);

        }        

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                GetCameras();
                videoCaptureDevice = new VideoCaptureDevice();
                pictureBox.Image = null;
                CameraStart_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Program.CameraMistake();
            }
        }


        private void GetCameras()
        {
#if DEBUG
            if(Environment.MachineName == "ISTI-PC")
            return;
#endif
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (filterInfoCollection.Count == 0)
                throw new Exception("Nem találtunk kamerát!");
            for (int i = 0; i < filterInfoCollection.Count; i++)
            {
                var device = filterInfoCollection[i];
                cboDevice.Items.Add(device.Name);
                if (device.Name.ToUpper().Contains("REAR"))
                {
                    cboDevice.SelectedIndex = i;
                }
            }
            if (cboDevice.SelectedIndex == -1)
                cboDevice.SelectedIndex = 0;
        }
#if DEBUG
        private void AddLinklabeToFlowLayoutPanel(string testing)
        {
            var linkLabel = new LinkLabel()
            {

                AutoSize = true,
                Location = new System.Drawing.Point(3, 0),
                Name = "linkLabel" + szamlaloLink,

                //this.linkLabel1.Text = "linkLabel1";
            };
            //linkLabel.BackColor = Color.Yellow;
            linkLabel.Size = new System.Drawing.Size(55, 13);
            //linkLabel1.TabIndex = 0;
            linkLabel.TabStop = true;
            var linkAct = new LinkLocal();
            linkLabel.Text = $"{szamlaloLink++}. {testing.ToString().Split('\\')?.Last()}";//linkAct.Show =
            linkLabel.Links.Add(0, linkLabel.Text.Length, testing.ToString());// = linkAct.Path = result.ToString();
            linkLabel.AutoSize = true;
            //LinkLocals.Add(linkAct);
            linkLabel.LinkClicked += LinkLabel1_LinkClicked;
            flowLayoutPanel1.Controls.Add(linkLabel);
        }
#endif  
        private void CameraStart_Click(object sender, EventArgs e)
        {
#if DEBUG
            if (Environment.MachineName == "ISTI-PC")
            {
#if DEBUG

                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
#endif
                return;
            }
#endif
            try
            {
                if (cboDevice.Items.Count == 0)
                {
                    MessageBox.Show("Kérem csatlakoztassa a kamerát, majd nyomjon az OK gombra!\nEgyéb esetben a kódolvasó leáll!");
                    GetCameras();
                    if (cboDevice.Items.Count == 0)
                        throw new Exception("Nincs elérhető kamera!");
                }
                ///Camera turn off
                if (IsCameraActive)
                {
                    videoCaptureDevice.Stop();
                    pictureBox.Image = null;
                    //startBtn.BackColor = Color.WhiteSmoke;//.LightGray;
                    timer1.Stop();
                    scanBtn.BackColor = Color.WhiteSmoke;
                    IsCameraActive = false;
                }
                else ///Turn on for camera
                {
                    pictureBox.Image = null;
                    videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
                    videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
                    videoCaptureDevice.Start();

                    //startBtn.BackColor = Color.Green;
                    IsCameraActive = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Program.CameraMistake();
            }
        }

        private void Scan_click(object sender, EventArgs e)
        {
            if (IsScanActive) ///Scan stop
            {
                timer1.Stop();
                //barcodeReader = null;
                scanBtn.BackColor = Color.WhiteSmoke;
                IsScanActive = false;
            }
            else ///Scan start
            {
                if (!IsCameraActive)
                    CameraStart_Click(null, null);

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

                #region kivett 2023 jan 19
                //var t1 = DateTime.Now;
                ////////int x = (pictureBox.Width - width) / 2;
                ////////int y = (pictur
                ///eBox.Height - height) / 2;

                ////////int a = pictureBox.Image.Width / pictureBox.Width;
                ////////int b = pictureBox.Image.Height / pictureBox.Height;


                ////////var rect = new Rectangle(a * x + (x % 2) * (width / 2), b * y + (y % 2) * (height / 2), Math.Max(a, b) * width, Math.Max(a, b) * height);//x, y, width, height);
                ////////var croppedImage = ((Bitmap)pictureBox.Image)
                ////////    .Clone(rect, pictureBox.Image.PixelFormat);
                #endregion

                var result = barcodeReader.Decode((Bitmap)pictureBox.Image);//croppedImage);
                //if (result != null)
                //{
                //    pictureBox.Image.Save(@"F:\2023\Varrival\Test\" + szamlaloLink + "_test.jpg");
                //    //Process.Start(@"F:\2023\Varrival\Test\" + szamlaloLink + "_test.jpg");
                //}

                if (result != null)
                {
                    Scan_click(null, null);
                    AddLinklabeToFlowLayoutPanel(result);
                    try
                    {

//#if DEBUG
//if(Environment.MachineName == "ISTI-PC")
//                        throw new Exception();
//#endif

                        Process.Start(result.ToString());
                        //Timer t1 = new Timer();

                    }
                    catch (Exception ex)
                    {
//#if DEBUG
//if(Environment.MachineName == "ISTI-PC")
//                        flowLayoutPanel1.Controls.Add(new Label() { Text = result.ToString() });
//#else                        
                        MessageBox.Show($"Hiba a ({result}) fájl megnyitásakor!\n\n"
                            + ex.Message);
                        RemoveLastLinklabeToFlowLayoutPanel(result);
//#endif
                    }
                }
            }
        }

        private void AddLinklabeToFlowLayoutPanel(Result result)
        {
            var linkLabel = new LinkLabel()
            {

                AutoSize = true,
                Location = new System.Drawing.Point(3, 0),
                Name = "linkLabel" + szamlaloLink,

                //this.linkLabel1.Text = "linkLabel1";
            };
            //linkLabel.BackColor = Color.Yellow;
            linkLabel.Size = new System.Drawing.Size(55, 13);
            //linkLabel1.TabIndex = 0;
            linkLabel.TabStop = true;
            var linkAct = new LinkLocal();
            linkLabel.Text = $"{szamlaloLink++}. {result.ToString().Split('\\')?.Last()}";//linkAct.Show =
            linkLabel.Links.Add(0, result.ToString().Length, result.ToString());// = linkAct.Path = result.ToString();
            linkLabel.AutoSize = true;
            //LinkLocals.Add(linkAct);
            linkLabel.LinkClicked += LinkLabel1_LinkClicked;
            flowLayoutPanel1.Controls.Add(linkLabel);
        }

        private void RemoveLastLinklabeToFlowLayoutPanel(Result result)
        {
            flowLayoutPanel1.Controls.RemoveAt(flowLayoutPanel1.Controls.Count - 1);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        
        private void cboDevice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_TextChanged(object sender, EventArgs e)
        {
            //pictureBox.Image = null;
        }

        private class LinkLocal
        {
            public string Path { get; set; }
            public string Show { get; set; }
        }

        private void ListaTorleseBtn_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
        }        
    }
}
