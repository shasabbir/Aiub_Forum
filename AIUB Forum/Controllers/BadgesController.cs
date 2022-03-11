using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AIUB_Forum.Models.Database;

namespace AIUB_Forum.Controllers
{
    public class BadgesController : Controller
    {
        private AIUB_ForumEntities2 db = new AIUB_ForumEntities2();

        // GET: Badges
        public ActionResult Index()
        {
            var badges = db.Badges.Include(b => b.User);
            return View(badges.ToList());
        }

        // GET: Badges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Badge badge = db.Badges.Find(id);
            if (badge == null)
            {
                return HttpNotFound();
            }
            return View(badge);
        }

        // GET: Badges/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name");
            return View();
        }

        // POST: Badges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BadgeId,UserId,Name,Date,Class")] Badge badge)
        {
            if (ModelState.IsValid)
            {
                db.Badges.Add(badge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", badge.UserId);
            return View(badge);
        }

        // GET: Badges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Badge badge = db.Badges.Find(id);
            if (badge == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", badge.UserId);
            return View(badge);
        }

        // POST: Badges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BadgeId,UserId,Name,Date,Class")] Badge badge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(badge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", badge.UserId);
            return View(badge);
        }

        // GET: Badges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Badge badge = db.Badges.Find(id);
            if (badge == null)
            {
                return HttpNotFound();
            }
            return View(badge);
        }

        // POST: Badges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Badge badge = db.Badges.Find(id);
            db.Badges.Remove(badge);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
