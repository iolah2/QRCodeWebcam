﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenFileWithQRReader
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Page1();//new NavigationPage(new ScanBarCode());//ScanBarCode();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
