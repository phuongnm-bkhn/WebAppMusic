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
    public class MusicPlaylistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MusicPlaylists
        public ActionResult Index()
        {
            return View(db.MusicPlaylists.ToList());
        }

        // GET: MusicPlaylists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicPlaylist musicPlaylist = db.MusicPlaylists.Find(id);
            if (musicPlaylist == null)
            {
                return HttpNotFound();
            }
            return View(musicPlaylist);
        }

        // GET: MusicPlaylists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MusicPlaylists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DateCreate,CountLike,CountDislike,CountListen,LinkOtherSite")] MusicPlaylist musicPlaylist)
        {
            if (ModelState.IsValid)
            {
                db.MusicPlaylists.Add(musicPlaylist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(musicPlaylist);
        }

        // GET: MusicPlaylists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicPlaylist musicPlaylist = db.MusicPlaylists.Find(id);
            if (musicPlaylist == null)
            {
                return HttpNotFound();
            }
            return View(musicPlaylist);
        }

        // POST: MusicPlaylists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DateCreate,CountLike,CountDislike,CountListen,LinkOtherSite")] MusicPlaylist musicPlaylist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musicPlaylist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(musicPlaylist);
        }

        // GET: MusicPlaylists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicPlaylist musicPlaylist = db.MusicPlaylists.Find(id);
            if (musicPlaylist == null)
            {
                return HttpNotFound();
            }
            return View(musicPlaylist);
        }

        // POST: MusicPlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MusicPlaylist musicPlaylist = db.MusicPlaylists.Find(id);
            db.MusicPlaylists.Remove(musicPlaylist);
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
