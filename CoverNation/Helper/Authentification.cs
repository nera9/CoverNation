using CoverNation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoverNation.DAL;

namespace CoverNation.Helper
{

    public class Authentification
    {
        public static User loginUser
        {
            get { return (User)HttpContext.Current.Session["_cn_user"]; }
            set { HttpContext.Current.Session["_cn_user"] = value; }
        }
        public static HttpCookie UserCookie
        {
            get { return HttpContext.Current.Request.Cookies["_cn_cookie"]; }
        }
        public static User logedUser
        {
             
            get
            {
                User foundUser = new User();
                if (loginUser != null)
                {
                    return foundUser = (User)HttpContext.Current.Session["_cn_user"];
                }
                else if (HttpContext.Current.Request.Cookies["_cn_cookie"] != null)
                {
                    CNContext db = new CNContext();
                    return foundUser = db.Users.Find(int.Parse(UserCookie.Values["UserId"]));
                }
                else
                {
                    return null;
                }
            }
        }
       

        public static void Login(User user)
        {
            loginUser = user;
        }
        public static void AddToCookie(User user)
        {
            HttpCookie cookie = UserCookie;
            if (cookie == null)
            {
                cookie = new HttpCookie("_cn_cookie");
            }
            cookie.Values.Add("UserId", user.UserId.ToString());
            cookie.Values.Add("Username", user.Username.ToString());
            cookie.Values.Add("UserRole", user.Role.ToString());
            cookie.Values.Add("UserProfilePicturePath", user.ProfilePictureURL!=null?user.ProfilePictureURL.ToString():"none");

            cookie.Expires = DateTime.Now.AddDays(7);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void Logout()
        {
            loginUser = null;
            if (HttpContext.Current.Request.Cookies["_cn_cookie"] != null)
            {
                var c = new HttpCookie("_cn_cookie");
                c.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(c);
            }
        }
    }
}