﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMZ_Coupon.Models;
using AMZ_Coupon.Utility;
using CouponSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CouponSite.Controllers
{
    public class BackendController : Controller
    {
        [Route("Backend/Backend")]
        public ActionResult Backend()
        {
            return View();
        }
        // Backend/api/InsertCouponDetail
        //[Route("Backend/api/InsertCouponDetail")]
        // GET api/<controller>/products
        [HttpPost]
        public bool InsertCouponDetail(CouponSite.Models.Product products)
        {
            var x = DateTime.Now;
            var n = CouponDB.InsertCouponDetail(products);
            return n;
        }
    }
}