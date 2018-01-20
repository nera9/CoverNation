using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CoverNation.Models;


namespace CoverNation.ViewModel
{
    public class PasswordViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,25}$", ErrorMessage = "Password needs to be in valid format: small letters, big letters, numbers and symbols. Also password must be between 8-25 characters long.")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password and password do not match.")]
        public string ConfirmPassword { get; set; }

        public int UserId { get; set; }
       
    }
}