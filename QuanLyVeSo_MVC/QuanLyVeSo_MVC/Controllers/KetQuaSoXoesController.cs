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
    public class KetQuaSoXoesController : Controller
    {
        private QLVeSoDbContext db = new QLVeSoDbContext();

        // GET: KetQuaSoXoes
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

            var ketQuaSoXo = db.KetQuaSoXo.Include(k => k.Giai).Include(k => k.LoaiVeso).Where(x => x.Flag == true);

            if (!string.IsNullOrEmpty(searchString))
            {
                ketQuaSoXo = ketQuaSoXo.Where(s => s.SoTrung.Contains(searchString) 
                || s.Giai.TenGiai.Contains(searchString) 
                || s.LoaiVeso.Tinh.Contains(searchString));
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(ketQuaSoXo.OrderBy(i => i.MaKetQua).ToPagedList(pageNumber, pageSize));
        }

        // GET: KetQuaSoXoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQuaSoXo ketQuaSoXo = db.KetQuaSoXo.Find(id);
            if (ketQuaSoXo == null)
            {
                return HttpNotFound();
            }
            return View(ketQuaSoXo);
        }

        // GET: KetQuaSoXoes/Create
        public ActionResult Create()
        {
            ViewBag.MaGiai = new SelectList(db.Giai, "MaGiai", "TenGiai");
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVeso, "MaLoaiVeSo", "Tinh");
            return View();
        }

        // POST: KetQuaSoXoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKetQua,MaLoaiVeSo,MaGiai,NgaySo,SoTrung,Flag")] KetQuaSoXo ketQuaSoXo)
        {
            if (ModelState.IsValid)
            {
                var dal = new KetQuaSoXo_DAL();

                ketQuaSoXo.MaKetQua = dal.AutoID();

                ketQuaSoXo.Flag = true;

                db.KetQuaSoXo.Add(ketQuaSoXo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGiai = new SelectList(db.Giai, "MaGiai", "TenGiai", ketQuaSoXo.MaGiai);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVeso, "MaLoaiVeSo", "Tinh", ketQuaSoXo.MaLoaiVeSo);
            return View(ketQuaSoXo);
        }

        // GET: KetQuaSoXoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQuaSoXo ketQuaSoXo = db.KetQuaSoXo.Find(id);
            if (ketQuaSoXo == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGiai = new SelectList(db.Giai, "MaGiai", "TenGiai", ketQuaSoXo.MaGiai);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVeso, "MaLoaiVeSo", "Tinh", ketQuaSoXo.MaLoaiVeSo);
            return View(ketQuaSoXo);
        }

        // POST: KetQuaSoXoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKetQua,MaLoaiVeSo,MaGiai,NgaySo,SoTrung,Flag")] KetQuaSoXo ketQuaSoXo)
        {
            if (ModelState.IsValid)
            {
                ketQuaSoXo.Flag = true;

                db.Entry(ketQuaSoXo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaGiai = new SelectList(db.Giai, "MaGiai", "TenGiai", ketQuaSoXo.MaGiai);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVeso, "MaLoaiVeSo", "Tinh", ketQuaSoXo.MaLoaiVeSo);
            return View(ketQuaSoXo);
        }

        // GET: KetQuaSoXoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KetQuaSoXo ketQuaSoXo = db.KetQuaSoXo.Find(id);
            if (ketQuaSoXo == null)
            {
                return HttpNotFound();
            }
            return View(ketQuaSoXo);
        }

        // POST: KetQuaSoXoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KetQuaSoXo ketQuaSoXo = db.KetQuaSoXo.Find(id);

            ketQuaSoXo.Flag = false;

            db.Entry(ketQuaSoXo).State = EntityState.Modified;

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
