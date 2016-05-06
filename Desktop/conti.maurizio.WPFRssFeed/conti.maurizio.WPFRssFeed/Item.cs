using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace conti.maurizio.WPFRssFeed
{
    public class Item
    {
        public static int id { get; set; }
        public string Titolo { get; set; }
        public BitmapImage Immagine { get; set; }
        public string MioCampo { get; set; }

        public Item(XElement item)
        {
            id++;
            MioCampo = "Ciao";

            try { Titolo = item.Element("title").Value; } catch { }

            try
            {
                string urlImmagine = item.Element("enclosure").Attribute("url").Value;
                Immagine = new BitmapImage(new Uri(urlImmagine));
            }
            catch { }
        }
    }

    public class Items : ObservableCollection<Item>
    {
        public int id { get; set; }
        public string Url { get; set; }

        public Items( string url )
        {
            Url = url;
            XElement e = XElement.Load(Url);

            foreach( XElement elemento in e.Element("channel").Elements("item") )
            {
                this.Add(new Item(elemento));
            }
        }
    }

    public class Sorgente
    {
        public int id { get; set; }
        public string Url { get; set; }
        public Items Items { get; set; }

        public Sorgente() { }
        public Sorgente(string url)
        {
            Url = url;
            Items = new Items( url );
        }

    }
}
