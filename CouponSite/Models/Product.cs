using Microsoft.AspNetCore.Http;
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
        public decimal Price { get; set; }
        public string Shelf { get; set; }
        public ObjectId OwnerID { get; set; }
        public string Picture { get; set; }
        public ObjectId VideoID { get; set; }
        public ObjectId CommandID { get; set; }
        public ObjectId StarID { get; set; }
        public BsonArray Coupons { get; set; } //BsonArray
        public decimal Discount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime AddedTime { get; set; }

        public string PCoupon { get; set; }
    }

    public class InsertProduct
    {
        [XmlIgnore]
        //product
        public ObjectId _id { get; set; }
        public string ProductName { get; set; }
        public string Valid { get; set; }
        public string Product_Valid { get; set; }
        public decimal Price { get; set; }
        public string Shelf { get; set; }
        public string Account { get; set; }
        public ObjectId OwnerID { get; set; }
        public ObjectId PictureID { get; set; }
        public ObjectId VideoID { get; set; }
        public ObjectId CommandID { get; set; }
        public ObjectId StarID { get; set; }
        public string Coupons { get; set; } //BsonArray
        public decimal Discount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime AddedTime { get; set; }

        public IFormFile Picture { get; set; }

        public string PCoupon { get; set; }
    }

    public class Coupons
    {
        public string Coupon { get; set; }
        public string Used { get; set; }

    }

    public class ReceiveCoupon
    {
        public string PCoupon { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ProductID { get; set; }

    }

    public class User
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class _Member {
        public ObjectId _id { get; set; }
        public ObjectId MemberID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string FBID { get; set; }
        public string GID { get; set; }
        public string IsWhiteList { get; set; }
        public DateTime RegistDate { get; set; }
    }


}
