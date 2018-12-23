using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using QuanLyVeSo_MVC.DTO;
using QuanLyVeSo_MVC.Models;

namespace QuanLyVeSo_MVC.Controllers
{
    public class SLDangKyController : Controller
    {
        private QLVeSoDbContext db = new QLVeSoDbContext();

        // GET: SoLuongDangKy
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

            var sldk = db.SoLuongDK.Where(x => x.Flag == true).Include(x => x.DaiLy).ToList();

            if (searchString != null)
            {
                searchString = searchString.ToLower();
                var result = db.SoLuongDK.Include(x => x.DaiLy).Where(x => x.Flag == true).ToList();
                var s = result.Where(x => x.DaiLy.TenDaiLy.ToLower().Contains(searchString)).ToList();
                int PageSize = 10;
                int PageNumber = (page ?? 1);
                return View(s.OrderBy(i => i.MaSoLuongDK).ToPagedList(PageNumber, PageSize));
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(sldk.OrderBy(i => i.MaSoLuongDK).ToPagedList(pageNumber, pageSize));
        }

        // GET: SoLuongDangKy/Details/5
        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoLuongDK sldk = db.SoLuongDK.Find(id);
            if (sldk == null)
            {
                return HttpNotFound();
            }
            return View(sldk);
        }

        // GET: SoLuongDangKy/Create
        public ActionResult Create()
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy");
            return View();
        }

        // POST: SoLuongDangKy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSoLuongDK,MaDaiLy,NgayDK,SoLuongDK1,Flag")] SoLuongDK sldk)
        {
            if (ModelState.IsValid)
            {
                var dal = new SLDangKy_DAL();

                sldk.MaSoLuongDK = dal.AutoID();

                sldk.Flag = true;

                db.SoLuongDK.Add(sldk);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy", sldk.MaDaiLy);
            return View(sldk);
        }

        // GET: SoLuongDangKy/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoLuongDK sldk = db.SoLuongDK.Find(id);
            if (sldk == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy", sldk.MaDaiLy);
            return View(sldk);
        }

        // POST: SoLuongDangKy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSoLuongDK,MaDaiLy,NgayDK,SoLuongDK1,Flag")] SoLuongDK sldk)
        {
            if (ModelState.IsValid)
            {
                sldk.Flag = true;

                db.Entry(sldk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy", sldk.MaDaiLy);
            return View(sldk);
        }

        // GET: SoLuongDangKy/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoLuongDK sldk = db.SoLuongDK.Find(id);
            if (sldk == null)
            {
                return HttpNotFound();
            }
            return View(sldk);
        }

        // POST: SoLuongDangKy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SoLuongDK sldk = db.SoLuongDK.Find(id);

            sldk.Flag = false;

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
