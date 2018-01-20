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
    public class AdministratorController : Controller
    {
        CNContext db = new CNContext();
        // GET: Administrator
        public ActionResult Index(int id)
        {
            ProfileViewModel model = new ProfileViewModel();
            model = db.Users.Where(x => x.UserId == id)
                         .Select(x => new ProfileViewModel
                         {
                             UserId = x.UserId,
                             FirstName = x.FirstName,
                             LastName = x.LastName,
                             Username = x.Username,
                             ProfilePictureURL = x.ProfilePictureURL,
                             Role = "Admin",
                             IsVisitor = model.IsVisitor,
                             TypeOfVisitingUser = "Admin"

                         }).FirstOrDefault();
          
            return View("Profile", model);
        }
        public ActionResult DeleteGenre(int id)
        {
            Genre u = db.Genres.Where(x => x.GenreId == id).FirstOrDefault();
            db.Genres.Remove(u);
            db.SaveChanges();
            return RedirectToAction("ShowGenres");
        }
        public ActionResult ShowGenres()
        {
            GenreIndexVM model = new GenreIndexVM
            {
               GenresList = db.Genres.Select(x => new GenreRow()
                {
                    GenreId = x.GenreId,
                    GenreName = x.GenreName,
                    GenreImage = x.GenreImage
                }).ToList()
        };
   
            return View("ShowGenres", model);
    }
    [HttpGet]
        public ActionResult AddGenre()
        {
            GenreViewModel model = new GenreViewModel
            {
                GenreList = db.Genres.ToList()
            };
            return View("AddGenre", model);
        }
    [HttpPost]
        public ActionResult AddGenre(GenreViewModel vm)
        {
            Genre genre;
            if (vm.GenreId == 0)
            {
                genre = new Genre();
                db.Genres.Add(genre);
            }
            else
            {
                genre = db.Genres.Find(vm.GenreId);
            }
            genre.GenreId = vm.GenreId;
            genre.GenreName = vm.GenreName.ToString();
            genre.GenreImage = vm.GenreImage.ToString();
            db.SaveChanges();
            return RedirectToAction("ShowGenres");
        }
        public ActionResult EditGenre(int id)
        {
            GenreViewModel model = db.Genres.Where(x => x.GenreId == id).Select(f => new GenreViewModel
            {
                GenreId = f.GenreId,
                GenreName = f.GenreName,
                GenreImage = f.GenreImage,
                GenreList = db.Genres.ToList()
            }).Single();
            return View("AddGenre", model);
        }
    }
}