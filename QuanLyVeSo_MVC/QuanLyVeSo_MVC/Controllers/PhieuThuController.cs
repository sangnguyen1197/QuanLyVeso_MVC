using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using QuanLyVeSo_MVC.DTO;
using QuanLyVeSo_MVC.Models;

namespace QuanLyVeSo_MVC.Controllers
{
    public class PhieuThuController : Controller
    {
        private QLVeSoDbContext db = new QLVeSoDbContext();

        // GET: PhieuThu
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

            var phieuthu = db.PhieuThu.Where(x => x.Flag == true).Include(x => x.DaiLy);

            if (searchString != null)
            {
                searchString = searchString.ToLower();
                var result = db.PhieuThu.Include(x => x.DaiLy).Where(x => x.Flag == true).ToList();
                var s = result.Where(x => x.DaiLy.TenDaiLy.ToLower().Contains(searchString)).ToList();
                int PageSize = 10;
                int PageNumber = (page ?? 1);
                return View(s.OrderBy(i => i.MaPhieuThu).ToPagedList(PageNumber, PageSize));
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(phieuthu.OrderBy(i => i.MaPhieuThu).ToPagedList(pageNumber, pageSize));
        }

        // GET: PhieuThu/Details/5
        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThu phieuthu = db.PhieuThu.Find(id);
            if (phieuthu == null)
            {
                return HttpNotFound();
            }
            return View(phieuthu);
        }

        // GET: PhieuThu/Create
        public ActionResult Create()
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy");
            return View();
        }

        // POST: PhieuThu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhieuThu,MaDaiLy,NgayNop,SoTienNop,Flag")] PhieuThu phieuthu)
        {
            if (ModelState.IsValid)
            {
                var dal = new PhieuThu_DAL();

                phieuthu.MaPhieuThu = dal.AutoID();

                phieuthu.Flag = true;

                db.PhieuThu.Add(phieuthu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy", phieuthu.MaDaiLy);
            return View(phieuthu);
        }

        // GET: PhieuThu/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThu phieuthu = db.PhieuThu.Find(id);
            if (phieuthu == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy", phieuthu.MaDaiLy);
            return View(phieuthu);
        }

        // POST: PhieuThu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhieuThu,MaDaiLy,NgayNop,SoTienNop,Flag")] PhieuThu phieuthu)
        {
            if (ModelState.IsValid)
            {
                phieuthu.Flag = true;

                db.Entry(phieuthu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy", phieuthu.MaDaiLy);
            return View(phieuthu);
        }

        // GET: PhieuThu/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThu phieuthu = db.PhieuThu.Find(id);
            if (phieuthu == null)
            {
                return HttpNotFound();
            }
            return View(phieuthu);
        }

        // POST: PhieuThu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhieuThu phieuthu = db.PhieuThu.Find(id);

            phieuthu.Flag = false;

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
