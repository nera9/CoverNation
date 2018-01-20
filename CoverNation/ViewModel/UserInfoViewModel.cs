using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoverNation.Models;

namespace CoverNation.ViewModel
{
    public class UserInfo
    {
        public class UserInfoViewModel
        {
            public int UserId { get; set; }
            public string UserType { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public DateTime RegistrationDate { get; set; }
            public DateTime BirthDate { get; set; }
            public string Gender { get; set; }
            public string Biography { get; set; }
            public List<Genre> PreferredGenres { get; set; }
            public List<Artist> FavouriteArtists { get; set; }
            public List<Instrument> MusicianEducation { get; set; }
        }
        public class UserSearch
        {
            public int UserId { get; set; }
            public string UserType { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Username { get; set; }
            public string ProfilePic { get; set; }
        }
        public class UserSearchList
        {
            public List<UserSearch> SearchList { get; set; }
        }
    }
    
}