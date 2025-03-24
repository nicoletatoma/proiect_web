using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.User;
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

        public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _auth = bl.GetAuthBL();
        }
        //end entry point
        public ActionResult Index()
        {
            // string session id provider
            //check id in session
            var sId = "abcd";   
            bool ISession = _session.ValidateSessionId(sId);
            return View();
        }
        [HttpPost]
        public ActionResult Auth (UserDataLogin login)
        {
            var data = new UserLoginDTO
            {
                Username = login.Username,
                Password = login.Password,
           
            };

            string token = _auth.UserAuthLogic(data);

            return View();
        }

    }
}