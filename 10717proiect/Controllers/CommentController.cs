using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _10717proiect.Domain.Model.Comment;
using _10717proiect.Models.Comment;

namespace _10717proiect.Controllers
{
    public class CommentController : Controller
    {

          public readonly IComment _comm;

          public CommentController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _comm = bl.GetCommentBL();
          }

          // GET: Comment
          public ActionResult Index()
          {
               return View();
          }

          [HttpPost]
          public ActionResult AddComm(CommentDataModelView addcomment) 
          { 
               var data = new _10717proiect.Domain.Model.Comment.CommentDataModel
               {
                    Description = addcomment.Description,
               };

               string result = _comm.CreateCommentLogic(data);

               return RedirectToAction("Index", "Evenimente");
          }



    }
}