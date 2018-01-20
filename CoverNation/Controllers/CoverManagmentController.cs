using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoverNation.Models;
using CoverNation.ViewModel;
using CoverNation.DAL;
using CoverNation.Helper;
using System.Threading.Tasks;
using static CoverNation.ViewModel.CoverManagmentViewModel;

namespace CoverNation.Controllers
{
    public class CoverManagmentController : Controller
    {
        CNContext db = new CNContext();

        public ActionResult GetCover(int coverId)
        {
            User user = db.Users.Find(Authentification.logedUser.UserId);
            int currentUserId = user.UserId;
            Cover cover = db.Covers.Find(coverId);
            var likes = db.Likes.Where(x => x.CoverId == cover.CoverId);
            CoverManagmentViewModel.CoverShow novi =
                new CoverManagmentViewModel.CoverShow
                {
                    CoverId = cover.CoverId,
                    CoverName = cover.CoverName,
                    UploadDate = cover.DateOfPosting,
                    Thumbnail = cover.Thumbnail,
                    CoverMusicianProfile = user.ProfilePictureURL,
                    YTurl = cover.VideoURL,
                    IsLikedByCurrentUser = likes.FirstOrDefault(x => x.UserId == currentUserId) == null ? false : true,
                    CurrentUserRaiting = likes.FirstOrDefault(x => x.UserId == currentUserId).Raiting,
                    NumOfRaitings = likes.Count()
                };

            novi.CoverRaiting = likes.Sum(x => x.Raiting);
            novi.CoverRaitingAvg = Math.Round(likes.Average(x => x.Raiting), 2); ;


            return PartialView("_cover", novi);
        }


