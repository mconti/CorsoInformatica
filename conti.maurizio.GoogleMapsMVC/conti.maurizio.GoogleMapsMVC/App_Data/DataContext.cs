using System;
using System.Collections.Generic;

namespace conti.maurizio.GoogleMapsMVC.Controllers
{
    public class RegionInfo
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Title { get; set; }
        public int zIndex { get; set; }
        public string ImagePath { get; set; }
        public string InfoWindowContent { get; set; }
        public double population { get; set; }
    }

    public class DataContext
    {

        public static IEnumerable<RegionInfo> GetRegions()
        {
            return new List<RegionInfo>
                       {
                           new RegionInfo
                               {
                                   Latitude =  44.029459156425688,
                                   Longitude = 12.46536634471437,
                                   Title = "Casa Mia",
                                   zIndex = 15,
                                   ImagePath = "Andalucia.jpg",
                                   InfoWindowContent = @"<h2>Andalucia</h2>",
                                   population = 8370975
                               },
                           new RegionInfo
                               {
                                   Latitude =  44.02955,
                                   Longitude = 12.46530,
                                   Title = "Casa Mia2",
                                   zIndex = 15,
                                   ImagePath = "Andalucia.jpg",
                                   InfoWindowContent = @"<h2>Andalucia</h2>",
                                   population = 8370975
                               },
                       };
        }
    }
}
