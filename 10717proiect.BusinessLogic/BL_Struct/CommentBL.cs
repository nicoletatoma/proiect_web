using _10717proiect.Domain.Model.Comment;
using _10717proiect.BusinessLogic.Core;
using _10717proiect.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.BL_Struct
{
     public class CommentBL : CommentAPI, IComment
     {
          public string CreateCommentLogic(CommentDataModel data)
          {
               
               return CommCreateLogic(data);
          }
     }
}
