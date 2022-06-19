using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM2E16778.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mp2 : ContentPage
    {
        public mp2()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            String latitudGuardada = lblLat.Text;
            String longitudGuardada = lblLong.Text;
            String descripcionGuardada = lblDesc.Text;
            base.OnAppearing();
            Pin ubicacion = new Pin();
            ubicacion.Label = "Tu destino";
            ubicacion.Address = descripcionGuardada;
            ubicacion.Position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(latitudGuardada), Convert.ToDouble(longitudGuardada));
            Mapa.Pins.Add(ubicacion);

            Mapa.MoveToRegion(new MapSpan(new Xamarin.Forms.Maps.Position(Convert.ToDouble(latitudGuardada), Convert.ToDouble(longitudGuardada)), 1, 1));

            var localizacion = CrossGeolocator.Current;
            if (localizacion != null)
            {
                localizacion.PositionChanged += localizacion_positionChanged;

                if (!localizacion.IsListening)
                {
                    await localizacion.StartListeningAsync(TimeSpan.FromSeconds(10), 100);
                }

            }
        }

        private void localizacion_positionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var posicion_mapa = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude);
            Mapa.MoveToRegion(new MapSpan(posicion_mapa, 1, 1));
        }
        public async Task CaptureScreenshot()
        {

            Byte[] imgByteArray;
            var fn = "IM1MAP.jpg";
            var file = Path.Combine(FileSystem.CacheDirectory, fn);

            var screenshot = await Screenshot.CaptureAsync();
            var stream = await screenshot.OpenReadAsync();
            var Image = ImageSource.FromStream(() => stream);

            StreamImageSource streamImageSource = (StreamImageSource)Image;
            System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream2 = task.Result;

            imgByteArray = ReadFully(stream2);
            File.WriteAllBytes(file, imgByteArray);

            await Share.RequestAsync(new ShareFileRequest { Title = Title, File = new ShareFile(file) });
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }

        }
        private void btnshare_Clicked(object sender, EventArgs e)
        {
            CaptureScreenshot();
        }

    }

}