        public PartialViewResult getMusicianCovers(int ProfileToShowUserId, bool isVisitor, int? currentStep,string searchTerm ="")
        {
            User User = db.Users.FirstOrDefault(x=> x.UserId == Authentification.logedUser.UserId);
            User ProfileToShowUser = db.Users.FirstOrDefault(x=> x.UserId == ProfileToShowUserId);
            List<Cover> covers;
            if (searchTerm == "")
            {
                if (ProfileToShowUser != null && ProfileToShowUser.Role == Role.Musician)
                {
                    covers = db.Covers.Where(x => x.MusicianId == ProfileToShowUserId).ToList();
                }
                else
                {
                    covers = db.Likes.Where(x => x.UserId == ProfileToShowUserId).Select(x => x.Cover).ToList();
                }
            }
            else
            {
                covers = db.Covers.Where(x => x.CoverName.ToLower().StartsWith(searchTerm.ToLower())).ToList();
            }
            
            var Likes = db.Likes.ToList();
            int coversToTake = 8;

            if (currentStep == null)
            {
                covers = covers.Take(coversToTake).ToList();
            }
            else
            {
                covers = covers.OrderBy(x => x.CoverId).Skip((int)currentStep * coversToTake).Take(coversToTake).ToList();
            }

            CoverManagmentViewModel.CoverShowList data =
                new CoverManagmentViewModel.CoverShowList
                {
                    ProfileToShowUserId = ProfileToShowUserId,
                    IsVisitor = isVisitor,
                    TypeOfVisitingUser = User.Role.ToString(),
                    
                    CoverList = covers.Select(x => new CoverShow
                    {
                        CoverId = x.CoverId,
                        CoverName = x.CoverName,
                        Thumbnail = x.Thumbnail,
                        YTurl = x.VideoURL,
                        UploadDate = x.DateOfPosting,
                        CoverMusicianProfile = User.ProfilePictureURL
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
                }else
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

        public ActionResult Index()
        {
            CoverManagmentViewModel.CoverManagmentAddCover model = new CoverManagmentViewModel.CoverManagmentAddCover();

            model.GenresList = db.Genres.Select(x => new SelectListItem { Value = x.GenreId.ToString(), Text = x.GenreName }).ToList();
            return PartialView("_coverManagmentIndex", model);
        }

        public ActionResult CoverModal(CoverShow vm)
        {
            return PartialView("_modalShow", vm);
        }

        [HttpGet]
        public PartialViewResult AddCover()

        {
            CoverManagmentViewModel.CoverManagmentAddCover model = new CoverManagmentViewModel.CoverManagmentAddCover();

            model.GenresList = db.Genres.Select(x => new SelectListItem { Value = x.GenreId.ToString(), Text = x.GenreName }).ToList();
            ViewData["genreList"] = model.GenresList;

            return PartialView("_addCover", model);

        }

        [HttpPost]
        public ActionResult AddCover(CoverManagmentAddCover VM)
        {
            Cover newCover = new Cover();


            #region GetThumbnail
            string YTVideoId = VM.VideoURL.Substring(VM.VideoURL.IndexOf("watch?v=") + 8, 11);
            #endregion

            #region url to embed
            string embedLink = VM.VideoURL.Remove(24, (VM.VideoURL.Length - 24));
            embedLink += "embed/";
            embedLink += VM.VideoURL.Remove(0, 32);
            #endregion


            newCover.CoverName = VM.CoverName;
            newCover.GenreId = VM.GenreId;
            newCover.VideoURL = embedLink;
            newCover.DateOfPosting = DateTime.Now;
            newCover.MusicianId = Authentification.logedUser.UserId;
            newCover.Thumbnail = "http://img.youtube.com/vi/" + YTVideoId + "/mqdefault.jpg";

            ViewData["succesMessage"] = "You have successfully added cover";

            db.Covers.Add(newCover);
            db.SaveChanges();

            //Creating new viewModel cause of redirection to the partial view
            CoverManagmentViewModel.CoverManagmentAddCover vm = new CoverManagmentViewModel.CoverManagmentAddCover();
            VM.GenresList = db.Genres.Select(x => new SelectListItem { Value = x.GenreId.ToString(), Text = x.GenreName }).ToList();


            return PartialView("_addCover", VM);
        }

        public ActionResult DeleteCover()
        {
            return PartialView("_deleteCover", null);
        }

        [HttpPost]
        public ActionResult DeleteCover(int coverId)
        {
            Cover coverToDelete = db.Covers.Find(coverId);
            db.Covers.Remove(coverToDelete);

            //DELETE COVER FROM PLAYLIST
            CoversToPlaylist ctp = new CoversToPlaylist();
            Like coverLike = new Like();
            Comment coverComments = new Comment();
            foreach (var item in db.CoversToPlayLists.Where(x => x.CoverId == coverId))
            {
                ctp = item;
                db.CoversToPlayLists.Remove(ctp);
            }
            foreach (var item in db.Likes.Where(x => x.CoverId == coverId))
            {
                coverLike = item;
                db.Likes.Remove(coverLike);
            }
            foreach (var item in db.Comments.Where(x => x.CoverId == coverId))
            {
                coverComments = item;
                db.Comments.Remove(coverComments);
            }
            db.SaveChanges();
            ViewData["succesMessage"] = "You have successfully deleted cover";

            return PartialView("_deleteCover", null);
        }

        public ActionResult SearchCover(string searchTerm = "")
        {
            List<Cover> model = new List<Cover>();

            if (searchTerm == "")
            {
                model = db.Covers.ToList();
            }
            else
            {
                model = db.Covers.Where(x => x.CoverName.StartsWith(searchTerm)).ToList();
            }


            return PartialView("_deleteList", model);

        }

        public JsonResult GetCovers(string term)
        {
            var data = db.Covers.Where(y => y.CoverName.StartsWith(term)).Select(x => x.CoverName).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public void RateCover(int Raiting, int CoverId)
        {
            int LogedUserId = Authentification.logedUser.UserId;
            Like Like = db.Likes.FirstOrDefault(x => x.CoverId == CoverId && x.UserId == LogedUserId);
            if (Like != null)
            {
                Like.Raiting = Raiting;
                db.SaveChanges();
            }
            else
            {
                Like = new Like();
                Like.UserId = LogedUserId;
                Like.CoverId = CoverId;
                Like.Raiting = Raiting;
                db.Likes.Add(Like);
                db.SaveChanges();
            }

        }

        //public static int RoundCoverRaiting(double? raiting)
        //{
        //    if (raiting == null)
        //    {
        //        return 0;
        //    }
        //    if (raiting > 4.5)
        //    {
        //        return 5;
        //    }
        //    else if (raiting > 3.5)
        //    {
        //        return 4;
        //    }
        //    else if (raiting > 2.5)
        //    {
        //        return 3;
        //    }
        //    else if (raiting > 1.5)
        //    {
        //        return 2;
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}

        [HttpPost]
        public ActionResult PostComment(int coverId,string commentText)
        {
            int userId = Authentification.logedUser.UserId;
            User user = db.Users.Find(userId);
            Comment newComment = new Comment();
            newComment.CommentText = commentText;
            newComment.CoverId = coverId;
            newComment.UserId = user.UserId;
            newComment.Date = DateTime.Now;
            db.Comments.Add(newComment);
            db.SaveChanges();
            CoverManagmentViewModel.CoverCommentSection comment = new CoverCommentSection
            {
                Comment = commentText,
                Date = DateTime.Now.ToString(),
                UserId = user.UserId,
                Username = user.Username,
                UserPictureURL = user.ProfilePictureURL
            };

            return PartialView("_comment",comment);
        }
        public ActionResult GetComments(int coverId)
        {
            CoverManagmentViewModel.CoverCommentSectionList vm
                = new CoverCommentSectionList
                {
                    CommentList = db.Comments.Where(x => x.CoverId == coverId)
                                    .Select(x => new CoverCommentSection
                                    {
                                        Comment = x.CommentText,
                                        Date = x.Date.ToString(),
                                        UserId = x.UserId
                                    }).OrderByDescending(x=> x.Date).ToList()
                };

            foreach (var item in vm.CommentList)
            {
                User user = db.Users.Find(item.UserId);
                item.Username = user.Username;
                item.UserPictureURL = user.ProfilePictureURL;
                item.Date = item.Date;
            }

            return PartialView("_commentList", vm);
        }

    }
}