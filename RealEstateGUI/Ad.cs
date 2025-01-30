using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateGUI
{
    public class Ad
    {
        public int Area { get; set; }
        public Category Category { get; set; }
        public DateTime CreateAt { get; set; }
        public string Description { get; set; }
        public int Floors { get; set; }
        public bool FreeOfCharge { get; set; }
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string LatLong { get; set; }
        public int Rooms { get; set; }
        public Seller Seller { get; set; }

        public double LatX => double.Parse(LatLong.Split(",")[0].Replace(".", ","));
        public double LatY => double.Parse(LatLong.Split(",")[1].Replace(".", ","));



        public Ad(string sor)
        {
            var x = sor.Split(";");

            Id = int.Parse(x[0]);
            Rooms = int.Parse(x[1]);
            LatLong = x[2];
            Floors = int.Parse(x[3]);
            Area = int.Parse(x[4]);
            Description = x[5];
            FreeOfCharge = x[6] == "1" ? true : false;
            ImageUrl = x[7];
            CreateAt = DateTime.Parse(x[8]);
            Seller = new Seller(int.Parse(x[9]), x[10], x[11]);
            Category = new Category(int.Parse(x[12]), x[13]);
        }
    }
}
