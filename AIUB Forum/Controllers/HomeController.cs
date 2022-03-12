using System.Data.Entity;
using AIUB_Forum.Models.Database;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace AIUB_Forum.Controllers
{
    // [Authorize]
    public class HomeController : Controller
    {
        private readonly AIUB_ForumEntities2 _db = new AIUB_ForumEntities2();
        [Authorize]
        public ActionResult Index()
        {
            var posts = _db.Posts.Include(p => p.User);
            return View(posts.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index");
            return View();
        }
        [AllowAnonymous]
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
                Session["usertype"] = data.UserType;
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
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}