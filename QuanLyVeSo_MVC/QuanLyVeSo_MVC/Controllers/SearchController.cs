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

namespace QuanLyVeSo_MVC.Controllers
{
    public class SearchController : Controller
    {
        private QLVeSoDbContext db = new QLVeSoDbContext();

        // GET: Search
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

            var ketQuaSoXo = db.KetQuaSoXo.Include(k => k.Giai).Include(k => k.LoaiVeso).Where(x => x.Flag == true && x.SoTrung == searchString);

            if (!string.IsNullOrEmpty(searchString))
            {
                ketQuaSoXo = ketQuaSoXo.Where(s => s.SoTrung == searchString);
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(ketQuaSoXo.OrderBy(i => i.MaKetQua).ToPagedList(pageNumber, pageSize));
        }

        // GET: Search/Details/5
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
