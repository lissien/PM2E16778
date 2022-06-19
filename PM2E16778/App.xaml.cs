using PM2E16778.Controllers;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E16778
{
    public partial class App : Application
    {
        static UbicacionesDB basedatos;

        public static UbicacionesDB BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new UbicacionesDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UbicacionesDB.db3"));
                }

                return basedatos;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.MainPage());
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