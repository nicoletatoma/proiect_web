using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.User.Reg;
using _10717proiect.Models.Reg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _10717proiect.Controllers
{
    public class RegisterController : Controller
    {

        private readonly IUser _user;

        //constructor
        public RegisterController()
        {
            //call business logic
            var bl = new BusinessLogic.BusinessLogic();
            //instantiere obiecte
            _user = bl.GetUserBL();

        }
        // GET: Register
        public ActionResult Index()
        {
            return View(new UserDataReg());
        }


        public ActionResult Reg(UserDataReg data)
        {
            var local = new UserRegDTO()
            {
                Username = data.Username,
                Password = data.Password,
                Email = data.Email,
                Phone = data.Phone,
                Address = data.Address,
                Level = data.Level
            };

            UserRegDataResp resp = _user.RegisterUserAction(local);
            return null;
        }
    }
}