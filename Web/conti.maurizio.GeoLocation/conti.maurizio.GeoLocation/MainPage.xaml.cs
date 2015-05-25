using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=391641

namespace conti.maurizio.GeoLocation
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Questo sorgente è preso da qui
        // https://msdn.microsoft.com/en-us/library/windows/apps/xaml/windows.devices.geolocation.geolocator.aspx

        private Geolocator geolocator = null;
        public ObservableCollection<Geoposition> Punti;

        public MainPage()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Punti = new ObservableCollection<Geoposition>();
            DataContext = Punti;

            geolocator = new Geolocator();
            geolocator.MovementThreshold = 1.0;
            geolocator.PositionChanged += geolocator_PositionChanged;
        }

        async void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            if (Punti != null)
            {
                Geoposition pos = await geolocator.GetGeopositionAsync();
                if (Dispatcher != null)
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                    {
                        // LatLong di Via Busca
                        // 44.029459156425688
                        // 12.46536634471437
                        //
                        // @44.029459156425688,12.46536634471437
                        // location=44.029459156425688,12.46536634471437
                        //
                        // @(Html.GoogleMap().Name("map")
                        //        .Latitude(44.029459156425688)
                        //        .Longitude(12.46536634471437))

                        // Immagini statiche
                        //
                        // Mappa
                        // https://maps.googleapis.com/maps/api/staticmap?center=44.029459156425688,12.46536634471437&zoom=16&size=600x600
                        //
                        // StreetView (heading va da 0 a 359 ed è la rotazione della fotocamera)
                        // Casa https://maps.googleapis.com/maps/api/streetview?size=600x600&location=44.029459156425688,12.46536634471437&heading=100
                        // https://maps.googleapis.com/maps/api/streetview?size=600x600&location=44.0469,12.5854&heading=150
                        //
                        // Immagini interattive
                        //
                        // Serve un PlaceID. Si usa una API
                        // https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=44.029459156425688,12.46536634471437&radius=1&key=AIzaSyAIyD5UaDi7d01R3wUOrOt-PU5jhOSwsXU
                        //
                        // Esempio: placeID di Via Busca  
                        // ChIJs6oeZDzALBMR7CwsLu0PI9I
                        //
                        // Servizio Place Details
                        // https://maps.googleapis.com/maps/api/place/details/json?placeid=ChIJs6oeZDzALBMR7CwsLu0PI9I&key=AIzaSyAIyD5UaDi7d01R3wUOrOt-PU5jhOSwsXU
                        //
                        // Esempio di mappa strorica con PNG
                        // http://www.jmelosegui.com/map/MapType/ImageMapType
                        //



                        Punti.Add(pos);
                    });
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private string GetStatusString(PositionStatus status)
        {
            var strStatus = "";

            switch (status)
            {
                case PositionStatus.Ready:
                    strStatus = "Location is available.";
                    break;

                case PositionStatus.Initializing:
                    strStatus = "Geolocation service is initializing.";
                    break;

                case PositionStatus.NoData:
                    strStatus = "Location service data is not available.";
                    break;

                case PositionStatus.Disabled:
                    strStatus = "Location services are disabled. Use the " +
                                "Settings charm to enable them.";
                    break;

                case PositionStatus.NotInitialized:
                    strStatus = "Location status is not initialized because " +
                                "the app has not yet requested location data.";
                    break;

                case PositionStatus.NotAvailable:
                    strStatus = "Location services are not supported on your system.";
                    break;

                default:
                    strStatus = "Unknown PositionStatus value.";
                    break;
            }

            return (strStatus);

        }


    }
}
