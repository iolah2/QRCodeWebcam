using OpenFileWithQRReader.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace OpenFileWithQRReader
{
    //https://stackoverflow.com/questions/69754142/xamarin-android-camera-permissions-with-zxing-net-mobile-only-works-after-app-re
    public partial class MainPage: ContentPage //: ZXingScannerView //
                                               //ContentPage, INotifyPropertyChanged
    {
        //private bool _isScanning;

        //public bool IsScanning
        //{
        //    get { return _isScanning; }
        //    set { _isScanning = value; }
        //}

        public MainPage()
        {
            InitializeComponent();
            //IsScanning = false;
            //BindingContext = this;
            //Indicator.Start();
        }

        //https://stackoverflow.com/questions/54750040/how-to-restart-the-camera-in-zxing-after-it-finds-the-qr-code?rq=1

        //public void startScan()
        //{ //use this when you want to resume the camera
        //    if (scannerView != null)
        //    {
        //        scannerView.setResultHandler(this);
        //        scannerView.startCamera();
        //        rescan();
        //    }
        //}

        //public void stopScan()
        //{ //use this when you want to stop scanning
        //  // it is very important to do that,
        //  // because the camera will keep scanning codes in background
        //    if (scannerView != null)
        //    {
        //        scannerView.stopCameraPreview();
        //        scannerView.stopCamera();
        //    }
        //}

        //public void rescan()
        //{
        //    if (scannerView != null)
        //    {
        //        scannerView.resumeCameraPreview(this);
        //    }
        //}

        private async void btnScan_Clicked(object sender, EventArgs e)
        {
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    txtBarcode.Text = result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
