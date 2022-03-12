using System;
using System.Web;
using System.Web.Mvc;

namespace AIUB_Forum.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authenticated = base.AuthorizeCore(httpContext);
            return authenticated && httpContext.Session["usertype"].ToString().Equals("Admin");
        }
    }
}