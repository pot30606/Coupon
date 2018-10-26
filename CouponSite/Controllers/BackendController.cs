using System;
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

        [Route("Backend/Login")]
        [HttpPost]
        public bool Login(_Member member) 
        {
            var status = CouponDB.CheckLogin(member);
            if(status == true)
            {
                return status;
            }
            return status;
        }

        [Route("Backend/Register")]
        [HttpPost]
        public string Register(_Member member)
        {
            var status = CouponDB.RegistAccount(member);
            if (status != null)
            {
                return status.Account;
            }
            return null;
        }

        [Route("Backend/Checklogin")]
        [HttpPost]
        public bool Checklogin(string parameter)
        {
           
            if (parameter != null)
            {
                return true;
            }
            return false;
        }

    }
}