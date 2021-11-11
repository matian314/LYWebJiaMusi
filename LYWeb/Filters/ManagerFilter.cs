using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace LYWeb.Filters
{
    public class ManagerAttribute : ActionFilterAttribute, IAuthenticationFilter 
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext context)
        {
            var role = context.HttpContext.Session["role"]?.ToString();
            if(role == null || role != "管理员")
            {
                context.Result = new HttpNotFoundResult();//new HttpUnauthorizedResult();
            }
        }
    }
}