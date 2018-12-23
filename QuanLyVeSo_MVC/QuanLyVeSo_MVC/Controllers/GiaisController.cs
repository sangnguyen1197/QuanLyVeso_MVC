using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QuanLyVeSo_MVC.DTO;
using QuanLyVeSo_MVC.Models;

namespace QuanLyVeSo_MVC.Controllers
{
    public class GiaisController : Controller
    {
        private QLVeSoDbContext db = new QLVeSoDbContext();

        // GET: Giais
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var giai = from gi in db.Giai where gi.Flag == true select gi;

            if (!string.IsNullOrEmpty(searchString))
            {
                giai = giai.Where(s => s.TenGiai.Contains(searchString));
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(giai.OrderBy(i => i.MaGiai).ToPagedList(pageNumber, pageSize));
        }

        // GET: Giais/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giai giai = db.Giai.Find(id);
            if (giai == null)
            {
                return HttpNotFound();
            }
            return View(giai);
        }

        // GET: Giais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Giais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGiai,TenGiai,SoTienNhan,Flag")] Giai giai)
        {
            if (ModelState.IsValid)
            {
                var dal = new Giai_DAL();

                giai.MaGiai = dal.AutoID();

                giai.Flag = true;

                db.Giai.Add(giai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(giai);
        }

        // GET: Giais/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giai giai = db.Giai.Find(id);
            if (giai == null)
            {
                return HttpNotFound();
            }
            return View(giai);
        }

        // POST: Giais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGiai,TenGiai,SoTienNhan,Flag")] Giai giai)
        {
            if (ModelState.IsValid)
            {
                giai.Flag = true;

                db.Entry(giai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giai);
        }

        // GET: Giais/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giai giai = db.Giai.Find(id);
            if (giai == null)
            {
                return HttpNotFound();
            }
            return View(giai);
        }

        // POST: Giais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Giai giai = db.Giai.Find(id);

            giai.Flag = false;

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
