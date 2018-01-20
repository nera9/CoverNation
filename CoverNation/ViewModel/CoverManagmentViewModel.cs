
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoverNation.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CoverNation.ViewModel
{
    public class CoverManagmentViewModel
    {
       public class CoverManagmentAddCover
        {
            public int Id { get; set; }
            [Display(Name = "Cover name: ")]
            public string CoverName { get; set; }
            [Display(Name = "Insert youtube URL: ")]
            public string VideoURL { get; set; }
            [Display(Name = "Select genre for cover: ")]
            public int GenreId { get; set; }
            public int MusicianId { get; set; }
            public List<SelectListItem> GenresList { get; set; }
        }

        public class CoverManagmentIndexList
        {
            public List<CoverManagmentAddCover> CoverList { get; set; }
        }
        public class CoverShow
        {
            public int CoverId { get; set; }
            public string CoverName { get; set; }
            public int CoverRaiting { get; set; }
            public double CoverRaitingAvg { get; set; }
            public int NumOfRaitings { get; set; }
            public DateTime UploadDate { get; set; }
            public string Thumbnail { get; set; }
            public string YTurl { get; set; }
            public bool IsLikedByCurrentUser { get; set; }
            public int? CurrentUserRaiting { get; set; }

            //ONLY FOR COVER FEED
            public bool IsCoverFeed { get; set; }
            public int CoverMusicianId { get; set; }
            public string CoverMusicianProfile { get; set; }
            public string CoverMusicianUsername { get; set; }
        }

        public class CoverShowList
        {
            public int ProfileToShowUserId { get; set; }
            public bool IsVisitor { get; set; }
            public bool IsCoverFeed { get; set; }
            public string TypeOfVisitingUser { get; set; }
            public List<CoverShow> CoverList { get; set; }
        }
        public class CoverCommentSection
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            public string Comment { get; set; }
            public string UserPictureURL { get; set; }
            public string Date { get; set; }

        }
        public class CoverCommentSectionList
        {
            public int PostingUserProfilePictureURL { get; set; }
            public List<CoverCommentSection> CommentList { get; set; }
        }


    }
}