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

        // GET ALL USERS
        public ActionResult GetAllUsers()
        {
            var domainUsers = _admin.GetAllUsers();

            var viewModel = domainUsers.Select(u => new UserData
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Address = u.Address,
                Phone = u.Phone,
                RequestTime = u.RequestTime,
                UserRole = u.UserRole
            }).ToList();

            return View(viewModel);
        }

        // EDIT USER
        [HttpPost]
        public ActionResult EditUser(UserData data)
        {
            if (data == null)
            {
                // Returnează un răspuns JSON cu succes: false și un mesaj de eroare
                return Json(new { success = false, message = "Datele primite sunt invalide." });
            }

            try
            {
                var userData = new UserAllData
                {
                    Id = data.Id,
                    Username = data.Username,
                    Email = data.Email,
                    Address = data.Address,
                    Phone = data.Phone,
                    UserRole = (URole)Enum.Parse(typeof(URole), data.UserRole.ToString())
                };

                bool isChanged = _admin.EditUser(userData);

                if (isChanged)
                {
                    return Json(new { success = true, message = "Utilizatorul a fost editat cu succes." });
                }
                else
                {
                    return Json(new { success = false, message = "A apărut o eroare la editarea utilizatorului." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "A apărut o eroare neașteptată: " + ex.Message });
            }
        }

        // GET SEARCH USER VIEW
        public ActionResult FindUser()
        {
            return View();
        }

        // SEARCH USER
        public JsonResult SearchUsers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Json(new { success = false, message = "Termenul de căutare nu poate fi gol." }, JsonRequestBehavior.AllowGet);
            }

            var users = _admin.SearchUser(searchTerm);

            if (users == null || !users.Any())
            {
                return Json(new { success = true, users = new object[0], message = "Nu s-au găsit utilizatori." }, JsonRequestBehavior.AllowGet);
            }

            var mappedUsers = users.Select(u => new UserData
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                RequestTime = u.RequestTime,
                UserRole = u.UserRole
            }).ToList();

            return Json(new { success = true, users = mappedUsers }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin
        public ActionResult Index()
          {
               return View();
          }
          public ActionResult EditEvenim()
          {
               return View();
          }

          public ActionResult EditLoc()
          {
               return View();
          }


     }
}