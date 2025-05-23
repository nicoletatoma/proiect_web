using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.User;
using _10717proiect.Domain.Model.User.UserActionResp;
using _10717proiect.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace _10717proiect.Controllers
{
	public class AuthController : Controller
    {
        //entry point
        private readonly ISession _session;
        private readonly IAuth _auth;


          public ActionResult SignIn()
          {

               return View(new UserDataLogin());
          }

          public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _auth = bl.GetAuthBL();
        }
        //end entry point
        
        [HttpPost]
        public ActionResult Auth (UserDataLogin login)
        {
            var data = new UserLoginDTO
            {
                Email = login.Email,
                Password = login.Password,
                UserIP = "Localhost"
           
            };

          var resp = _auth.UserAuthLogic(data);

            if (resp.Status)
            {

                var respCookies = _auth.GeneratCookieByUser(resp.UserId);

                var cookie = respCookies.Cookie;
                ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                Session["UserEmail"] = login.Email;
            }
            else
            {
                return RedirectToAction("Error", "Error");
            }



            //set cookies key value to session
            return RedirectToAction("Index", "Home"); 


        }

        public ActionResult Logout()
        {
            
            Session.Clear();

           
            var sessionCookie = Request.Cookies["X-KEY"];
            if (sessionCookie != null)
            {
                _session.InvalidateUserSession(sessionCookie.Value);

               
                sessionCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(sessionCookie);
            }

            
            System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";

            return RedirectToAction("SignIn", "Auth");
        }


    }
}