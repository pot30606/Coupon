using CouponSite.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
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
        /*
        private static bool CreateCouponDetailInCoupon(Product product)
        {
            var collection = db.GetCollection<BsonDocument>("Coupon");
            try
            {
                string[] CouponArray = product.PCoupon.Split('\n');
                foreach (string coupon in CouponArray)
                {
                    var document = new BsonDocument
                    {
                        {"Coupon",  coupon },
                        {"StartTime",  product.StartTime },
                        {"EndTime",  product.EndTime },
                        {"Discount",  product.Discount },
                        {"Valid",  product.Valid },
                        {"CouponID",  product.CouponID }
                    };
                    collection.InsertOne(document);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        */


        public static Product GetSingleProductDetail(string id)
        {
            ObjectId _id = ObjectId.Parse(id);
            var result = new Product();
            db = GetMongoDatabase();

            var query_AMZCoupon = db.GetCollection<Product>("AMZCoupon");

            return null;


        }
    }
}

/*

var db = GetDbInstance();
            var Plist = new List<Product>();

            var Coupons = product.PCoupon.Split('\n');
            for(int i =0; i< Coupons.Length; i++)
            {
                product.ProductID = DateTime.Now.ToString("yyyyMMddhhmmss") +  i;
                product.PCoupon = Coupons[i];
                Plist.Add(product);
            }
            db.Product.InsertAllOnSubmit(Plist);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception error)
            {
                return false;
            }

            return true;
 * */
