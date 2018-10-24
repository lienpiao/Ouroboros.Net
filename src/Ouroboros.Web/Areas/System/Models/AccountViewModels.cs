using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ouroboros.Web.Areas.System.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "账号非空")]
        [Display(Name = "账号")]
        public string Name { get; set; }

        [Required(ErrorMessage = "密码非空")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "验证码")]
        [Required(ErrorMessage = "验证码非空")]
        public string VCode { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}