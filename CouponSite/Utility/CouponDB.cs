using CouponSite.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
        private static IMongoDatabase mongoDatabase;

        public static IMongoDatabase GetMongoDatabase()
        {
            var client = new MongoClient("mongodb+srv://Terry:ji2bvPDHJz5jLe21@clusteramzcoupon-g3tzk.mongodb.net/test?retryWrites=true");
            return client.GetDatabase("AMZCouponDB");
        }

       
        public static List<Product> GetData()
        {
            //Get the database connection
            mongoDatabase = GetMongoDatabase();
            //fetch the details from CustomerDB and pass into view
            var result = mongoDatabase.GetCollection<Product>("AMZCoupon").Find(FilterDefinition<Product>.Empty).ToList();
            return result;
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
