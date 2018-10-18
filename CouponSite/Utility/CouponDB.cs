using CouponSite.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMZ_Coupon.Utility
{


    public class CouponDB
    {
        private static IMongoDatabase db;



        public static IMongoDatabase GetMongoDatabase()
        {
            var client = new MongoClient("mongodb+srv://Terry:VBCrzhDOc7ZWNAmC@clusteramzcoupon-g3tzk.mongodb.net/test?retryWrites=true");
            return client.GetDatabase("AMZCouponDB");
        }


        public static List<Product> GetData()
        {
            //Get the database connection
            db = GetMongoDatabase();
            //fetch the details from CustomerDB and pass into view
            var result = db.GetCollection<Product>("AMZCoupon").Find(FilterDefinition<Product>.Empty).ToList();
            return result;
        }

        public static bool InsertCouponDetail(Product product)
        {
            db = GetMongoDatabase();
            var result = true;
            product = CreateProductDetailInAMZCoupon(product);

            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static Product CreateProductDetailInAMZCoupon(Product product)
        {
            var collection = db.GetCollection<BsonDocument>("AMZCoupon");
            product.StarID = ObjectId.GenerateNewId();
            product.PictureID = ObjectId.GenerateNewId();
            product.VideoID = ObjectId.GenerateNewId();
            product.CommandID = ObjectId.GenerateNewId();
            var Coupons = new BsonArray();
            string[] CouponArray = product.PCoupon.Split('\n');
            foreach (var item in CouponArray)
            {
                Coupons.Add(new BsonDocument { { "Used", "n" }, { "Coupon", item } });
            }

            var document = new BsonDocument
            {
                {"ProductName", product.ProductName},
                {"Price",  product.Price },
                {"StarID",   product.StarID},
                {"PictureID",  product.PictureID },
                {"VideoID",  product.VideoID },
                {"CommandID",  product.CommandID },
                {"Coupons",  Coupons },
                {"Shelf",  product.Shelf },
                {"Valid",  product.Valid },
                {"AddedTime",  DateTime.Now },
                {"OwnerID",  "testOwner123" },
                {"Discount",  product.Discount },
                {"StartTime",  product.StartTime },
                {"EndTime",  product.EndTime }

            };
            try
            {
                collection.InsertOne(document);
                return product;
            }
            catch
            {
                return null;
            }
        }
       

        public static Product GetSingleProductDetail(string id)
        {
            ObjectId _id = ObjectId.Parse(id);
            db = GetMongoDatabase();
            var result = new Product();
            var collection_AMZCoupon = db.GetCollection<Product>("AMZCoupon");

            //var filterold = Builders<Product>.Filter.AnyEq("Coupons", new BsonDocument { { "Used", "n" } , { "Coupon", "xxx-xxx-xxx" } });
            var builder = Builders<Product>.Filter;
            var filter = builder.And(builder.Eq("Coupons.Used", "n"));
            var query = collection_AMZCoupon.Find(filter).ToList();
            if(query.Count() < 1)
            {
                return null;
            }
            //get Coupon
            result = query.FirstOrDefault();
            var couponArray = result.Coupons.FirstOrDefault();
            var xx = (BsonDocument)couponArray;

            result.PCoupon = xx["Coupon"].ToString();
            return result;

        }
    }
}
