﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRCodeWebcam2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new FormTerv());//Terv());
            }
            catch (Exception)
            {
                CameraMistake();
            }
        }

        public static void CameraMistake()
        {
            MessageBox.Show("Kérem csatlakoztassa a kamerát!\nA kódolvasó leáll!");
            Application.Exit();
        }
    }
}
