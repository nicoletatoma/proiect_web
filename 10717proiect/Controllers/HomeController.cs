using _10717proiect.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _10717proiect.BusinessLogic.DBContext;

namespace _10717proiect.Web.Controllers
{
     public class HomeController : Controller
     {
          // GET: Home
          public ActionResult Index()
          {
               return View();
          }

          public ActionResult Cos()
          {
               return View();
          }

          public ActionResult Ticket()
          {
               return View();
          }

          public ActionResult Details()
          {
               return View();

          }

          public ActionResult Error()
          {
               return View();
          }

          public ActionResult Profil()
          {
               // Presupunem că autentificarea s-a făcut pe baza emailului
               string userEmail = User.Identity.Name;

               using (var db = new UserContext())
               {
                    var user = db.Users.FirstOrDefault(u => u.Email == userEmail);

                    if (user == null)
                    {
                         return RedirectToAction("Auth", "SignIn"); // sau controllerul tău de login
                    }

                    return View(user); // Trimite modelul către View
               }
          }

     }
}