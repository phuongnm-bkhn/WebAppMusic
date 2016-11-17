using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            //return View(db.MusicPlaylists.ToList());
            return RedirectToAction("List");
        }

        // GET: MusicPlaylists
        public ActionResult List()
        {
            Dictionary<string, List<MusicPlaylist>> dataPlaylist
                = new Dictionary<string, List<MusicPlaylist>>();
            List<MusicPlaylist> lstPlaylist = new List<MusicPlaylist>();
            if (User.Identity.IsAuthenticated)
            {
                lstPlaylist = db.Users
                    .Where(user => user.UserName == User.Identity.Name)
                    .Select(user => user.Playlists)
                    .First()
                    .ToList();
                
            }
            dataPlaylist.Add("userPlaylist", lstPlaylist);
            dataPlaylist.Add("nonUserPlaylist", db.MusicPlaylists.ToList());

            return View(dataPlaylist);
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


        // GET: MusicPlaylists/Add/5
        [Authorize]
        public ActionResult Add([Bind(Include = "Id")] int Id)
        {
            // Them playlist vao ds playlist cua user
            db.Users
                .Where(user => user.UserName == User.Identity.Name)
                .First()
                .Playlists.Add(db.MusicPlaylists.Find(Id));
            db.SaveChanges();

            // Luu playlist nhac vao tai khoan nguoi dung hien tai
            return RedirectToAction("List");
        }

        // GET: MusicPlaylists/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: MusicPlaylists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Name,LinkOtherSite")] MusicPlaylist musicPlaylist,
            [Bind(Include = "linkMusic")] string[] linkMusic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Cai dat lai gia tri cac thuoc tinh 
                    // :Reset all Attributes
                    musicPlaylist.DateCreate = System.DateTime.Now;
                    musicPlaylist.CountLike = 0;
                    musicPlaylist.CountDislike = 0;
                    musicPlaylist.CountListen = 0;

                    // Luu lai doi tuong
                    // :Save obj to db
                    MusicPlaylist newPlaylist = db.MusicPlaylists.Add(musicPlaylist);

                    //if (true)
                    //{
                        
                    //    // Lưu liên kết playlist với người dùng 
                    //    db.Users.Where(user => user.UserName == User.Identity.Name)
                    //    .First()
                    //    .Playlists
                    //    .Add(newPlaylist);

                    //    db.SaveChanges();   // Luu Playlist 
                    //    return RedirectToAction("Index");
                    //}

                    db.SaveChanges();   // Luu Playlist 
                    List<MusicFile> listMusic= new List<MusicFile>();
                    foreach (var link in linkMusic)
                    {
                        // Kiem tra link mp3.zing, chiasenhac, youtube
                        if (false || link.Length <= 3) continue; 

                        // Kiem tra trong csdl 
                        if (db.MusicFiles
                            .Where(e => e.Link == link)
                            .Select(m => m.Id)
                            .Count() > 0)
                            continue;

                        // Them bai hat moi vao csdl 
                        MusicFile newFile = new MusicFile();
                        newFile.Link = link;

                        db.MusicFiles.Add(newFile);
                        db.SaveChanges();   // Luu file nhac
                        listMusic.Add(newFile);
                    }

                    // Lưu liên kết nhạc vs playlist
                    newPlaylist.MusicFiles = listMusic.ToArray();   
                    db.SaveChanges();

                    // Lưu liên kết playlist với người dùng 
                    db.Users.Where(user => user.UserName == User.Identity.Name)
                    .First()
                    .Playlists
                    .Add(newPlaylist);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Không tạo được playlist mới. Lỗi cấu trúc dữ liệu.");
                throw ex;
            }
            return View(musicPlaylist);
        }

        // GET: MusicPlaylists/Edit/5
        [Authorize]
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
        public ActionResult Edit([Bind(Include = "Id,Name,DateCreate,LinkOtherSite")] MusicPlaylist musicPlaylist)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userName = db.Users
                   .Where(user => user.UserName == User.Identity.Name)
                   .FirstOrDefault();

                MusicPlaylist playlistX = userName.Playlists
                    .Where(playlist=>playlist.Id == musicPlaylist.Id)
                    .FirstOrDefault();

                if (playlistX == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //db.Entry(musicPlaylist).State = EntityState.Modified;
                //db.MusicPlaylists.Attach(musicPlaylist);
                //var entry = db.Entry(musicPlaylist);
                //entry.Property(e => e.Name).IsModified = true;

                playlistX.Name = musicPlaylist.Name;
                db.Entry(playlistX).State = EntityState.Modified;

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
