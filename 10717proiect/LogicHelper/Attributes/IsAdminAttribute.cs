using _10717proiect.BusinessLogic.Interfaces;
using _10717proiect.Domain.Enums;
using _10717proiect.Domain.Model.User.UserActionResp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _10717proiect.LogicHelper.Attributes
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        private readonly IUser _session;
        public IsAdminAttribute()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetUserBL();
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionKey = HttpContext.Current.Request.Cookies["X-KEY"];
            if (sessionKey != null)
            {
                UserResp profile = _session.GetUserByCookie(sessionKey.Value);
                if (profile != null && profile.Level == URole.Admin)
                {
                    filterContext.Result =
                        new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new { controller = "Error", action = "Error" }));
                }
            }

        }
    }
}