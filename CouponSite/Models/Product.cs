using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CouponSite.Models
{
    public class Product
    {
        [XmlIgnore]
        public ObjectId _id { get; set; }
        public string PCoupon { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Shelf { get; set; }
        public string Valid { get; set; }
    }
}
