﻿using System;
using System.Collections;
using System.Linq;
using QuanLyVeSo_MVC.DTO;

namespace QuanLyVeSo_MVC.Models
{
    public class CongNo_DAL
    {
        QLVeSoDbContext db = null;
        public CongNo_DAL()
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
            string fullID = (from cn in db.CongNo orderby cn.MaCongNo descending select cn.MaCongNo).FirstOrDefault().ToString();

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