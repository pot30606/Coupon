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
        public JsonResult ReceiveCoupon(ReceiveCoupon receiveInfo)
        {
            bool result = CouponDB.ReceiveCoupon(receiveInfo);
            var x = result;
            return Json(new { Success = true, Message = "Success" });
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
