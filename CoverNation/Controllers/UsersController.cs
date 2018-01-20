using CoverNation.Helper;
using CoverNation.ViewModel;
using CoverNation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoverNation.DAL;

namespace CoverNation.Controllers
{
    public class UsersController : Controller
    {
        CNContext db = new CNContext();

        /*
            Action Subscribe is used on the profile part where you (as basic type user) want to subscribe/unsubscribe
            to another user (to musician type user). There is ajax call of this method where we send musicianId 
            to the action. Other id is taken from authentification.
        */
        public void Subscribe(int musicianId)
        {
            int subscriberId = Authentification.logedUser.UserId;
            Subscriber subscription = db.Subscribers.FirstOrDefault(x => x.BasicUserId == subscriberId && x.MusicianId == musicianId);
            if (subscription == null)
            {
                Subscriber NewSubscription = new Subscriber();
                NewSubscription.BasicUserId = subscriberId;
                NewSubscription.MusicianId = musicianId;
                NewSubscription.DateOfSubscription = DateTime.Now;
                db.Subscribers.Add(NewSubscription);
            }
            else
            {
                db.Subscribers.Remove(subscription);
            }

            db.SaveChanges();

        }
        /*
            Action Profile is one of the application main action. In here, lot of things are done and every
            time user request for the profile (any type of profile) this action is being called. 
            There are a lot things that we need to check here but some of them are:
                - Main thing to check is the requested user type and the visiting (type of the users which profile 
                  we are requesting) type, and also very important thing to chech is if user request his own profile.
                - If call is being made from basic to musician profile we need to check if user (basic user) is subscribed to musician
                - When we are visiting musician type profile we need to calculate how many covers and how many subscribtions he has,
                  but if we are visiting basic user then we need to know how many times he is subscribed and how many covers does he rated.
                     
        */
        public new ActionResult Profile(int id = 0)
        {
            if (Authentification.logedUser == null)
            {
                return RedirectToAction("Login");
            }
            ProfileViewModel model = new ProfileViewModel();
            int userFromDbId;
            model.IsVisitor = false;
            model.IsSubscribed = false;

            User sessionUser = db.Users.FirstOrDefault(x => x.UserId == Authentification.logedUser.UserId);


            if (id == 0)
            {
                if (sessionUser != null)
                {
                    userFromDbId = sessionUser.UserId;
                }
                else
                {
                    return RedirectToAction("Login", "Users");
                }
            }
            else
            {
                userFromDbId = id;
                //checking if user is visiting his own profile based on session and based on cookie
                if (sessionUser != null)
                {
                    model.IsVisitor = sessionUser.UserId == userFromDbId ? false : true;
                }
                else
                {
                    return RedirectToAction("Profile", "Users");
                }
                //checking the type of user who is requesting profile
                if (model.IsVisitor)
                {
                    model.TypeOfVisitingUser = sessionUser.Role.ToString();
                    model.IsSubscribed = db.Subscribers.FirstOrDefault(x => x.MusicianId == id && x.BasicUserId == sessionUser.UserId) == null ? false : true;

                }
            }
            model = db.Users.Where(x => x.UserId == userFromDbId)
                          .Select(x => new ProfileViewModel
                          {
                              UserId = x.UserId,
                              FirstName = x.FirstName,
                              LastName = x.LastName,
                              Username = x.Username,
                              ProfilePictureURL = x.ProfilePictureURL,
                              Role = x.Role.ToString() == "Basic" ? "BasicUser" : "Musician",
                              IsVisitor = model.IsVisitor,
                              IsSubscribed = model.IsSubscribed,
                              TypeOfVisitingUser = model.TypeOfVisitingUser == "Basic" ? "BasicUser" : "Musician",
                              Location = x.Location,
                              Subs = x.Role.ToString() == "Musician" ? db.Subscribers.Where(y => y.MusicianId == x.UserId).Count() : db.Subscribers.Where(y => y.BasicUserId == x.UserId).Count(),
                              NumOfCovers = x.Role.ToString() == "Musician" ? db.Covers.Where(y => y.MusicianId == x.UserId).Count() : db.Likes.Where(y => y.UserId == x.UserId).Count(),
                          }).FirstOrDefault();

            if (model == null)
            {
                return RedirectToAction("Logout");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Authentification.logedUser != null)
                return RedirectToAction("Profile", "Users");

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel userLogin)
        {

            if (ModelState.IsValid)
            {
                string password = Security.PasswordHash(userLogin.Password);

                User userFromDb = db.Users.FirstOrDefault(x => x.Username.Equals(userLogin.Username) && x.Password.Equals(password));

                if (userFromDb == null)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View(userLogin);
                }

                Authentification.Login(userFromDb);

                if (userLogin.RemeberMe)
                {
                    Authentification.AddToCookie(userFromDb);
                }

                if (userFromDb.Role == Role.Admin)
                {
                    return RedirectToAction("Profile", "Administrator");
                }

                return RedirectToAction("Profile", "Users");

            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Authentification.Logout();
            return RedirectToAction("Login", "Users");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegistrationViewModel user)
        {
            if (ModelState.IsValid)
            {
                #region Password hash
                user.Password = Security.PasswordHash(user.Password);
                user.ConfirmPassword = Security.PasswordHash(user.ConfirmPassword);
                #endregion
                if (UserToDatabase(user))
                    return RedirectToAction("Login", "Users");
            }

            return RedirectToAction("Register", "Users");
        }
        private bool UserToDatabase(RegistrationViewModel user)
        {
            try
            {
                User userToDb;

                if (user.TypeOfUser == RegistrationViewModel.UserType.Musician)
                {
                    userToDb = new Musician();
                    userToDb.Role = Role.Musician;
                }
                else if (user.TypeOfUser == RegistrationViewModel.UserType.BasicUser)
                {
                    userToDb = new BasicUser();
                    userToDb.Role = Role.Basic;
                }
                else
                {
                    userToDb = new Administrator();
                    userToDb.Role = Role.Admin;
                }

                DateTime userBirthDate = new DateTime(Convert.ToInt32(user.BirthDateYears), Convert.ToInt32(user.BirthDateMonths), Convert.ToInt32(user.BirthDateDays));
                userToDb.FirstName = user.FirstName;
                userToDb.LastName = user.LastName;
                userToDb.Password = user.Password;
                userToDb.Username = user.Username;
                userToDb.BirthDate = userBirthDate;
                userToDb.Email = user.Email;
                userToDb.RegistrationDate = DateTime.Now;
                userToDb.Gender = user.Gender;
                userToDb.ProfilePictureURL = "/Content/images/default-profile.png";

                db.Users.Add(userToDb);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public ActionResult About(int userId)
        {
            User data = db.Users.Find(userId);

            UserInfo.UserInfoViewModel vm = new UserInfo.UserInfoViewModel
            {
                UserId = data.UserId,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                Gender = data.Gender.ToString(),
                BirthDate = data.BirthDate,
                RegistrationDate = data.RegistrationDate,
                FavouriteArtists = db.Artists.Where(x => x.UserId == data.UserId).ToList(),
            };
            vm.UserType = "Basic";
            if (data.Role == Role.Musician)
            {
                vm.Biography = db.Musicians.Find(data.UserId).Biography;
                vm.UserType = "Musician";
                vm.MusicianEducation = db.Musician_Educations.Where(x => x.MusicianId == userId).Select(x => x.Instrument).ToList();
            }

            vm.PreferredGenres = db.PreferredGenres.Where(x => x.UserId == data.UserId).Select(x => x.Genre).ToList();
            return PartialView("_about", vm);
        }
        public ActionResult SearchUsers(string searchTerm)
        {
            string tlSearchTerm = searchTerm.ToLower();
            UserInfo.UserSearchList vm =
                new UserInfo.UserSearchList
                {
                    SearchList = db.Users
                                 .Where(x => x.FirstName.ToLower().StartsWith(tlSearchTerm) ||
                                             x.LastName.ToLower().StartsWith(tlSearchTerm) ||
                                             x.Username.ToLower().StartsWith(tlSearchTerm))
                                 .Select(x => new UserInfo.UserSearch
                                 {
                                     UserId = x.UserId,
                                     FirstName = x.FirstName,
                                     LastName = x.LastName,
                                     ProfilePic = x.ProfilePictureURL,
                                     Username = x.Username,
                                     UserType = x.Role.ToString()
                                 }).ToList()
                };
            return PartialView("_usersSearchList", vm);
        }

    }
}