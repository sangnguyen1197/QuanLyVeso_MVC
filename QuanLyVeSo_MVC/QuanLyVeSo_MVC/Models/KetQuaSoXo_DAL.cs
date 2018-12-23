using QuanLyVeSo_MVC.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyVeSo_MVC.Models
{
    public class KetQuaSoXo_DAL
    {
        QLVeSoDbContext db = null;

        public KetQuaSoXo_DAL()
        {
            db = new QLVeSoDbContext();
        }

        public bool CheckCharNumber(char data)
        {
            double number;
            bool isNum = double.TryParse(data.ToString(), out number);
            return isNum;
        }

        public string AutoID()
        {
            string fullID = (from kqxs in db.KetQuaSoXo orderby kqxs.MaKetQua descending
                             select kqxs.MaKetQua).FirstOrDefault().ToString();

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