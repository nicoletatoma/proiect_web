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
               // string session id provider
               //check id in session
               //var sId = "abcd";   
               //bool ISession = _session.ValidateSessionId(sId);
               return View(new UserDataLogin());
          }

          public ActionResult SignUp()
          {
               return View();
          }

          public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _auth = bl.GetAuthBL();
        }
        //end entry point
        public ActionResult Index()
        {
            //var sId = "abcd";   
            //bool ISession = _session.ValidateSessionId(sId);
            return View(new UserDataLogin());
        }
        [HttpPost]
        public ActionResult Auth (UserDataLogin login)
        {
            var data = new UserLoginDTO
            {
                Email = login.Email,
                Password = login.Password,
           
            };

          var resp = _auth.UserAuthLogic(data);

            if (resp.Status)
            {

                var respCookies = _auth.GeneratCookieByUser(resp.UserId);

                var cookie = respCookies.Cookie;
                ControllerContext.HttpContext.Response.Cookies.Add(cookie);

            }
            else
            {
                return RedirectToAction("Error", "Error");
            }

            Session["UserEmail"] = login.Email;

          //set cookies key value to session
          return RedirectToAction("Index", "Home"); 


        }

          //public ActionResult Logout()
          //{
          //     Session.Clear(); // sau Session["UserEmail"] = null;
          //     return RedirectToAction("Index", "Home");
          //}

          
    }
}