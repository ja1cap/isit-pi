using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdManager.Models;
using Microsoft.AspNet.Identity;

namespace AdManager.Controllers
{
    [Authorize(Roles = "ADMIN,PUBLISHER")]
    public class ZonesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Zones
        public async Task<ActionResult> Index()
        {
            var userID = User.Identity.GetUserId();
            var isAdmin = User.IsInRole("ADMIN");
            IQueryable<Zone> zones = db.Zones;
            if (!isAdmin)
            {
                zones = zones.Where(z => z.UserID.Equals(userID));
            }
            zones = zones.Include(z => z.User).Include(z => z.Website);
            return View(await zones.ToListAsync());
        }

        // GET: Zones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = await db.Zones.Include("Website").SingleAsync(z => z.ID == id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        // GET: Zones/Create
        public ActionResult Create()
        {
            var userID = User.Identity.GetUserId();
            ViewBag.WebsiteID = new SelectList(db.Websites.Where(w => w.UserID.Equals(userID)).ToList(), "ID", "Name");
            return View();
        }

        // POST: Zones/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,WebsiteID,Name,AdPlacementWidth,AdPlacementHeight")] Zone zone)
        {
            if (ModelState.IsValid)
            {
                zone.UserID = User.Identity.GetUserId();

                db.Zones.Add(zone);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var userID = User.Identity.GetUserId();
            ViewBag.WebsiteID = new SelectList(db.Websites.Where(w => w.UserID.Equals(userID)).ToList(), "ID", "Name");

            return View(zone);
        }

        // GET: Zones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = await db.Zones.FindAsync(id);
            if (zone == null)
            {
                return HttpNotFound();
            }

            var userID = User.Identity.GetUserId();
            ViewBag.WebsiteID = new SelectList(db.Websites.Where(w => w.UserID.Equals(userID)).ToList(), "ID", "Name", zone.WebsiteID);

            return View(zone);
        }

        // POST: Zones/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,UserID,WebsiteID,Name,AdPlacementWidth,AdPlacementHeight")] Zone zone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zone).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var userID = User.Identity.GetUserId();
            ViewBag.WebsiteID = new SelectList(db.Websites.Where(w => w.UserID.Equals(userID)).ToList(), "ID", "Name", zone.WebsiteID);

            return View(zone);
        }

        // GET: Zones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = await db.Zones.Include("Website").SingleAsync(z => z.ID == id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Zone zone = await db.Zones.FindAsync(id);
            db.Zones.Remove(zone);
            await db.SaveChangesAsync();
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
