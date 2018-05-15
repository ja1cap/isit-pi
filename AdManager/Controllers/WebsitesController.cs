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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace AdManager.Controllers
{
    [Authorize(Roles = "ADMIN,PUBLISHER")]
    public class WebsitesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;

        public WebsitesController()
        {
        }

        public WebsitesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Websites
        public async Task<ActionResult> Index()
        {
            var userID = User.Identity.GetUserId();
            var isAdmin = await UserManager.IsInRoleAsync(userID, "ADMIN");
            IQueryable<Website> websites = db.Websites;
            if (!isAdmin)
            {
                websites = websites.Where(w => w.UserID.Equals(userID));
            }
            websites = websites.Include(w => w.User);
            return View(await websites.ToListAsync());
        }

        // GET: Websites/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website website = await db.Websites.FindAsync(id);
            if (website == null)
            {
                return HttpNotFound();
            }
            return View(website);
        }

        // GET: Websites/Create
        public ActionResult Create()
        {
            //ViewBag.UserID = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Websites/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Url,Name,ContactName,ContactEmail")] Website website)
        {
            if (ModelState.IsValid)
            {
                //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                website.UserID = User.Identity.GetUserId();

                db.Websites.Add(website);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.UserID = new SelectList(db.Users, "Id", "Email", website.UserID);
            return View(website);
        }

        // GET: Websites/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website website = await db.Websites.FindAsync(id);
            if (website == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserID = new SelectList(db.Users, "Id", "Email", website.UserID);
            return View(website);
        }

        // POST: Websites/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Url,UserID,Name,ContactName,ContactEmail")] Website website)
        {
            if (ModelState.IsValid)
            {
                db.Entry(website).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.UserID = new SelectList(db.Users, "Id", "Email", website.UserID);
            return View(website);
        }

        // GET: Websites/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Website website = await db.Websites.FindAsync(id);
            if (website == null)
            {
                return HttpNotFound();
            }
            return View(website);
        }

        // POST: Websites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Website website = await db.Websites.FindAsync(id);
            db.Websites.Remove(website);
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
