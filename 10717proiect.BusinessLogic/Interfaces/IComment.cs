using _10717proiect.Domain.Model.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.Interfaces
{
     public interface IComment
     {
          string CreateCommentLogic(CommentDataModel data);
     }
}
