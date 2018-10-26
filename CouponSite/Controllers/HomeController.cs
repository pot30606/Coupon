using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CouponSite.Models;
using AMZ_Coupon.Utility;
using MongoDB.Bson;

namespace CouponSite.Controllers
{
    
    public class HomeController : Controller
    {
        [Route("Home/CouponSite/{id}")]
        public IActionResult CouponSite(string id)
        {
            var result = new Product();
            if (id != null)
            {
                result = CouponDB.GetSingleProductDetail(id);
            }
            return View(result);
        }



        [Route("api/ReceiveCoupon")]
        [HttpPost]
        public Product ReceiveCoupon(ReceiveCoupon receiveInfo)
        {
            var result = CouponDB.ReceiveCoupon(receiveInfo);
            if (result != null)
            {
                return result;
            }
            return null;
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
