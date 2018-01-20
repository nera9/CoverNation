using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoverNation.ViewModel;
using CoverNation.DAL;
using CoverNation.Models;
using CoverNation.Helper;



namespace CoverNation.Controllers
{
    public class CoverFeedController : Controller
    {
        CNContext db = new CNContext();      
        public ActionResult Index()
        {
            return View();           
        }
        public ActionResult test(string searchTerm, string group1)
        {
            if (group1 == "cover")
            {
                return RedirectToAction("getMusicianCovers", "CoverManagment", new { ProfileToShowUserId = 0, isVisitor = true, searchTerm = searchTerm });
            }
            else
            {
                return RedirectToAction("SearchUsers", "Users", new { searchTerm = searchTerm });
                
            }
        }
        public ActionResult getCoverFeedCovers(int? currentStep)
        {
            User User = db.Users.FirstOrDefault(x => x.UserId == Authentification.logedUser.UserId);
            List<Cover> covers;

            if (User.Role == Role.Musician)
            {
                List<int> musicianGenreList = db.PreferredGenres.Where(x => x.UserId == User.UserId).Select(x => x.GenreId).ToList();
                covers = db.Covers.Where(x => musicianGenreList.Contains(x.GenreId)).ToList();
            }
            else
            {
                List<int> userSubscriptions = db.Subscribers.Where(x => x.BasicUserId == User.UserId).Select(x => x.MusicianId).ToList();
                covers = db.Covers.Where(x => userSubscriptions.Contains(x.MusicianId)).ToList();
            }
            var Likes = db.Likes.ToList();
            int coversToTake = 8;

            if (currentStep == null)
            {
                covers = covers.Take(coversToTake).ToList();
            }
            else
            {
                covers = covers.OrderBy(x => x.DateOfPosting).Skip((int)currentStep * coversToTake).Take(coversToTake).ToList();
            }

            CoverManagmentViewModel.CoverShowList data =
                new CoverManagmentViewModel.CoverShowList
                {
                    IsCoverFeed = true,
                    CoverList = covers.Select(x => new CoverManagmentViewModel.CoverShow
                    {
                        CoverId = x.CoverId,
                        CoverName = x.CoverName,
                        Thumbnail = x.Thumbnail,
                        YTurl = x.VideoURL,
                        UploadDate = x.DateOfPosting,
                        IsCoverFeed = true,
                        CoverMusicianId = x.MusicianId,
                        CoverMusicianProfile = db.Users.FirstOrDefault(y => y.UserId == x.MusicianId).ProfilePictureURL,
                        CoverMusicianUsername = db.Users.FirstOrDefault(y => y.UserId == x.MusicianId).Username,

                    }).ToList()
                };
            foreach (var item in data.CoverList)
            {
                var l = Likes.Where(y => y.CoverId == item.CoverId).ToList();
                item.NumOfRaitings = l.Count();
                if (l.Count > 0)
                {
                    item.CoverRaiting = l.Sum(y => y.Raiting);
                    item.CoverRaitingAvg = Math.Round(l.Average(y => y.Raiting), 2);
                }
                else
                {
                    item.CoverRaiting = 0;
                }
                bool isLikedByCurrent = Likes.FirstOrDefault(y => y.CoverId == item.CoverId && y.UserId == User.UserId) == null ? false : true;
                item.IsLikedByCurrentUser = isLikedByCurrent;
                if (isLikedByCurrent)
                {
                    item.CurrentUserRaiting = Likes.FirstOrDefault(y => y.CoverId == item.CoverId && y.UserId == User.UserId).Raiting;
                }
                else
                {
                    item.CurrentUserRaiting = 0;
                }

            }
            if (currentStep == null)
            {
                return PartialView("_coversPartial", data);

            }
            else
            {
                return PartialView("_coversPartialScroll", data);
            }
        }
        
      

    }
}