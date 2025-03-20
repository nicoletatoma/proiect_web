using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.User;
using _10717proiect.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _10717proiect.Controllers
{
    public class SearchController : Controller
    {

        private ISearch _search;
        public SearchController()
        { 
            var bl = new BusinessLogic.BusinessLogic();
            _search = bl.GetSearchBL();
        }

        
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchEv search)
        {
            var eveniment = new SearchEv
            {
                NumeEveniment = search.NumeEveniment
            };

            string cautare = _search.UserSearchLogic(eveniment);

            return View();
        }
    }
}