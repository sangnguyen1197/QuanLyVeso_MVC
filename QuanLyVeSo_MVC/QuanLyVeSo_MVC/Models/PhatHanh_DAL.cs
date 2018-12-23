using QuanLyVeSo_MVC.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyVeSo_MVC.Models
{
    public class PhatHanh_DAL
    {
        QLVeSoDbContext db = null;

        public PhatHanh_DAL()
        {
            db = new QLVeSoDbContext();
        }

        public decimal GetGiaVeSo(string loaiveso)
        {
            decimal giaveso = Convert.ToDecimal((from lvs in db.LoaiVeso
                                                 where lvs.MaLoaiVeSo == loaiveso
                                                 select lvs.GiaBan).SingleOrDefault());
            return giaveso;
        }

        public decimal CalcDoanhThu(string loaiveso, int slban)
        {
            decimal giaveso = GetGiaVeSo(loaiveso);
            decimal doanhthu = slban * giaveso;
            return doanhthu;
        }

        public decimal CalcTienThanhToan(decimal doanhthu, decimal hoahong)
        {
            decimal tienthanhtoan = doanhthu * (1 - (hoahong / 100));
            return tienthanhtoan;
        }

        public bool CheckCharNumber(char data)
        {
            double number;
            bool isNum = double.TryParse(data.ToString(), out number);
            return isNum;
        }

        public string AutoID()
        {
            string fullID = (from ph in db.PhatHanh
                             orderby ph.MaPhatHanh descending
                             select ph.MaPhatHanh).FirstOrDefault().ToString();

            ArrayList array = new ArrayList();

            int n = fullID.Length;
            for (int i = 0; i < n - 1; i++)
            {
                if (CheckCharNumber(fullID[i]) == false)
                {
                    array.Add(fullID[i]);
                    fullID = fullID.Replace(fullID[i], ' ');
                }
            }

            string word = string.Join("", array.ToArray());
            int id = Convert.ToInt32(fullID.Trim()) + 1;

            if (id < 10)
            {
                return word + "0" + id.ToString();
            }
            else
            {
                return word + id.ToString();
            }
        }
    }
}