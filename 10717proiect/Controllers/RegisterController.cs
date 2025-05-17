using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.User.Reg;
using _10717proiect.Web.Models.Reg;
using Azure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace _10717proiect.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUser _user;
        public RegisterController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _user = bl.GetUserBL();
        }

        // GET: Register
        public ActionResult Index()
        {
            return View(new UserRegData());
        }

        [HttpPost]
        public ActionResult Reg(UserRegData data)
        {
            var local = new UserRegDTO
            {
                Id = data.Id,
                Username = data.Username,
                Address = data.Address,
                Email = data.Email,
                Password = data.Password,
                Phone = data.Phone
            };
            UserRegDataResp resp = _user.RegisterUserAction(local);

            // Verificați rezultatul înregistrării
            if (resp.Status)
            {
                // Redirecționare corectă către acțiunea SignIn din controllerul Auth
                return RedirectToAction("SignIn", "Auth");
            }
            else
            {
                // În caz de eroare, afișați un mesaj de eroare și rămâneți pe aceeași pagină
                ModelState.AddModelError("", resp.Error);
                return View("Index", data);
            }
        }

    }
}
