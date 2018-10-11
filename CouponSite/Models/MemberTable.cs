using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMZ_Coupon.Models
{
    public class MemberTable
    {
        public string Email { get; set; }
        public string password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }
        public string Manager { get; set; }
        public string PCoupon { get; set; }
    }
}