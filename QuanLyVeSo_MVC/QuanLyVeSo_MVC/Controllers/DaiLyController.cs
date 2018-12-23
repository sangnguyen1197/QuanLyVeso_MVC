using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using QuanLyVeSo_MVC.DTO;
using QuanLyVeSo_MVC.Models;

namespace QuanLyVeSo_MVC.Controllers
{
    public class DaiLyController : Controller
    {
        private QLVeSoDbContext db = new QLVeSoDbContext();
        // GET: DaiLy
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

            var daily = db.DaiLy.Where(x => x.Flag == true);

            if (searchString != null)
            {
                searchString = searchString.ToLower();
                var result = db.DaiLy.Where(x => x.Flag == true).ToList();
                var s = result.Where(x=>x.TenDaiLy.ToLower().Contains(searchString)).ToList();
                int PageSize = 10;
                int PageNumber = (page ?? 1);
                return View(s.OrderBy(i => i.MaDaiLy).ToPagedList(PageNumber, PageSize));
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(daily.OrderBy(i => i.MaDaiLy).ToPagedList(pageNumber, pageSize));
        }

        // GET: Daily/Details/5
        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy daily = db.DaiLy.Find(id);
            if (daily == null)
            {
                return HttpNotFound();
            }
            return View(daily);
        }

        // GET: DaiLy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DaiLy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDaiLy,TenDaiLy,DiaChi,SDT")] DaiLyModel daily)
        {
            if (ModelState.IsValid)
            {
                DaiLy dl = new DaiLy();
                dl.TenDaiLy = daily.TenDaiLy;
                dl.SDT = daily.SDT;
                dl.DiaChi = daily.DiaChi.DuongSoNha + ", " + daily.DiaChi.PhuongXa + ", " + daily.DiaChi.QuanHuyen + ", " + daily.DiaChi.TinhTP;

                var dal = new DaiLy_DAL();
                dl.MaDaiLy = dal.AutoID();
                dl.Flag = true;
                db.DaiLy.Add(dl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(daily);
        }

        // GET: Daily/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy daily = db.DaiLy.Find(id);
            if (daily == null)
            {
                return HttpNotFound();
            }
            return View(daily);
        }

        // POST: DaiLy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDaiLy,TenDaiLy,DiaChi,SDT,Flag")] DaiLy daily)
        {
            if (ModelState.IsValid)
            {
                daily.Flag = true;

                db.Entry(daily).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(daily);
        }

        // GET: Daily/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaiLy daily = db.DaiLy.Find(id);
            if (daily == null)
            {
                return HttpNotFound();
            }
            return View(daily);
        }

        // POST: Daily/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DaiLy daily = db.DaiLy.Find(id);

            daily.Flag = false;

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
