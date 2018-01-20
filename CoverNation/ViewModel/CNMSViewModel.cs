using CoverNation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoverNation.ViewModel
{
    public class CNMSViewModel
    {
        public class CheckUserType
        {
            public bool IsMusician { get; set; }
        }
        public class ChangePersonalInfo
        {
            public int UserId { get; set; }

            [Display(Name = "First name")]
            [Required(ErrorMessage = "First name is required")]
            public string FirstName { get; set; }

            [Display(Name = "Last name")]
            [Required(ErrorMessage = "Last name is required")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Username name is required")]
            [RegularExpression(@"^[a-zA-Z0-9.]+$", ErrorMessage = "Username can't contain symbols. Only letters, numbers and dot are allowed.")]
            [StringLength(25, MinimumLength = 6, ErrorMessage = "Username must be between 6-25 symbols.")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Please provide a valid input for email. Valid format: name@example.com")]
            public string Email { get; set; }
            
        }

        public class UserGenres
        {
            public List<Genre> Genres { get; set; }
            public int GenreId { get; set; }
            [Display(Name = "Add new genre")]
            public List<SelectListItem> NewGenre { get; set; }
        }
        public class ImageShow
        {
            public string ProfilePictureURL { get; set; }
        }
        public class ChangePassword
        {
            [Required(ErrorMessage = "Old password is required")]
            [Display(Name = "Type your old password")]
            [DataType(DataType.Password)]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            [Display(Name = "Type your new password")]
            //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,25}$", ErrorMessage = "Password needs to be in valid format: small letters, big letters, numbers and symbols. Also password must be between 8-25 characters long.")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Confirm password and password do not match.")]
            [Display(Name = "Repeat your new password")]
            public string NewPasswordRepeat { get; set; }
        }
        public class FavouriteArtists
        {
            public int ArtistId { get; set; }
            public string ArtistName { get; set; }
            public string ArtistPictureURL { get; set; }
            public string Description { get; set; }
        }
        public class FavouriteArtistsList
        {
            public List<FavouriteArtists> artistList { get; set; }
        }
        public class FavouriteArtistsEdit
        {
            [Required(ErrorMessage = "Artist name is required")]
            [Display(Name = "Artist name")]
            public string ArtistName { get; set; }

            [Required(ErrorMessage = "Artist picture is required")]
            [Display(Name = "Select picture of artist")]
            public string ArtistPictureURL { get; set; }

            [Required(ErrorMessage = "Description is required")]
            [Display(Name = "Why do you like this one?")]
            public string Description { get; set; }
        }

        public class MusicianEducationEdit
        {
            [Display(Name = "Select Instrument")]
            public int InstrumentId { get; set; }
            public List<SelectListItem> Instruments { get; set; }

            [Display(Name = "Instituion or professor's name (not required)")]
            public string InstitutionName { get; set; }

            [Required(ErrorMessage = "Date is required")]
            [Display(Name = "Date when you started playing an instrument?")]
            public DateTime DateOfStart { get; set; }

            [Display(Name = "Tell us something more about you started playing this instrument :)")]
            public string DetailsAboutEducation { get; set; }

        }

        public class Instrument
        {
            public int InstrumentId { get; set; }
            public string InstrumentName { get; set; }
            public string ImageURL { get; set; }
        }
        public class InstrumentList
        {
            public List<Instrument> Instruments{ get; set; }
        }
        public class BiographyUpdate
        {
            public string Biography { get; set; }
        }
    }
}