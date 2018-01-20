using CoverNation.DAL;
using CoverNation.Helper;
using CoverNation.Models;
using CoverNation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoverNation.Controllers
{
    public class PlaylistController : Controller
    {
        CNContext db = new CNContext();
        public ActionResult Index(int? userId,string v = "")
        {
            int logedUserId = Authentification.logedUser.UserId;
            PlaylistViewModel.PlaylistViewModelIndexList vm = new PlaylistViewModel.PlaylistViewModelIndexList
            {
            };

            if (userId == null)
            {
                userId = logedUserId;
                vm.IsDeleteList = true;
            }
            else
            {
                if (userId != logedUserId)
                {
                    vm.IsVisitor = true;
                }
                else
                {
                    vm.IsVisitor = false;
                }
                vm.IsDeleteList = false;
            }

            vm.Playlists = db.Playlists.Where(x => x.UserId == userId).Select(x => new PlaylistViewModel.PlaylistViewModelIndex
            {
                PlaylistId = x.PlaylistId,
                PlaylistName = x.Title,
                PlaylistThumbnail = db.CoversToPlayLists.Where(y => y.PlaylistId == x.PlaylistId).Select(y => y.Cover.Thumbnail).FirstOrDefault(),
                NumberOfCovers = db.CoversToPlayLists.Where(y => y.PlaylistId == x.PlaylistId).Count()
            }).ToList();

            //THIS PART IS CHECKING IS CALL MADE FROM PROFILE OR COVER FEED 
            if (v == "")
            {
                return PartialView("_index", vm);
            }
            else
            {
                return PartialView("_coverFeedIndex", vm);
            }
        }

        public ActionResult Play(int playlistId)
        {
            List<Cover> coversFromPlaylist = db.CoversToPlayLists.Where(x => x.PlaylistId == playlistId).Select(y => y.Cover).ToList();

            PlaylistViewModel.ModalPlay vm =
                new PlaylistViewModel.ModalPlay
                {
                    PlaylistPlayContent = coversFromPlaylist.Select(x => new PlaylistViewModel.PlayIndex
                    {
                        CoverId = x.CoverId,
                        Thumbnail = x.Thumbnail
                    }).ToList()
                };
            return PartialView("_playlistPlay", vm);
        }
        public ActionResult PlaylistItemPlay(int coverId)
        {
            int currentUserId = Authentification.logedUser.UserId;
            Cover cover = db.Covers.Find(coverId);
            var likes = db.Likes.Where(x => x.CoverId == cover.CoverId);
            CoverManagmentViewModel.CoverShow novi =
                new CoverManagmentViewModel.CoverShow
                {
                    CoverId = cover.CoverId,
                    CoverName = cover.CoverName,
                    UploadDate = cover.DateOfPosting,
                    Thumbnail = cover.Thumbnail,
                    YTurl = cover.VideoURL,
                    IsLikedByCurrentUser = likes.FirstOrDefault(x => x.UserId == currentUserId) == null ? false : true,

                };
            if (novi.IsLikedByCurrentUser)
            {
                novi.CurrentUserRaiting = likes.FirstOrDefault(x => x.UserId == currentUserId).Raiting;
            }
            else
            {
                novi.CurrentUserRaiting = 0;
            }
            novi.CoverRaiting = likes.Sum(x => x.Raiting);
            novi.CoverRaitingAvg = likes.Average(x => x.Raiting);
            novi.NumOfRaitings = likes.Count();

            return PartialView("_modalShow", novi);
        }

        public ActionResult ManagmentIndex()
        {
            return PartialView("_playlistManagmentIndex");
        }

        public ActionResult AddNew()
        {
            return PartialView("_addNew");
        }

        [HttpPost]
        public ActionResult AddNew(string playlistName)
        {
            Playlist newPlaylist = new Playlist();
            newPlaylist.UserId = Authentification.logedUser.UserId;
            newPlaylist.Title = playlistName;
            newPlaylist.DateOfCreating = DateTime.Now;
            db.Playlists.Add(newPlaylist);
            db.SaveChanges();

            PlaylistViewModel.AddNewCover playlistNames =
                new PlaylistViewModel.AddNewCover
                {
                    PlaylistNames = db.Playlists.Select(x => new SelectListItem { Value = x.PlaylistId.ToString(), Text = x.Title }).ToList(),
                };



            return PartialView("_coverToNew", playlistNames);
        }

        public ActionResult SearchAllCovers(string searchTerm)
        {

            CoverManagmentViewModel.CoverShowList vm =
                new CoverManagmentViewModel.CoverShowList
                {
                    CoverList = db.Covers.Where(x => x.CoverName.ToLower().StartsWith(searchTerm.ToLower()))
                                  .Select(x => new CoverManagmentViewModel.CoverShow
                                  {
                                      CoverId = x.CoverId,
                                      CoverName = x.CoverName,
                                      Thumbnail = x.Thumbnail,
                                      UploadDate = x.DateOfPosting
                                  }).ToList()
                };

            return PartialView("_coverToPL", vm);
        }

        public ActionResult CoverToPlaylist()
        {
            int userId = Authentification.logedUser.UserId;
            PlaylistViewModel.AddNewCover playlistNames =
               new PlaylistViewModel.AddNewCover
               {
                   PlaylistNames = db.Playlists.Where(x=> x.UserId == userId).Select(x => new SelectListItem { Value = x.PlaylistId.ToString(), Text = x.Title }).ToList(),
               };
            return PartialView("_coverToNew", playlistNames);
        }


        [HttpPost]
        public string CoverToPlaylist(int coverId, int playlistId)
        {

            if (db.CoversToPlayLists.FirstOrDefault(x => x.PlaylistId == playlistId && x.CoverId == coverId) != null)
            {
                return "The cover you selected is already in playlist.";
            }

            CoversToPlaylist ctpl = new CoversToPlaylist();
            ctpl.CoverId = coverId;
            ctpl.PlaylistId = playlistId;
            db.CoversToPlayLists.Add(ctpl);

            db.SaveChanges();

            return "You have successfully added cover to the playlist.";
        }

        public ActionResult GetPlaylistCovers(int playlistId)
        {
            var covers = db.CoversToPlayLists.Where(x => x.PlaylistId == playlistId).Select(x => x.Cover);
            PlaylistViewModel.DeleteList vm =
                new PlaylistViewModel.DeleteList
                {
                    PlaylistId = playlistId,
                    CoverDeleteList = covers.Select(x => new CoverManagmentViewModel.CoverShow
                    {
                        CoverId = x.CoverId,
                        CoverName = x.CoverName,
                        Thumbnail = x.Thumbnail,
                        YTurl = x.VideoURL,
                        UploadDate = x.DateOfPosting,
                    }).ToList()
                };

            return PartialView("_deleteList", vm);
        }

        public ActionResult DeleteFromPlaylist(int coverId, int playlistId)
        {

            var ctpl = db.CoversToPlayLists.FirstOrDefault(x => x.PlaylistId == playlistId && x.CoverId == coverId);
            if (ctpl != null)
            {
                db.CoversToPlayLists.Remove(ctpl);
                db.SaveChanges();
            }

            return RedirectToAction("GetPlaylistCovers", new { playlistId = playlistId });
        }

        public ActionResult DeletePlaylist(int playlistId)
        {
            var ctpl = db.CoversToPlayLists.Where(x => x.PlaylistId == playlistId).ToList();
            db.CoversToPlayLists.RemoveRange(ctpl);

            db.Playlists.Remove(db.Playlists.Find(playlistId));
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult PlaylistCoverSmallScreen(int playlistId)
        {
            int userId = Authentification.logedUser.UserId;
            int profileToShowUserId = db.Playlists.FirstOrDefault(x => x.PlaylistId == playlistId).UserId;
            var Likes = db.Likes.ToList();


            CoverManagmentViewModel.CoverShowList data =
                new CoverManagmentViewModel.CoverShowList
                {
                    ProfileToShowUserId = profileToShowUserId,
                    CoverList = db.CoversToPlayLists.Where(x => x.PlaylistId == playlistId).Select(x => x.Cover)
                                                    .Select(x => new CoverManagmentViewModel.CoverShow
                                                    {
                                                        CoverId = x.CoverId,
                                                        CoverName = x.CoverName,
                                                        Thumbnail = x.Thumbnail,
                                                        YTurl = x.VideoURL,
                                                        UploadDate = x.DateOfPosting,
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
                bool isLikedByCurrent = Likes.FirstOrDefault(y => y.CoverId == item.CoverId && y.UserId == userId) == null ? false : true;
                item.IsLikedByCurrentUser = isLikedByCurrent;
                if (isLikedByCurrent)
                {
                    item.CurrentUserRaiting = Likes.FirstOrDefault(y => y.CoverId == item.CoverId && y.UserId == userId).Raiting;
                }
                else
                {
                    item.CurrentUserRaiting = 0;
                }
            }

            return PartialView("_playlistCoverList_ss", data);


        }
    }
}