using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using QuanLyVeSo_MVC.DTO;
using QuanLyVeSo_MVC.Models;

namespace QuanLyVeSo_MVC.Controllers
{
    public class CongNoController : Controller
    {
        private QLVeSoDbContext db = new QLVeSoDbContext();

        // GET: CongNo
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

            var congno = db.CongNo.Where(x => x.Flag == true).Include(x=>x.DaiLy);

            if (searchString != null)
            {
                searchString = searchString.ToLower();
                var result = db.CongNo.Include(x => x.DaiLy).Where(x => x.Flag == true).ToList();
                var s = result.Where(x => x.DaiLy.TenDaiLy.ToLower().Contains(searchString)).ToList();
                int PageSize = 10;
                int PageNumber = (page ?? 1);
                return View(s.OrderBy(i => i.MaCongNo).ToPagedList(PageNumber, PageSize));
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(congno.OrderBy(i => i.MaCongNo).ToPagedList(pageNumber, pageSize));
        }

        // GET: CongNo/Details/5
        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongNo congno = db.CongNo.Find(id);
            if (congno == null)
            {
                return HttpNotFound();
            }
            return View(congno);
        }

        // GET: CongNo/Create
        public ActionResult Create()
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy");
            return View();
        }

        // POST: CongNo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCongNo,MaDaiLy,NgayNo,SoTienNo,Flag")] CongNo congno)
        {
            if (ModelState.IsValid)
            {
                var dal = new CongNo_DAL();

                congno.MaCongNo = dal.AutoID();

                congno.Flag = true;

                db.CongNo.Add(congno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy", congno.MaDaiLy);
            return View(congno);
        }

        // GET: CongNo/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongNo congno = db.CongNo.Find(id);
            if (congno == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy", congno.MaDaiLy);
            return View(congno);
        }

        // POST: CongNo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCongNo,MaDaiLy,NgayNo,SoTienNo,Flag")] CongNo congno)
        {
            if (ModelState.IsValid)
            {
                congno.Flag = true;

                db.Entry(congno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy", congno.MaDaiLy);
            return View(congno);
        }

        // GET: CongNo/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongNo congno = db.CongNo.Find(id);
            if (congno == null)
            {
                return HttpNotFound();
            }
            return View(congno);
        }

        // POST: CongNo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CongNo congno = db.CongNo.Find(id);

            congno.Flag = false;

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
