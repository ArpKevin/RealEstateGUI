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
        public string? Description { get; set; }
        public bool FreeOfCharge { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreateAt { get; set; }
        public Seller Seller { get; set; }
        public Category Category { get; set; }
        public double SzelessegiFok => double.Parse(Latlong.Split(",")[0].Replace(".", ","));
        public double HosszusagiFok => double.Parse(Latlong.Split(",")[1].Replace(".", ","));

        public Ad(int id, int rooms, string latlong, int floors, int area, string description, bool freeOfCharge, string imageUrl, DateTime createAt, Seller seller, Category category)
        {
            Id = id;
            Rooms = rooms;
            Latlong = latlong;
            Floors = floors;
            Area = area;
            Description = description;
            FreeOfCharge = freeOfCharge;
            ImageUrl = imageUrl;
            CreateAt = createAt;
            Seller = seller;
            Category = category;
        }
    }
}
