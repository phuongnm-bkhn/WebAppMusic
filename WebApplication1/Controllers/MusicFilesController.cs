using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppMusic.Models;

namespace WebAppMusic.Controllers
{
    public class MusicFilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MusicFiles
        public ActionResult Index()
        {
            return View(db.MusicFiles.ToList());
        }

        // GET: MusicFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicFile musicFile = db.MusicFiles.Find(id);
            if (musicFile == null)
            {
                return HttpNotFound();
            }
            return View(musicFile);
        }

        // GET: MusicFiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MusicFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Link")] MusicFile musicFile)
        {
            if (ModelState.IsValid)
            {
                db.MusicFiles.Add(musicFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(musicFile);
        }

        // GET: MusicFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicFile musicFile = db.MusicFiles.Find(id);
            if (musicFile == null)
            {
                return HttpNotFound();
            }
            return View(musicFile);
        }

        // POST: MusicFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Link")] MusicFile musicFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musicFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(musicFile);
        }

        // GET: MusicFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicFile musicFile = db.MusicFiles.Find(id);
            if (musicFile == null)
            {
                return HttpNotFound();
            }
            return View(musicFile);
        }

        // POST: MusicFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MusicFile musicFile = db.MusicFiles.Find(id);
            db.MusicFiles.Remove(musicFile);
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
