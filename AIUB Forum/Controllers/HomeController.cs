using System.Linq;
using System.Web.Mvc;
using AIUB_Forum.Models.Database;

namespace AIUB_Forum.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
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
                return RedirectToAction("Index");
            }

            TempData["msg"] = "Invalid Credentials";
            return RedirectToAction("Login");
        }
    }
}