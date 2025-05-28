using _10717proiect.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

     }
}