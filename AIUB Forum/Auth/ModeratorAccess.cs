using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIUB_Forum.Auth
{
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class ModeratorAccess : AuthorizeAttribute
        {
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                var authenticated = base.AuthorizeCore(httpContext);
                return authenticated && httpContext.Session["usertype"].ToString().Equals("Moderator");
            }
        }
    
}