using AIUB_Forum.Models.Database;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AIUB_Forum.Controllers
{
    public class PostsController : Controller
    {
        private readonly AIUB_ForumEntities2 _db = new AIUB_ForumEntities2();

        // GET: Posts
        public ActionResult Index()
        {
            
            return View(_db.Posts.ToList());
        }
        public ActionResult View(int id)
        {
            
            var post = (from u in _db.Posts
               where u.PostId.Equals(id)
               select u).FirstOrDefault();
            
            return View(post);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Post());
        }
        [HttpPost]
        public ActionResult Create(Post b)
        {
            if (ModelState.IsValid)
            {
                _db.Posts.Add(b);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(b);
        }
        public ActionResult CreateAns(Answer b)
        {
            //if (ModelState.IsValid)
            //{
            _db.Answers.Add(b);
            _db.SaveChanges();
            return RedirectToAction("View", new { id = b.PostId });
            //}
            //return View(b);
        }
        public ActionResult CreateAnsReply(AnswerComment b)
        {
            //if (ModelState.IsValid)
            //{
            var ans = (from u in _db.Answers
                        where u.AnsId.Equals(b.AnsId)
                        select u).FirstOrDefault();
            _db.AnswerComments.Add(b);
            _db.SaveChanges();
            return RedirectToAction("View", new { id = ans.PostId});
            //}
            //return View(b);
        }
        
        public ActionResult Edit(int id)
        {
            var post = (from u in _db.Posts
                        where u.PostId.Equals(id)
                        select u).FirstOrDefault();

            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                var opost = (from p in _db.Posts
                            where p.PostId == post.PostId
                            select p).FirstOrDefault();
                //var npost = opost;
                //_db.Posts.Remove(opost); _db.SaveChanges();
                //npost.Title = post.Title;
                //npost.Body = post.Body;
                //_db.Posts.Add(npost);
                _db.Entry(opost).CurrentValues.SetValues(post);
                _db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int id)
        {
            var post = (from u in _db.Posts
                        where u.PostId.Equals(id)
                        select u).FirstOrDefault();

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var post = (from u in _db.Posts
                        where u.PostId.Equals(id)
                        select u).FirstOrDefault();
            if (post != null) { _db.Posts.Remove(post); }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
