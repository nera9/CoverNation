using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoverNation.Models
{
    [Table("Users")]
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProfilePictureURL { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
        public string Location { get; set; }
    }

    public enum Gender {
        Male = 1,
        Female = 2
    }

    public enum Role
    {
        Admin = 1,
        Musician = 2,
        Basic = 3
    }
}