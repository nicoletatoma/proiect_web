using _10717proiect.Models.Carduri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _10717proiect.Controllers
{
    public class EvenimenteController : Controller
    {
        // GET: Evenimente
        public ActionResult Index()
        {
            List<AdaugareCard> card = new List<AdaugareCard>()
            {
                new AdaugareCard{   NumeCard = "Carbon",ImagineURL = "/Content/assets/images/small/carbon.jpeg",
                                    Pret = "120",Data = "12",Luna = "Iunie",BuyButon = "#"  },
                new AdaugareCard{   NumeCard = "Puterea Probabilitatii",
                                    ImagineURL = "/Content/assets/images/small/puterea.jpg",
                                    Pret = "100",Data = "23",Luna = "Februarie",BuyButon = "#" },
                new AdaugareCard{   NumeCard = "Tunete",ImagineURL = "/Content/assets/images/small/tunete.jpg",
                                    Pret = "120",Data = "12",Luna = "Iunie",BuyButon = "#"  },
                new AdaugareCard{   NumeCard = "Made in Moldova",ImagineURL = "/Content/assets/images/small/mademd.jpg",
                                    Pret = "120",Data = "12",Luna = "Iunie",BuyButon = "#"  },
                new AdaugareCard{   NumeCard = "Varvara",ImagineURL = "/Content/assets/images/small/Varvara.jpg",
                                    Pret = "120",Data = "12",Luna = "Iunie",BuyButon = "#"  },
                new AdaugareCard{   NumeCard = "Pescărușul",ImagineURL = "/Content/assets/images/small/pescarus.jpg",
                                    Pret = "120",Data = "12",Luna = "Iunie",BuyButon = "#"  },
                new AdaugareCard{   NumeCard = "Caleidoscop",ImagineURL = "/Content/assets/images/small/caleidoscop.jpg",
                                    Pret = "120",Data = "12",Luna = "Iunie",BuyButon = "#"  },
                new AdaugareCard{   NumeCard = "Women's day",ImagineURL = "/Content/assets/images/small/womenday.jpg",
                                    Pret = "120",Data = "12",Luna = "Iunie",BuyButon = "#"  }
            };
                return View(card);
            
        }


        
    }
}