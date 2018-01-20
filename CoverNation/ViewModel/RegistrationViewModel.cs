using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CoverNation.Models;

namespace CoverNation.ViewModel
{
    public class RegistrationViewModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username name is required")]
        //[RegularExpression (@"^[a-zA-Z0-9.]+$", ErrorMessage = "Username can't contain symbols. Only letters, numbers and dot are allowed.")]
        //[StringLength(25, MinimumLength = 6, ErrorMessage = "Username must be between 6-25 symbols.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Please provide a valid input for email. Valid format: name@example.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,25}$", ErrorMessage = "Password needs to be in valid format: small letters, big letters, numbers and symbols. Also password must be between 8-25 characters long.")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password and password do not match.")]
        public string ConfirmPassword { get; set; }

        public string BirthDateDays { get; set; }
        public string BirthDateMonths { get; set; }
        public string BirthDateYears { get; set; }

        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "User type is required")]
        public UserType TypeOfUser { get; set; }

        public enum UserType
        {
            Musician = 2,
            BasicUser = 3
        }

    }
}