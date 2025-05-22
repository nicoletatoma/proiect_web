using _10717proiect.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _10717proiect.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
        
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("SignIn", "Auth");
            }
            return View();
    
        }

          public ActionResult Cos()
          {
               return View();
          }

    }
}