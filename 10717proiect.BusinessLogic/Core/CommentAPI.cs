using _10717proiect.BusinessLogic.DBContext;
using _10717proiect.Domain.Model.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10717proiect.BusinessLogic.Core
{
     public class CommentAPI 
     {
          internal string CommCreateLogic(CommentDataModel data)
          {
               using (var dbContext = new CommentContext())
               {
                    if (data == null || string.IsNullOrEmpty(data.Description))
                    {
                         return null;
                    }
                    var newComment = new CommentDbTable
                    {
                         Description = data.Description

                    };
                    dbContext.Comments.Add(newComment);
                    dbContext.SaveChanges();

                    return "Comment added successfully!";
               }
          }
     }
}
