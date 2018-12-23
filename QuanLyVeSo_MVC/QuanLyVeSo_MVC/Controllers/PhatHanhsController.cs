using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using QuanLyVeSo_MVC.DTO;
using QuanLyVeSo_MVC.Models;

namespace QuanLyVeSo_MVC.Controllers
{
    public class PhatHanhsController : Controller
    {
        private QLVeSoDbContext db = new QLVeSoDbContext();

        // GET: PhatHanhs
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

            var phatHanh = db.PhatHanh.Include(p => p.DaiLy).Include(p => p.LoaiVeso).Where(x => x.Flag == true);

            if (!string.IsNullOrEmpty(searchString))
            {
                phatHanh = phatHanh.Where(s => s.DaiLy.TenDaiLy.Contains(searchString)
                || s.LoaiVeso.Tinh.Contains(searchString));
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(phatHanh.OrderBy(i => i.MaPhatHanh).ToPagedList(pageNumber, pageSize));
        }

        // GET: PhatHanhs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhatHanh phatHanh = db.PhatHanh.Find(id);
            if (phatHanh == null)
            {
                return HttpNotFound();
            }
            return View(phatHanh);
        }

        // GET: PhatHanhs/Create
        public ActionResult Create()
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLy.Where(x=>x.Flag==true), "MaDaiLy", "TenDaiLy");
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVeso, "MaLoaiVeSo", "Tinh");
            return View();
        }

        // POST: PhatHanhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhatHanh,MaDaiLy,MaLoaiVeSo,SoLuong,NgayNhan,SLBan,DoanhThuDPH,HoaHong,TienThanhToan,Flag")] PhatHanh phatHanh)
        {

            //So luong de xuat: tinh dua tren SoLuongDK 
            //So luong duoc dang ký 
            if (ModelState.IsValid)
            {

                var dal = new PhatHanh_DAL();

                phatHanh.MaPhatHanh = dal.AutoID();
                var sl = db.SoLuongDK.Where(x => x.MaDaiLy == phatHanh.MaDaiLy && x.Flag == true).OrderByDescending(x=>x.NgayDK).Take(1).FirstOrDefault().SoLuongDK1;
                if (phatHanh.SoLuong == null)
                    phatHanh.SoLuong = sl;
                else
                {
                    double tk = thongke(phatHanh.MaDaiLy);
                    if (tk < 0.75 && phatHanh.SoLuong > sl)
                    {
                        //Error
                        ModelState.AddModelError(string.Empty, "Phần trăm doanh số của đại lý này là "+tk+". Không thể tăng số lượng vé số phát hành");
                        return View(phatHanh);
                    }
                }
                phatHanh.DoanhThuDPH = dal.CalcDoanhThu(phatHanh.MaLoaiVeSo, Convert.ToInt32(phatHanh.SLBan));

                phatHanh.TienThanhToan = dal.CalcTienThanhToan(Convert.ToDecimal(phatHanh.DoanhThuDPH), Convert.ToDecimal(phatHanh.HoaHong));

                phatHanh.Flag = true;

                db.PhatHanh.Add(phatHanh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy", phatHanh.MaDaiLy);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVeso, "MaLoaiVeSo", "Tinh", phatHanh.MaLoaiVeSo);
            return View(phatHanh);
        }

        private double thongke(string MaDaiLy)
        {
            var phathanhs = db.PhatHanh.Where(x => x.MaDaiLy == MaDaiLy && x.NgayNhan < DateTime.Now).OrderByDescending(x => x.NgayNhan).Take(3).ToList();
            int tongSoLuong = 0;
            int tongSLBan = 0;
            foreach(PhatHanh i in phathanhs)
            {
                tongSoLuong += i.SoLuong.GetValueOrDefault();
                tongSLBan += i.SLBan.GetValueOrDefault();
            }
            if (tongSoLuong == 0) return 1;
            return tongSLBan*(1.0) / tongSoLuong;
        }

        // GET: PhatHanhs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhatHanh phatHanh = db.PhatHanh.Find(id);
            if (phatHanh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy", phatHanh.MaDaiLy);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVeso, "MaLoaiVeSo", "Tinh", phatHanh.MaLoaiVeSo);
            return View(phatHanh);
        }

        // POST: PhatHanhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhatHanh,MaDaiLy,MaLoaiVeSo,SoLuong,NgayNhan,SLBan,DoanhThuDPH,HoaHong,TienThanhToan,Flag")] PhatHanh phatHanh)
        {
            if (ModelState.IsValid)
            {
                phatHanh.Flag = true;

                var sl = db.SoLuongDK.Where(x => x.MaDaiLy == phatHanh.MaDaiLy && x.Flag == true).OrderByDescending(x => x.NgayDK).Take(1).FirstOrDefault().SoLuongDK1;
                if (phatHanh.SoLuong == null)
                    phatHanh.SoLuong = sl;
                else
                {
                    double tk = thongke(phatHanh.MaDaiLy);
                    if (tk < 75 && phatHanh.SoLuong > sl)
                    {
                        //Error
                        ModelState.AddModelError(string.Empty, "Phần trăm doanh số của đại lý này là " + tk + ". Không thể tăng số lượng vé số phát hành");
                        return View(phatHanh);
                    }
                }

                db.Entry(phatHanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLy, "MaDaiLy", "TenDaiLy", phatHanh.MaDaiLy);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVeso, "MaLoaiVeSo", "Tinh", phatHanh.MaLoaiVeSo);
            return View(phatHanh);
        }

        // GET: PhatHanhs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhatHanh phatHanh = db.PhatHanh.Find(id);
            if (phatHanh == null)
            {
                return HttpNotFound();
            }
            return View(phatHanh);
        }

        // POST: PhatHanhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhatHanh phatHanh = db.PhatHanh.Find(id);

            phatHanh.Flag = false;

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
