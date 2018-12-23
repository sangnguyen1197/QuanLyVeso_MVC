using QuanLyVeSo_MVC.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace QuanLyVeSo_MVC.Models
{
    public class Giai_DAL
    {
        QLVeSoDbContext db = null;

        public Giai_DAL()
        {
            db = new QLVeSoDbContext();
        }

        public string AutoID()
        {
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            int length = 3;
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return "GI" + result.ToString();
        }
    }
}