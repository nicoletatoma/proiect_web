using _10717proiect.BusinessLogic.Interfaces;
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
        public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
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
    }
}