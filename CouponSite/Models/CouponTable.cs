using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace AMZ_Coupon.Models
{
    public class CouponTable
    {
        [XmlIgnore]
        public ObjectId _id { get; set; }
        public string Coupon { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Valid { get; set; }
    }
}