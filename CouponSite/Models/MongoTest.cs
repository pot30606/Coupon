using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace AMZ_Coupon.Models
{
    public class MongoTest
    {
        [XmlIgnore]
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public int age  { get; set; }
        public string Email { get; set; }

    }
}