using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateGUI
{
    internal class Seller
    {
        public Seller(int sellerId, string sellerName, string sellerPhone)
        {
            SellerId = sellerId;
            SellerName = sellerName;
            SellerPhone = sellerPhone;
        }

        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public string SellerPhone { get; set; }
    }
}
