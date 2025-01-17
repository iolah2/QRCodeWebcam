﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace OpenFileWithQRReader.Droid
{
    [Activity(Label = "OpenFileWithQRReader", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //https://www.c-sharpcorner.com/blogs/using-zxing-code-128-scanner-in-xamarin-forms2
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        //[Obsolete]
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {           
            //ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}