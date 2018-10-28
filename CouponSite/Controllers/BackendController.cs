using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMZ_Coupon.Models;
using AMZ_Coupon.Utility;
using CouponSite.Models;
using Microsoft.AspNetCore.Hosting;
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

        private readonly IHostingEnvironment _environment;

        // Constructor
        public BackendController(IHostingEnvironment IHostingEnvironment)
        {
            _environment = IHostingEnvironment;
        }


        [HttpPost]
        public bool InsertCouponDetail(CouponSite.Models.InsertProduct products)
        {
            //if (products.Picture.Length > 1100000) // >1.1mb
            //{
            //    return false;
            //};
            var x = DateTime.Now;
            var n = CouponDB.InsertCouponDetail(products, _environment);
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