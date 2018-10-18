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
        //product
        public ObjectId _id { get; set; }
        public string ProductName { get; set; }
        public string Valid { get; set; }
        public string Product_Valid { get; set; }
        public Decimal128 Price { get; set; }
        public string Shelf { get; set; }
        public string OwnerID { get; set; }
        public ObjectId PictureID { get; set; }
        public ObjectId VideoID { get; set; }
        public ObjectId CommandID { get; set; }
        public ObjectId StarID { get; set; }
        public Array Coupons { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }


        public string PCoupon { get; set; }
    }



    public class CouponList
    {
        public string Coupon { get; set; }
        public string Used { get; set; }
    }






}
