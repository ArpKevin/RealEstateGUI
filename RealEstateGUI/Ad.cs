using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateGUI
{
    internal class Ad
    {
        public int Id { get; set; }
        public int Rooms { get; set; }
        public string Latlong { get; set; }
        public int Floors { get; set; }
        public int Area { get; set; }
        public string Description { get; set; }
        public bool FreeOfCharge { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateAt { get; set; }
        public Seller Seller { get; set; }
        public Category Category { get; set; }
        public double SzelessegiFok => double.Parse(Latlong.Split(",")[0].Replace(".", ","));
        public double HosszusagiFok => double.Parse(Latlong.Split(",")[1].Replace(".", ","));

        public Ad(string sor)
        {
            var x = sor.Split(";");
            Id = int.Parse(x[0]);
            Rooms = int.Parse(x[1]);
            Latlong = x[2];
            Floors = int.Parse(x[3]);
            Area = int.Parse(x[4]);
            Description = x[5];
            FreeOfCharge = x[6] == "1";
            ImageUrl = x[7];
            CreateAt = DateTime.Parse(x[8]);
            Seller = new Seller(int.Parse(x[9]), x[10], x[11]);
            Category = new Category(int.Parse(x[12]), x[13]);
        }
        //public static List<Ad> LoadFromCsv(string filePath)
        //{
        //    List<Ad> ads = new();
        //    foreach (var item in File.ReadAllLines(filePath).Skip(1))
        //    {
        //        ads.Add(new(item));
        //    }

        //    return ads;
        //}
    }
}
