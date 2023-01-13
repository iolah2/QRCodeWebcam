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
        Timer timer2;
        private static readonly List<BarcodeFormat> Fmts = new List<BarcodeFormat> { BarcodeFormat.CODE_39, BarcodeFormat.CODE_128, BarcodeFormat.QR_CODE };
       // private readonly List<LinkLocal> LinkLocals;

        public bool IsCameraActive { get; private set; }
        public bool IsScanActive { get; private set; }
        //private int linePosition;
        private int szamlaloLink;

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            
            startBtn.BackColor = Color.WhiteSmoke;
            scanBtn.BackColor = Color.WhiteSmoke;
            linePosition = 0;
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
            ////Panel panel1 = new Panel();
            //flowLayoutPanel1.AutoScroll = true;
            //flowLayoutPanel1.Controls.Add
            // Add controls to the panel
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 1" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 2" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 3" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 1" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 2" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 3" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 1" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 2" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 3" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 1" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 2" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 1" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 2" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 3" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 1" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 2" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 3" });
            //flowLayoutPanel1.Controls.Add(new Button() { Text = "Button 3" });
            // Add the panel to the group box
            //groupBox1.Controls.Add(panel1);
            //pictureBox1.Location = new Point(/*pictureBox.Location.X +*/ (pictureBox.Width - width) / 2 - 2, /*pictureBox.Location.Y +*/ (pictureBox.Height - height) / 2 - 2);
            //pictureBox1.Size = new Size(width + 4, height + 4);
            //pictureBox1.BackColor = Color.Transparent;
            //pictureBox.Controls.Add(pictureBox1);
            //listView1.View = View.Details;
            //listView1.OwnerDraw = true;
            //listView1.DrawItem += listView1_DrawItem;
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.Clear(Color.Aqua);
            var rect = new Rectangle((pictureBox.Width - width) / 2, (pictureBox.Height - height) / 2, width, height);
            var p = new Pen(Brushes.White, 2);
            e.Graphics.DrawRectangle(p, rect);
            //e.Graphics.DrawRectangle(Pens.Black, 10, 10, 10, 10);
        }

        //private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        //{
        //    LinkLabel linkLabel = new LinkLabel();
        //    linkLabel.Text = e.Item.Text;
        //    linkLabel.Bounds = e.Bounds;
        //    e.Graphics.DrawString(e.Item.Text, e.Item.Font, new SolidBrush(e.Item.ForeColor), e.Bounds);
        //    linkLabel.LinkClicked += (s, ev) =>
        //    {
        //        MessageBox.Show("Link clicked!");
        //    };
        //    listView1.Controls.Add(linkLabel);//e.Items.Add(linkLabel);
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            //linkLabel1.Text = "Link helye";
            //linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            /*timer2 = new Timer();
            timer2.Interval = 30;
            timer2.Tick += Timer2_Tick;
            */
            try
            {
                GetCameras();
                videoCaptureDevice = new VideoCaptureDevice();
                pictureBox.Image = null;
            }
            catch (Exception)
            {
                Program.CameraMistake();
            }
        }
        int direction = 1;
        int width = 180;
        int height = 180;
        //Rectangle? rect = null;
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (pictureBox.Image == null)
                return;

            // Get a Graphics object for the PictureBox and draw the line
            //using (var g = pictureBox.CreateGraphics())
            //{
            ////    //if (rect == null)
            ////    {
            //        var rect = new Rectangle((pictureBox.Width - width) / 2, (pictureBox.Height - height) / 2, width, height);
            //        var p = new Pen(Brushes.White, 2);
            //        g.DrawRectangle(p, rect);
            ////    }
            ////g.DrawLine(Pens.Red, 0, linePosition, pictureBox.Width, linePosition);
            ////    //g.DrawLine(Pens.Red, (pictureBox.Width - width) / 2, (pictureBox.Height - height) / 2 + linePosition, (pictureBox.Width + width) / 2, (pictureBox.Height - height) / 2 + linePosition);

            //}
            //pictureBox.Controls.Remove(pictureBox1);
            //pictureBox1 = new PictureBox();            
            //pictureBox1.Top = TopLevel;
           
            //using (var g2 = pictureBox1.CreateGraphics())
            //{
            //    g2.DrawLine(Pens.Red, 0, linePosition, width+4, linePosition);//(pictureBox.Width - width) / 2, (pictureBox.Height - height) / 2 + linePosition, (pictureBox.Width + width) / 2, (pictureBox.Height - height) / 2 + linePosition);
               
            //    var rect = new Rectangle(2, 2, width, height);
            //    var p = new Pen(Brushes.White, 2);
            //    g2.DrawRectangle(p, rect);
            //}
                //direction = 1;
                // Update the line position
            //    linePosition += 5 * direction;
            //if (linePosition > height)
            //{
            //    direction = -1;
            //    linePosition += 5 * direction;
            //}
            //else if (linePosition < 0)
            //{
            //    direction = 1;
            //    linePosition += 5 * direction;
            //}
        }

        private void GetCameras()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in filterInfoCollection)
                cboDevice.Items.Add(Device.Name);
            if (cboDevice.Items.Count > 0)
                cboDevice.SelectedIndex = 0;
        }

        private void CameraStart_Click(object sender, EventArgs e)
        {

            try
            {
                if (cboDevice.Items.Count == 0)
                {
                    MessageBox.Show("Kérem csatlakoztassa a kamerát, majd nyomjon az OK gombra!\nEgyéb esetben a kódolvasó leáll!");
                    GetCameras();
                    if (cboDevice.Items.Count == 0)
                        throw new Exception("Nincs elérhető kamera!");
                }
                if (IsCameraActive)
                {
                    
                    ////g.DrawLine(Pens.Red, 0, linePosition, pictureBox.Width, linePosition);
                    ////    //g.DrawLine(Pens.Red, (pictureBox.Width - width) / 2, (pictureBox.Height - height) / 2 + linePosition, (pictureBox.Width + width) / 2, (pictureBox.Height - height) / 2 + linePosition);

                    //}
                    //timer2.Stop();
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
                    //using (var g = pictureBox.CreateGraphics())
                    //{
                    //    ////    //if (rect == null)
                    //    ////    {
                    //    var rect = new Rectangle((pictureBox.Width - width) / 2, (pictureBox.Height - height) / 2, width, height);
                    //    var p = new Pen(Brushes.White, 2);
                    //    g.DrawRectangle(p, rect);                        
                    //}
                    //colorBackStart = startBtn.BackColor;
                    startBtn.BackColor = Color.Green;
                    IsCameraActive = true;
                    //timer2.Start();
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
                //var t1 = DateTime.Now;
                int x = (pictureBox.Width - width) / 2;
                int y = (pictureBox.Height - height) / 2;
                //Bitmap myClone = ((Bitmap)pictureBox.Image)
                // .Clone(     //new Rectangle(0, 0, ((Bitmap)pictureBox.Image).Width, ((Bitmap)pictureBox.Image).Height), PixelFormat.Format24bppRgb);
                //////    new Rectangle(x, y, /*((Bitmap)pictureBox.Image).Width*/width, /*((Bitmap)pictureBox.Image).Height)*/height), PixelFormat.Format24bppRgb);
                //////Result result = barcodeReader.Decode(myClone);
                int a = pictureBox.Image.Width / pictureBox.Width;
                int b = pictureBox.Image.Height / pictureBox.Height;

                // Get the image of the scanning square from the PictureBox
                //Bitmap image = new Bitmap(width, height);
                //using (Graphics g = Graphics.FromImage(image))
                //{
                //    g.DrawImage(pictureBox.Image, new Rectangle(0, 0, width, height),
                //                new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
                //    g.Save();
                //}
                var rect = new Rectangle(a * x + (x % 2) * 90, b * y + (y % 2) * 90, Math.Max(a, b) * 180, Math.Max(a, b) * 180);//x, y, width, height);
                var croppedImage = ((Bitmap)pictureBox.Image)
                    .Clone(rect, pictureBox.Image.PixelFormat);
                //MessageBox.Show((DateTime.Now - t1).TotalSeconds.ToString());
                //pictureBox.Image.Save(@"C:\Users\User\Source\Repos\2022\QRCodeWebcam\QRCodeWebcam2\bin\Debug\QRKepek\2022_src.jpg");

                //croppedImage.Save(@"C:\Users\User\Source\Repos\2022\QRCodeWebcam\QRCodeWebcam2\bin\Debug\QRKepek\2022_tgt.jpg");
                //.Save(@"C:\Users\User\Source\Repos\2022\QRCodeWebcam\QRCodeWebcam2\bin\Debug\QRKepek\2022.jpg");
                // Create a BarcodeReader object and set the options
                //BarcodeReader reader = new BarcodeReader();
                //barcodeReader.Options.TryHarder = true;

                // Decode the QR code in the image
                //return;
                var result = barcodeReader.Decode(croppedImage);
                if (result != null)
                {
                    timer1.Stop();
                    
                    //MessageBox.Show(result.ToString());
                    //txtQRCode.Text = $"{((!string.IsNullOrWhiteSpace(txtQRCode.Text)) ? (txtQRCode.Text + Environment.NewLine) : "")} result: {result.ToString()} {Environment.NewLine} BarcodeFormat: {result.BarcodeFormat} {Environment.NewLine}";
                    //timer1.Stop();
#if DEBUG
                    // if (!System.IO.Directory.Exists("QRKepek"))
                    //   System.IO.Directory.CreateDirectory("QRKepek");
                    // myClone.Save($@"C:\Users\User\Source\Repos\2022\QRCodeWebcam\QRCodeWebcam2\bin\Debug\QRKepek\{sorszam++}.jpg");
#endif
                    try
                    {
                        //var match =
                        /*
                         //        var match = Regex.Match(textBox1.Text, @"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
//        if (match.Success)
                         */
                        Process.Start(result.ToString());
                        Timer t1 = new Timer();
                        //t1.Start();
                        var linkLabel = new LinkLabel()
                        {

                            AutoSize = true,
                        Location = new System.Drawing.Point(3, 0),
                        Name = "linkLabel"+szamlaloLink,
                        
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
                        //flowLayoutPanel1.Invalidate();
                        //listView1.Items.Add("linkLabel1");
                        //listView1.Items.Add("linkLabel2");
                        //listView1.Items.Add("linkLabel3");
                        //listView1.Invalidate();
                        //var stringBuilderShow = new StringBuilder();
                        //var stringBuilderPath = new StringBuilder();
                        ////var links = LinkLocals;
                        //var links = new List<LinkLabel.Link>();
                        //foreach (var link in LinkLocals)
                        //{
                        //    if (stringBuilderShow.Length > 0)
                        //    {
                        //        stringBuilderShow.AppendLine();
                        //        stringBuilderPath.AppendLine();
                        //    }


                        //    // We cannot add the new LinkLabel.Link to the LinkLabel yet because
                        //    // there is no text in the label yet, so the label will complain about
                        //    // the link location being out of range. So we'll temporarily store
                        //    // the links in a collection and add them later.
                        //    links.Add(new LinkLabel.Link(stringBuilderPath.Length+1, link.Path.Length, link.Path));
                        //    stringBuilderShow.Append(link.Show);
                        //    stringBuilderPath.Append(link.Path);
                        //}

                        //var linkLabel = linkLabel1;// new LinkLabel();
                        //// We must set the text before we add the links.
                        //linkLabel.Text = stringBuilderShow.ToString();
                        //foreach (var link in links)
                        //{
                        //    linkLabel.Links.Add(link);
                        //}
                        //linkLabel.AutoSize = true;
                        //t1.Stop();
                        //MessageBox.Show(t1.Interval.ToString() + "ms");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hiba a ({result}) fájl megnyitásakor!\n\n"
                            + ex.Message);
                        //Logger need: ex.Message into a text file for later can debugging
                    }
                    Scan_click(null, null);
                }
            }
        }
       


        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        //        using System.Text.RegularExpressions;

        //public Form1()
        //    {
        //        InitializeComponent();
        //        textBox1.TextChanged += TextBox1_TextChanged;
        //    }

        //    private void TextBox1_TextChanged(object sender, EventArgs e)
        //    {
        //        var match = Regex.Match(textBox1.Text, @"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //        if (match.Success)
        //        {
        //            linkLabel1.Text = match.Value;
        //            linkLabel1.Links.Add(0, match.Value.Length, match.Value);
        //        }
        //        else
        //        {
        //            linkLabel1.Links.Clear();
        //            linkLabel1.Text = "";
        //        }
        //    }


        //private void TxtQRCode_TextChanged(object sender, EventArgs e)
        //{
        //    //Scan_click(null, null);//pictureBox.Image = null;
        //    //timer1.Stop();
        //    //try
        //    //{
        //    //    //if (videoCaptureDevice.IsRunning)
        //    //    //{
        //    //    //    videoCaptureDevice.Stop();
        //    //    //    pictureBox.Image = null;       //btnStart_Click(null, null);
        //    //    //}
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    Program.CameraMistake();
        //    //}
        //}

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

        /*
            private void button1_Click(object sender, EventArgs e)
{
    // Get the dimensions of the scanning square
    int width = 50;
    int height = 50;
    int x = (pictureBox1.Width - width) / 2;
    int y = (pictureBox1.Height - height) / 2;

    // Get the image of the scanning square from the PictureBox
    Bitmap image = new Bitmap(width, height);
    using (Graphics g = Graphics.FromImage(image))
    {
        g.DrawImage(pictureBox1.Image, new Rectangle(0, 0, width, height),
                    new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
    }

    // Create a BarcodeReader object and set the options
    BarcodeReader reader = new BarcodeReader();
    reader.Options.TryHarder = true;

    // Decode the QR code in the image
    var result = reader.Decode(image);
    if (result != null)
    {
        MessageBox.Show("QR code text: " + result.Text);
    }
    else
    {
        MessageBox.Show("No QR code found.");
    }
}

             */
    }
}
