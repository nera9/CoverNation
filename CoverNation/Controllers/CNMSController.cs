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
    public class CNMSController : Controller
    {
        CNContext db = new CNContext();

        // GET: CNMS
        public ActionResult Index()
        {
            if (Authentification.logedUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            CNMSViewModel.CheckUserType vm =
                new CNMSViewModel.CheckUserType
                {
                    IsMusician = db.Users.Find(Authentification.logedUser.UserId).Role == Role.Musician
                };
                
                

            return View("CNMS",vm);
        }
        public ActionResult UserGenres()
        {
            CNMSViewModel.UserGenres vm =
                new CNMSViewModel.UserGenres
                {
                    Genres = db.PreferredGenres.Where(x => x.UserId == Authentification.logedUser.UserId)
                               .Select(x => x.Genre).ToList(),
                    NewGenre = db.Genres.Select(x => new SelectListItem { Value = x.GenreId.ToString(), Text = x.GenreName }).ToList(),
                };

            return PartialView("_genreChange", vm);
        }

        public ActionResult DeleteGenreFromUser(int genreId)
        {
            int UserId = Authentification.logedUser.UserId;
            PreferredGenre pg = db.PreferredGenres.FirstOrDefault(x => x.UserId == UserId && x.GenreId == genreId);
            db.PreferredGenres.Remove(pg);
            db.SaveChanges();

            return RedirectToAction("UserGenres");
        }

        [HttpPost]
        public ActionResult AddGenreToUser(string GenreId)
        {
            int UserId = Authentification.logedUser.UserId;
            PreferredGenre pg = new PreferredGenre();
            pg.GenreId = int.Parse(GenreId);
            pg.UserId = UserId;
            if (db.PreferredGenres.FirstOrDefault(x=>x.GenreId == pg.GenreId && x.UserId == pg.UserId) == null)
            {
                db.PreferredGenres.Add(pg);
                db.SaveChanges();
            }
            

            return RedirectToAction("UserGenres");
        }
        public ActionResult ChangePersonalInfo()
        {
            int userId = Authentification.logedUser.UserId;
            User user = db.Users.Find(userId);
            CNMSViewModel.ChangePersonalInfo vm =
                new CNMSViewModel.ChangePersonalInfo
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,

                };

            return PartialView("_personalInfoChange", vm);
        }

        [HttpPost]
        public ActionResult ChangePersonalInfo(CNMSViewModel.ChangePersonalInfo vm)
        {
            User user = db.Users.Find(Authentification.logedUser.UserId);
            if (vm.Email != user.Email)
            {
                if (IsExistEmail(vm.Email))
                {
                    ModelState.AddModelError("", "Email alredy exist.");
                    return PartialView("_personalInfoChange", vm);
                }
            }
            if (vm.Username != user.Username)
            {
                if (IsExistUsername(vm.Username))
                {
                    ModelState.AddModelError("", "Username alredy exist.");
                    return PartialView("_personalInfoChange", vm);
                }
            }
            

            if (ModelState.IsValid)
            {

                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.Username = vm.Username;
                user.Email = vm.Email;
                db.SaveChanges();

                ViewBag.SuccessMessage = "You have successfully update your info!";

                return RedirectToAction("ChangePersonalInfo");
            }

            return PartialView("_personalInfoChange", vm);
        }
        public ActionResult ChangeProfile()
        {
            string profilePicutureURL = Authentification.logedUser.ProfilePictureURL;
            if (profilePicutureURL == null)
            {
                profilePicutureURL = db.Users.Find(Authentification.logedUser.UserId).ProfilePictureURL;
            }
            CNMSViewModel.ImageShow vm =
                new CNMSViewModel.ImageShow
                {
                    ProfilePictureURL = profilePicutureURL
                };
            return PartialView("_changeProfilePicture", vm);
        }
        [HttpPost]
        public ActionResult ChangeProfile(HttpPostedFileBase uploadImg)
        {
            CNContext db = new CNContext();
            User user = db.Users.Find(Authentification.logedUser.UserId);
            string ImageName = System.IO.Path.GetFileName(uploadImg.FileName);
            string physicalPath = Server.MapPath("~/Content/images/" + ImageName);

            uploadImg.SaveAs(physicalPath);
            user.ProfilePictureURL = "/Content/images/" + ImageName;
            db.SaveChanges();

            CNMSViewModel.ImageShow vm =
               new CNMSViewModel.ImageShow
               {
                   ProfilePictureURL = user.ProfilePictureURL
               };

            return PartialView("_changeProfilePicture", vm);

        }

        public ActionResult ChangePassword()
        {


            return PartialView("_changePassword");
        }

        [HttpPost]
        public ActionResult ChangePassword(CNMSViewModel.ChangePassword vm)
        {
            if (ModelState.IsValid)
            {
                User u = db.Users.Find(Authentification.logedUser.UserId);
                if (Security.PasswordHash(vm.OldPassword) != u.Password)
                {
                    ModelState.AddModelError("", "You've entered wrong old password");
                    return PartialView("_changePassword", vm);
                }
                u.Password = Security.PasswordHash(vm.NewPassword);
                db.SaveChanges();
                vm.OldPassword = "";
                vm.NewPassword = "";
                vm.NewPasswordRepeat = "";
            }

            return PartialView("_changePassword", vm);
        }

        public ActionResult FavouriteArtistEdit()
        {
            return PartialView("_favouriteArtistsEdit");
        }


        [HttpPost]
        public ActionResult AddNewArtist(HttpPostedFileBase artistImg, string artistName, string description)
        {
            
            Artist artist = new Artist();
            string ImageName = System.IO.Path.GetFileName(artistImg.FileName);
            string physicalPath = Server.MapPath("~/Content/images/FavouriteArtists/" + ImageName);

            artistImg.SaveAs(physicalPath);
            artist.Name = artistName;
            artist.Description = description;
            artist.ImageURL = "/Content/images/FavouriteArtists/" + ImageName;
            artist.UserId = Authentification.logedUser.UserId;

            db.Artists.Add(artist);
            db.SaveChanges();

            CNMSViewModel.FavouriteArtists vm =
                new CNMSViewModel.FavouriteArtists
                {
                    ArtistId = artist.ArtistId,
                    ArtistName = artistName,
                    ArtistPictureURL = artist.ImageURL,
                    Description = description
                };



            return PartialView("_favouriteArtistItem", vm);
        }

        public ActionResult GetFavouriteArtist()
        {
            CNMSViewModel.FavouriteArtistsList vm =
                new CNMSViewModel.FavouriteArtistsList
                {
                    artistList = db.Artists.Where(x => x.UserId == Authentification.logedUser.UserId)
                                   .Select(x => new CNMSViewModel.FavouriteArtists
                                   {
                                       ArtistId = x.ArtistId,
                                       ArtistName = x.Name,
                                       ArtistPictureURL = x.ImageURL,
                                       Description = x.Description
                                   }).OrderByDescending(x => x.ArtistName).ToList()
                };



            return PartialView("_favouriteArtistList", vm);
        }
        public ActionResult DeleteFavouriteArtist(int artistId)
        {
            Artist a = db.Artists.Find(artistId);
            db.Artists.Remove(a);
            db.SaveChanges();

            return RedirectToAction("GetFavouriteArtist");
        }

        public ActionResult AddEducationItem()
        {
            CNMSViewModel.MusicianEducationEdit vm =
                new CNMSViewModel.MusicianEducationEdit
                {
                    Instruments = db.Instruments.Select(x => new SelectListItem { Value = x.InstrumentId.ToString(), Text = x.Name }).ToList(),
                };

            return PartialView("_educationEdit", vm);

        }

        [HttpPost]
        public ActionResult AddEducationItem(CNMSViewModel.MusicianEducationEdit vm)
        {
            MusicianEducation me = new MusicianEducation();
            me.DateOfLearning = vm.DateOfStart;
            me.InstrumentId = vm.InstrumentId;
            me.InstitutionName = vm.InstitutionName;
            me.DetailsAboutEducation = vm.DetailsAboutEducation;
            me.MusicianId = Authentification.logedUser.UserId;

            db.Musician_Educations.Add(me);
             db.SaveChanges();

            return RedirectToAction("GetMusicianInstruments");

        }

        public ActionResult GetMusicianInstruments()
        {
            CNMSViewModel.InstrumentList vm =
                new CNMSViewModel.InstrumentList
                {
                    Instruments = db.Musician_Educations.Where(x => x.MusicianId == Authentification.logedUser.UserId)
                                     .Select(x => x.Instrument).Select(x => new CNMSViewModel.Instrument
                                     {
                                         ImageURL = x.PictureURL,
                                         InstrumentId = x.InstrumentId,
                                         InstrumentName = x.Name

                                     }).ToList()
                };

            return PartialView("_musicianInstruments", vm);

        }

        public ActionResult DeleteMusicianInstrument(int instrumentId)
        {
            MusicianEducation me = db.Musician_Educations.FirstOrDefault(x=> x.InstrumentId == instrumentId && x.MusicianId == Authentification.logedUser.UserId);
            db.Musician_Educations.Remove(me);
            db.SaveChanges();


            return RedirectToAction("GetMusicianInstruments");
        }

        public ActionResult UpdateBiography()
        {
            CNMSViewModel.BiographyUpdate vm =
                new CNMSViewModel.BiographyUpdate
                {
                    Biography = db.Musicians.FirstOrDefault(x => x.UserId == Authentification.logedUser.UserId).Biography
                };

            return PartialView("_updateBiography", vm);
        }

        [HttpPost]
        public ActionResult UpdateBiography(CNMSViewModel.BiographyUpdate vm)
        {
            Musician m = db.Musicians.FirstOrDefault(x => x.UserId == Authentification.logedUser.UserId);
            m.Biography = vm.Biography;
            db.SaveChanges();

            ViewData["succesMessage"] = "You have successfully updated biography";

            return RedirectToAction("UpdateBiography");
        }




        #region IfExist
        public bool IsExistUsername(string username)
        {
            if (db.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower()) != null)
            {
                return true;
            }
            return false;
        }

        public bool IsExistEmail(string email)
        {
            if (db.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower()) != null)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}