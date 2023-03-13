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
using Microsoft.Win32;
using ZXing;
using ZXing.Common;

//https://foxlearn.com/windows-forms/qr-code-scanner-using-camera-in-csharp-380.html

namespace QRCodeWebcam2
{
    public partial class FormTerv : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        int width = 200;
        int height = 200;
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
            //Todo for test
            //Width = 1920; Height = 1280;
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Maximized;
            //startBtn.BackColor = Color.WhiteSmoke;
            scanBtn.BackColor = Color.WhiteSmoke;
            //linePosition = 0;
            szamlaloLink = 1;
            pictureBox.Paint += PictureBox_Paint;
            SystemEvents.PowerModeChanged += OnPowerModeChanged;
            //  LinkLocals = new List<LinkLocal>();
            //  this.Controls.Remove(pictureBox1);
            //flowLayoutPanel1 = new FlowLayoutPanel
            //{
            //    AutoScroll = true,
            //    FlowDirection = FlowDirection.TopDown,
            //    WrapContents = false
            //};            
        }



        private void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Resume)
            {
                CameraStart_Click(null, null);
                Program.AddMessage("Kamera újraindítva!");//Debug.WriteLine("Now I awake: " + DateTime.Now);
                //videoCaptureDevice.Start();
            }
            else if (e.Mode == PowerModes.Suspend)
            {
                CameraStart_Click(null, null);
                //Debug.WriteLine("Now go to sleep: " + DateTime.Now);
                //videoCaptureDevice.Stop();
            }
        }


        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            //var rect = new Rectangle((pictureBox.Width - width) / 2, (pictureBox.Height - height) / 2, width, height);
            //var p = new Pen(Brushes.White, 2);
            ////var rect2 = new Rectangle((pictureBox.Width - width) / 2-10, (pictureBox.Height - height) / 2-10, width+20, height+20);
            ////var p2 = new Pen(Brushes.Red, 2);
            //e.Graphics.DrawRectangle(p, rect);
            ////e.Graphics.DrawRectangle(p2, rect2);

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
            catch (Exception)
            {
                //Program.AddMessage(ex.Message);
                //MessageBox.Show("Test_1");
                Program.CameraMistake();
            }
        }


        private void GetCameras()
        {
#if DEBUG
            if (Environment.MachineName == "ISTI-PC")
            {
             //   Console.WriteLine();
              //  return;
            }
#endif
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (filterInfoCollection.Count == 0)
            {
                //MessageBox.Show("Test_2");
                Program.CameraMistake();//throw new Exception("Nem találtunk kamerát!");
                return;
            }
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
//#if DEBUG
//            if (Environment.MachineName == "ISTI-PC")
//            {
//                //#if DEBUG
//                Debug.WriteLine("Camera click on/off");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
//                AddLinklabeToFlowLayoutPanel("dfsfdf  fssődf sffd sfdsfd");
////#endif
//                return;
//            }
//#endif
            try
            {
                if (cboDevice.Items.Count == 0)
                {
                    //MessageBox.Show("Test_3");
                    Program.CameraMistake();
                    //Program.AddMessage("Kérem csatlakoztassa a kamerát!\nA kódolvasó leáll!");
                    /*GetCameras();
                    if (cboDevice.Items.Count == 0)
                        throw new Exception("Nincs elérhető kamera!");*/
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
                // Program.AddMessage(ex.Message);
                //MessageBox.Show("Test_4");
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
                //MessageBox.Show("Test_5");
                //Program.CameraMistake();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Stop();

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

                //var rect2 = new Rectangle((pictureBox.Width - width) / 2 - 10, (pictureBox.Height - height) / 2 - 10, width + 20, height + 20);
                ////var p2 = new Pen(Brushes.Red, 2);
                //////////var rect = new Rectangle(a * x + (x % 2) * (width / 2), b * y + (y % 2) * (height / 2), Math.Max(a, b) * width, Math.Max(a, b) * height);//x, y, width, height);
                //var croppedImage = ((Bitmap)pictureBox.Image)
                //    .Clone(rect2,PixelFormat.Format48bppRgb);
                #endregion
                #region febr 1
                //pictureBox.InitialImage.Save(@"F:\2023\Varrival\Test\Full_test.jpg");
                //pictureBox.Image.Save(@"F:\2023\Varrival\Test\Full_image.jpg");
                var originalImage = (Bitmap)pictureBox.Image;
                int x = (originalImage.Width - pictureBox.ClientSize.Width) / 2;// 4;
                int y = (originalImage.Height - pictureBox.ClientSize.Height) / 2;// / 4;
                int width = pictureBox.ClientSize.Width; //originalImage.Width / 2;
                int height = pictureBox.ClientSize.Height; //originalImage.Height / 2;

                Rectangle cropRect = new Rectangle(x, y, width, height);
                Bitmap croppedImage = originalImage.Clone(cropRect, originalImage.PixelFormat);

                pictureBox.Image = croppedImage;

                //var reader = new BarcodeReader();
                //var result = reader.Decode(croppedImage);

                //Bitmap originalImage = (Bitmap)pictureBox.Image;
                //int x = pictureBox.ClientRectangle.X; //.HorizontalScrollBar.Value;
                //int y = pictureBox.ClientRectangle.Y;//VerticalScrollBar.Value;
                //int width = pictureBox.ClientSize.Width;
                //int height = pictureBox.ClientSize.Height;

                //Rectangle cropRect = new Rectangle(pictureBox.Bounds.X, pictureBox.Bounds.Y, width, height);
                //Bitmap croppedImage = originalImage.Clone(cropRect, originalImage.PixelFormat);

                //pictureBox.Image = croppedImage;

                //Bitmap originalImage = (Bitmap)pictureBox.Image;
                //int x = pictureBox.DisplayRectangle.X;
                //int y = pictureBox.DisplayRectangle.Y;
                //int width = pictureBox.DisplayRectangle.Width;
                //int height = pictureBox.DisplayRectangle.Height;

                //Rectangle cropRect = new Rectangle(x, y, width, height);
                //Bitmap croppedImage = originalImage.Clone(cropRect, originalImage.PixelFormat);

                //pictureBox.Image = croppedImage;
                //Bitmap originalImage = (Bitmap)pictureBox.Image;
                //int width = pictureBox.ClientSize.Width;
                //int height = pictureBox.ClientSize.Height;

                //Image resizedImage = originalImage.GetThumbnailImage(width, height, null, IntPtr.Zero);

                //pictureBox.Image = resizedImage;
                #endregion

                //pictureBox.Image.Save(@"F:\2023\Varrival\Test\Showed_part.jpg");
                ////Process.Start(@"F:\2023\Varrival\Test\Full_test.jpg");
                //Process.Start(@"F:\2023\Varrival\Test\Full_image.jpg");
                //Process.Start(@"F:\2023\Varrival\Test\Showed_part.jpg");
                //Application.Exit();
                var result = barcodeReader.Decode((Bitmap)pictureBox.Image);//                                                  croppedImage);
                //if (result != null)
                //{
                //    pictureBox.Image.Save(@"F:\2023\Varrival\Test\" + szamlaloLink + "_test.jpg");
                //    //Process.Start(@"F:\2023\Varrival\Test\" + szamlaloLink + "_test.jpg");
                //}

                if (result != null)
                {
                    Scan_click(null, null);
                    //MessageBox.Show($"{pictureBox.Width}x{pictureBox.Height} and Rectangle {width}x{height} and window: {Width}x{Height}");
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
                        Program.AddMessage($"Hiba a ({result}) fájl megnyitásakor!\n\n"
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

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
