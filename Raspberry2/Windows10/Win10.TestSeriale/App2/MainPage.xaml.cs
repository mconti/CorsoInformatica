using System;
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

using Windows.Devices.SerialCommunication;
using Windows.Devices.Enumeration;
using System.Threading.Tasks;
using Windows.Storage.Streams;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410


// Esempio di utilizzo di COM preso da
// https://www.hackster.io/team-falafel-software/zigbee-communication-with-the-pi-2-and-windows-iot-core


namespace Win10TestSeriale
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //using Windows.Devices.SerialCommunication
        SerialDevice port;

        DataReader _dataReader;
        DataWriter _dataWriter;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void blueButton_Click(object sender, RoutedEventArgs e)
        {
            var writeResult = await Write();
            txtRisposta.Text = writeResult.TextResult;
        }
        private async void greenButton_Click(object sender, RoutedEventArgs e)
        {
            var readResult = await Read();
            txtRisposta.Text = readResult.TextResult;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Il sistema ritorna l'elenco delle seriali disponibili

            //using Windows.Devices.SerialCommunication
            string deviceQuery = SerialDevice.GetDeviceSelector();

            //using Windows.Devices.Enumeration
            var deviceInfo = await DeviceInformation.FindAllAsync(deviceQuery);


            if (deviceInfo != null && deviceInfo.Count > 0)
            {
                var elenco = from d in deviceInfo
                             select new {
                                 Id = d.Id,
                                 miniId = d.Id.ToString().Substring(0, 20) + " ... ",
                                 Name = d.Name
                             };

                elencoSeriali.ItemsSource = elenco;
                elencoSeriali.SelectedValuePath = "Id";
            }

        }

        private async void elencoSeriali_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (elencoSeriali.SelectedValue != null)
            {
                string deviceId = elencoSeriali.SelectedValue.ToString();
                port = await SerialDevice.FromIdAsync(deviceId);
  
                List<SerialDevice> l = new List<SerialDevice>();
                l.Add(port);

                elencoAttributi.ItemsSource = l;

                // Configure serial settings
                port.WriteTimeout = TimeSpan.FromMilliseconds(1000);
                port.ReadTimeout = TimeSpan.FromMilliseconds(1000);
                port.BaudRate = 9600;
                port.Parity = SerialParity.None;
                port.StopBits = SerialStopBitCount.One;
                port.DataBits = 8;

                blueButton.IsEnabled = true;
                greenButton.IsEnabled = true;
            }
        }

        //using System.Threading.Tasks
        private async Task<CommResult> Read()
        {
            var retvalue = new CommResult();
            try
            {
                // using Windows.Storage.Streams;
                _dataReader = new DataReader(port.InputStream);
                var numBytesRecvd = await _dataReader.LoadAsync(1024);
                retvalue.IsSuccessful = true;
                if (numBytesRecvd > 0)
                {
                    retvalue.TextResult = _dataReader.ReadString(numBytesRecvd).Trim();
                }
            }
            catch (Exception ex)
            {
                retvalue.IsSuccessful = false;
                retvalue.TextResult = ex.Message;
            }
            finally
            {
                if (_dataReader != null)
                {
                    _dataReader.DetachStream();
                    _dataReader = null;
                }
            }
            return retvalue;
        }
        internal class CommResult
        {
            public bool IsSuccessful { get; set; }
            public string TextResult { get; set; }

            public CommResult()
            {
                this.IsSuccessful = false;
                this.TextResult = "";
            }
        }
        private async Task<CommResult> Write()
        {
            CommResult retvalue = new CommResult();
            try
            {
                _dataWriter = new DataWriter(port.OutputStream);
                
                //send the message
                var numBytesWritten = _dataWriter.WriteString(edtMessaggio.Text);
                await _dataWriter.StoreAsync();
                retvalue.IsSuccessful = true;
                retvalue.TextResult = "Text has been successfully sent";
            }
            catch (Exception ex)
            {
                retvalue.IsSuccessful = false;
                retvalue.TextResult = ex.Message;
            }
            finally
            {
                if (_dataWriter != null)
                {
                    _dataWriter.DetachStream();
                    _dataWriter = null;
                }
            }
            return retvalue;
        }

    }
}
