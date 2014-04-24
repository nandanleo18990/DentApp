using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DentAppSys.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address: ")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "First Name: ")]
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Last Name: ")]
        public string Lastname { get; set; }

    }

    public class RegAndLog
    {
        public RegisterModel RegisterModel { get; set; }

        public LoginModel LoginModel { get; set; }

        
    }
}