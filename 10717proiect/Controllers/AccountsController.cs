using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _10717proiect.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
          public ActionResult SignIn()
          {
               return View();
          }

          public ActionResult SignUp()
          {
               return View();
          }
     }
}