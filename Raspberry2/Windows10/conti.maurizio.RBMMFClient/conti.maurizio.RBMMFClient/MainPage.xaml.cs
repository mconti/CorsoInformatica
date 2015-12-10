using System;
using PubNubMessaging.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Newtonsoft.Json;
using System.Collections.ObjectModel;


// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace conti.maurizio.RBMMFClient
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Pubnub pubnub;
        ObservableCollection<Campione> Campioni;

        public MainPage()
        {
            this.InitializeComponent();
            pubnub = new Pubnub("pub-c-65914541-3bbd-4fa9-979d-ffe4b018be8f", "sub-c-12fa7c6c-8534-11e5-83e3-02ee2ddab7fe");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Campioni = new ObservableCollection<Campione>();
            lvData.ItemsSource = Campioni;
            pubnub.Subscribe<string>("Canale1", userCallBack, connectCallback, errorCallback);
        }

        private void connectCallback(string obj)
        {
            //Dispatcher.RunAsync(
            //    CoreDispatcherPriority.Normal,
            //    () =>
            //    {
            //        lvLog.Dispatcher.RunAsync(
            //            CoreDispatcherPriority.Normal,
            //            () => lvLog.Items.Add("Connesso: " + obj));
            //    }
            //);
        }

        private void errorCallback(PubnubClientError obj)
        {
            //Dispatcher.RunAsync(
            //    CoreDispatcherPriority.Normal,
            //    () =>
            //    {
            //        lvLog.Dispatcher.RunAsync(
            //            CoreDispatcherPriority.Normal,
            //            () => lvLog.Items.Add("Erore: " + obj.Message));
            //    }
            //);
        }

        private async void userCallBack(string obj)
        {
            try
            {

                await Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal,
                    () =>
                    {
                        List<object> deserializedMessage = pubnub.JsonPluggableLibrary.DeserializeToListOfObject(obj);
                        EON eon = JsonConvert.DeserializeObject<EON>(deserializedMessage[0].ToString());
                        Campioni.Add(eon.eon);
                    }
                );
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pubnub.EnableJsonEncodingForPublish = true;

            int temperatura, luminosita, rumore;
            int.TryParse(edtTemperatura.Text, out temperatura);
            int.TryParse(edtLuminosita.Text, out luminosita);
            int.TryParse(edtRumore.Text, out rumore);

            EON e1 = new EON();
            Campione c = new Campione { Luminosita = luminosita, Rumore = rumore, Temperatura = temperatura };
            e1.eon = c;

            pubnub.Publish("Canale1", e1, userPublish, userPubError);
        }

        private void userPublish(object obj)
        {
        }

        private void userPubError(PubnubClientError obj)
        {
        }
    }

    /*
                // Event Handler user
                async (string obj) =>
                    await Dispatcher.RunAsync(
                        CoreDispatcherPriority.Normal,
                        () => {
                            List<object> deserializedMessage = pubnub.JsonPluggableLibrary.DeserializeToListOfObject(obj);
                            //string serializedResultMessage = pubnub.JsonPluggableLibrary.SerializeToJsonString(deserializedMessage[0]);
                            Campione campione = JsonConvert.DeserializeObject<Campione>(deserializedMessage[0].ToString());
                            Campioni.Add(campione);
                        }
                    ),

                // Event Handler connection
                async (string obj) =>
                    await lvLog.Dispatcher.RunAsync(
                        CoreDispatcherPriority.Normal,
                        () => lvLog.Items.Add("Connesso: " + obj)
                    ),

                // Event Handler error
                async (PubnubClientError obj) =>
                    await lvLog.Dispatcher.RunAsync(
                        CoreDispatcherPriority.Normal,
                        () => lvLog.Items.Add("Erore: " + obj.Message)
                    )

    */


    public class Campione
    {
        public double Temperatura { get; set; }
        public double Luminosita { get; set; }
        public double Rumore { get; set; }
    }

    public class EON
    {
        public Campione eon { get; set; }
    }
}
