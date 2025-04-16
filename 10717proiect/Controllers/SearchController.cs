using _10717proiect.BusinessLogic;
using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Model.User;
using _10717proiect.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace _10717proiect.Controllers
{
    public class SearchController : Controller
    {

          private readonly ISearch _search;
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
          public ActionResult Search(string NumeEveniment)
          {
               var search = new SearchEv { NumeEveniment = NumeEveniment };
               var rezultate = _search.UserSearchLogic(search);
               return View("SearchResults", rezultate);
          }

     }
}
