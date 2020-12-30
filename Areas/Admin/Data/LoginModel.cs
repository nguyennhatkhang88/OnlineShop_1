using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnLineShop_1.Areas.Admin.Data
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Mời bạn nhập user name ")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Mời bạn nhập password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}