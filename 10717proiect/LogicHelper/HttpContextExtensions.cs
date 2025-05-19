using _10717proiect.Domain.Model.User.UserActionResp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _10717proiect.LogicHelper
{
    public static class HttpContextExtensions
    {
        public static UserResp GetUserProfile(this HttpContext context)
        {
            return (UserResp)context?.Session["__SessionObject"];
        }

        public static void SetUserProfile(this HttpContext context, UserResp data)
        {
            context?.Session.Add("__SessionObject", data);
        }


    }
}