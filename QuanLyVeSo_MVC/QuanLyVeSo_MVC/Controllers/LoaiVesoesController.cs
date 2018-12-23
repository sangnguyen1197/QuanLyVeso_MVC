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
    public class LoaiVesoesController : Controller
    {
        private QLVeSoDbContext db = new QLVeSoDbContext();

        // GET: LoaiVesoes
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

            var loaiveso = from lvs in db.LoaiVeso where lvs.Flag == true select lvs;

            if (!string.IsNullOrEmpty(searchString))
            {
                loaiveso = loaiveso.Where(s => s.Tinh.Contains(searchString));
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(loaiveso.OrderBy(i => i.MaLoaiVeSo).ToPagedList(pageNumber, pageSize));
        }

        // GET: LoaiVesoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVeso loaiVeso = db.LoaiVeso.Find(id);
            if (loaiVeso == null)
            {
                return HttpNotFound();
            }
            return View(loaiVeso);
        }

        // GET: LoaiVesoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiVesoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiVeSo,Tinh,GiaBan,Flag")] LoaiVeso loaiVeso)
        {
            if (ModelState.IsValid)
            {
                var dal = new LoaiVeSo_DAL();

                loaiVeso.MaLoaiVeSo = dal.AutoID();

                loaiVeso.Flag = true;

                db.LoaiVeso.Add(loaiVeso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiVeso);
        }

        // GET: LoaiVesoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVeso loaiVeso = db.LoaiVeso.Find(id);
            if (loaiVeso == null)
            {
                return HttpNotFound();
            }
            return View(loaiVeso);
        }

        // POST: LoaiVesoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiVeSo,Tinh,GiaBan,Flag")] LoaiVeso loaiVeso)
        {
            if (ModelState.IsValid)
            {
                loaiVeso.Flag = true;

                db.Entry(loaiVeso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiVeso);
        }

        // GET: LoaiVesoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiVeso loaiVeso = db.LoaiVeso.Find(id);
            if (loaiVeso == null)
            {
                return HttpNotFound();
            }
            return View(loaiVeso);
        }

        // POST: LoaiVesoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LoaiVeso loaiVeso = db.LoaiVeso.Find(id);

            loaiVeso.Flag = false;

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
