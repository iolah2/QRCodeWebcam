﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenFileWithQRReader
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }
        private async void PickFile_Clicked(object sender, EventArgs args)
        {
            await PickAndShowFile(null);
        }

        private async void PickImage_Clicked(object sender, EventArgs args)
        {
            string[] fileTypes = null;

            if (Device.RuntimePlatform == Device.Android)
            {
                fileTypes = new string[] { "image/png", "image/jpeg" };
            }

            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    fileTypes = new string[] { "public.image" }; // same as iOS constant UTType.Image
            //}

            //if (Device.RuntimePlatform == Device.UWP)
            //{
            //    fileTypes = new string[] { ".jpg", ".png" };
            //}

            //if (Device.RuntimePlatform == Device.WPF)
            //{
            //    fileTypes = new string[] { "JPEG files (*.jpg)|*.jpg", "PNG files (*.png)|*.png" };
            //}

            await PickAndShowFile(fileTypes);
        }

        private async Task PickAndShowFile(string[] fileTypes)
        {
            try
            {
                var pickedFile = await CrossFilePicker.Current.PickFile(fileTypes);

                if (pickedFile != null)
                {
                    FileNameLabel.Text = pickedFile.FileName;
                    FilePathLabel.Text = pickedFile.FilePath;

                    if (pickedFile.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase)
                        || pickedFile.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        FileImagePreview.Source = ImageSource.FromStream(() => pickedFile.GetStream());
                        FileImagePreview.IsVisible = true;
                    }
                    else
                    {
                        FileImagePreview.IsVisible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                FileNameLabel.Text = ex.ToString();
                FilePathLabel.Text = string.Empty;
                FileImagePreview.IsVisible = false;
            }
        }
    }
}