using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoverNation.Models;

namespace CoverNation.ViewModel
{
    public class ProfileViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string ProfilePictureURL { get; set; }
        public string Role { get; set; }
        public bool IsVisitor { get; set; }
        public bool IsSubscribed { get; set; }
        public string TypeOfVisitingUser { get; set; }
        public string Location { get; set; }
        public int Subs { get; set; }
        public int NumOfCovers { get; set; }
    }

}