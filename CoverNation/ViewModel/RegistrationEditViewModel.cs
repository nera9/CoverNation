using System;
using CoverNation.Models;
using System.ComponentModel.DataAnnotations;


namespace CoverNation.ViewModel
{
    public class RegistrationEditViewModel
    {
        public int UserId { get; set; }

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
        [DataType(DataType.EmailAddress, ErrorMessage = "Please provide a valid input for email. Valid format: name@example.com")]
        public string Email { get; set; }


        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:mm/dd/yy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

       public Role Role { get; set; }
    }
}

    