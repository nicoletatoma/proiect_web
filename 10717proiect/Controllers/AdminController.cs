using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Enums;
using _10717proiect.Domain.Model.Admin;
using _10717proiect.LogicHelper.Attributes;
using _10717proiect.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;

namespace _10717proiect.Controllers
{
     [IsAdmin]
     public class AdminController : Controller
     {
          private readonly IAdmin _admin;

          public AdminController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _admin = bl.GetAdminBL();
          }

          // GET: Admin
          public ActionResult Index()
          {
               return View();
          }
          public ActionResult Events()
          {
               return View();
          }

          public ActionResult Location()
          {
               return View();
          }

          public ActionResult Transactions()
          {
               return View();
          }

     }
}