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
    public class PhieuChisController : Controller
    {
        private QLVeSoDbContext db = new QLVeSoDbContext();

        // GET: PhieuChis
        public ViewResult Index(string searchString, string currentFilter, int? page)
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

            var phieuchi = from pc in db.PhieuChi where pc.Flag == true select pc;

            if (!string.IsNullOrEmpty(searchString))
            {
                phieuchi = phieuchi.Where(s => s.NoiDung.Contains(searchString));
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(phieuchi.OrderBy(i => i.MaPhieuChi).ToPagedList(pageNumber, pageSize));
        }

        // GET: PhieuChis/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuChi phieuChi = db.PhieuChi.Find(id);
            if (phieuChi == null)
            {
                return HttpNotFound();
            }
            return View(phieuChi);
        }

        // GET: PhieuChis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhieuChis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhieuChi,NgayChi,NoiDung,SoTien,Flag")] PhieuChi phieuChi)
        {
            if (ModelState.IsValid)
            {
                var dal = new PhieuChi_DAL();
                
                phieuChi.MaPhieuChi = dal.AutoID();

                phieuChi.Flag = true;

                db.PhieuChi.Add(phieuChi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phieuChi);
        }

        // GET: PhieuChis/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuChi phieuChi = db.PhieuChi.Find(id);
            if (phieuChi == null)
            {
                return HttpNotFound();
            }
            return View(phieuChi);
        }

        // POST: PhieuChis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhieuChi,NgayChi,NoiDung,SoTien,Flag")] PhieuChi phieuChi)
        {
            if (ModelState.IsValid)
            {
                phieuChi.Flag = true;

                db.Entry(phieuChi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phieuChi);
        }

        // GET: PhieuChis/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuChi phieuChi = db.PhieuChi.Find(id);
            if (phieuChi == null)
            {
                return HttpNotFound();
            }
            return View(phieuChi);
        }

        // POST: PhieuChis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhieuChi phieuChi = db.PhieuChi.Find(id);

            phieuChi.Flag = false;

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
