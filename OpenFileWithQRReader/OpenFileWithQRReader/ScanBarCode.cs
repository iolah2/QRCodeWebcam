using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace OpenFileWithQRReader
{
    public class ScanBarCode : ContentPage
    {
        StackLayout stackMainlayout;
        public ScanBarCode()
        {
            stackMainlayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
            };
            ZXingScannerPage scanPage;
            Button btnScan = new Button
            {
                Text = "Scan",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            btnScan.Clicked += async (a, s) =>
            {
                scanPage = new ZXingScannerPage() { IsScanning = true, IsAnalyzing = true};
                try
                {
                    
                }
                catch (Exception)
                {

                    throw;
                }
                // Use current Navigation directly    
                //await Navigation.PushAsync(scanPage);
                //https://social.msdn.microsoft.com/Forums/en-US/e61054c5-e2d7-4468-a900-532aca3d1a1b/zxingscannerpage-is-not-opening-to-read-qr-code?forum=xamarinios
                scanPage.OnScanResult += (result) =>
                {
                    scanPage.IsScanning = false;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Navigation.PopModalAsync();
                        await DisplayAlert("Scanned Barcode", result.Text + " , " + result.BarcodeFormat + " ," + result.ResultPoints[0].ToString(), "OK");
                    });
                };
                await Navigation.PushModalAsync(scanPage);
            };
            stackMainlayout.Children.Add(btnScan);
            Content = stackMainlayout;
        }
    }
}