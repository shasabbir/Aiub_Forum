using AIUB_Forum.Models.Database;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace AIUB_Forum.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index");
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var entities = new AIUB_ForumEntities2();
            var data = (from e in entities.Users
                        where e.Password.Equals(user.Password) &&
                              e.Email.Equals(user.Email)
                        select e).FirstOrDefault();
            if (data != null)
            {
                FormsAuthentication.SetAuthCookie("data.Username", false);
                //Session["username"]=data.Username;
                return RedirectToAction("Index");
            }

            TempData["msg"] = "Invalid Credentials";
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}