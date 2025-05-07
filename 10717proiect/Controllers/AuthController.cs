using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Models.Auth;
using _10717proiect.Domain.Model.User.Auth;
using System;
using System.Collections.Generic;
using System.IO;
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
          //constructor
          public AuthController()
          {
               //call business logic
               var bl = new BusinessLogic.BusinessLogic();
               //instantiere obiecte
               _session = bl.GetSessionBL();
               _auth = bl.GetAuthBL();

          }
          //end entry point
          

          [HttpPost]
          public ActionResult Auth(UserDataLogin login)
          {
               
               //transmitere date în business logic
               var data = new UserLoginDTO
               {
                    LoginTime = DateTime.Now,
                    Email = login.Email,
                    Password = login.Password,
                    UserIP = "localhost"
               };

               string token = _auth.UserAuthLogic(data);

               //set cookies key value to session
               return RedirectToAction("Index", "Home"); // sau orice alt view existent


          }

     }
}