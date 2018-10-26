﻿using CouponSite.Models;
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

        public static bool CheckLogin(_Member member)
        {
            db = GetMongoDatabase();
            var builder = Builders<_Member>.Filter;
            var memberCollection = db.GetCollection<_Member>("Member");
            var filter = builder.And(builder.Eq("Account", member.Account), builder.Eq("Password", member.Password));
            var query = memberCollection.Find(filter).ToList();

            if(query.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public static _Member RegistAccount(_Member member)
        {
            db = GetMongoDatabase();
            member.MemberID = ObjectId.GenerateNewId();
            var document = new BsonDocument
            {
                { "Account" , member.Account},
                { "Password" , member.Password},
                { "FBID" , ""},
                { "GID" , ""},
                { "IsWhiteList" , "y"},
                { "RegistDate" , DateTime.Now}
            };
            var memberCollection = db.GetCollection<BsonDocument>("Member");
            try
            {
                memberCollection.InsertOne(document);
                return member;
            }
            catch
            {
                return null;
            }

        }

        public static Product ReceiveCoupon(ReceiveCoupon receiveInfo)
        {
            db = GetMongoDatabase();
            string UnUsedCoupon = null;
            var collection_AMZCoupon = db.GetCollection<Product>("AMZCoupon");
            var _id = ObjectId.Parse(receiveInfo.ProductID);

            var builder = Builders<Product>.Filter;
            var filter = builder.And(builder.Eq("_id", _id));
            var query = collection_AMZCoupon.Find(filter).ToList().FirstOrDefault();
            foreach (var item in query.Coupons)
            {
                if (item["Used"] == "n")
                {
                    UnUsedCoupon = item["Coupon"].ToString();
                    item["Used"] = "y";
                    break;
                }
            }
            if(UnUsedCoupon == null)
            {
                return null;
            }
            var update = Builders<Product>.Update.Set("Coupons", query.Coupons);
            var updateResult = collection_AMZCoupon.UpdateOne(filter, update);

            //Insert User Info (Email and Name)
            var collectionMember = db.GetCollection<BsonDocument>("User");
            var document = new BsonDocument
            {
                {"Name", receiveInfo.Name},
                {"Email",  receiveInfo.Email }

            };
            collectionMember.InsertOne(document);

            query.Coupons = null;
            query.PCoupon = UnUsedCoupon;

            return query;
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
            var result = new Product();
            //string singleCoupon = null;
            ObjectId _id = ObjectId.Parse(id);
            //find Product By Id
            List<Product> queryResult =  findProductDetailById(_id);
            if (queryResult.Count() < 1)
            {
                return null;
            }

            //Get Coupon
            bool IsCouponExist = false;
            result = queryResult.FirstOrDefault();
            var couponArray = result.Coupons;
            foreach (var item in couponArray)
            {
                if (item["Used"].ToString() == "n")
                {
                    IsCouponExist = true;
                    //singleCoupon = item["Coupon"].ToString();
                    break;
                }
            }
            if(IsCouponExist == false)
            {
                return null;
            }
            result.Coupons = null;
            //result.PCoupon = singleCoupon;
            return result;

        }

        private static List<Product> findProductDetailById(ObjectId _id)
        {
            db = GetMongoDatabase();
            var collection_AMZCoupon = db.GetCollection<Product>("AMZCoupon");
            var builder = Builders<Product>.Filter;
            var filter = builder.And(builder.Eq("_id", _id));
            var query = collection_AMZCoupon.Find(filter).ToList();
            return query;
        }
    }
}
