using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.XPath;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32.SafeHandles;

namespace ConsoleApp1
{
        class Program
        {
            static void Main(string[] args)
            {
                NorthwindContext context = new NorthwindContext();

            string keyword = "1996-8-14";
            string stringQuery = $"CompanyName.Contains(\"{keyword}\") || ContactName.Contains(\"{keyword}\")";

            stringQuery = $"shipname.contains(\"{keyword}\")";

            stringQuery = $"Freight.tostring().contains(\"{keyword}\")";

            DateTime result = new DateTime();
            bool a = DateTime.TryParse(keyword, out result);


            if (a)
            {
                var n = GetWords(result, keyword);
                var lstRange = getRange(result, n);

                if (lstRange.Count>1)
                {
                    //stringQuery = $"orderdate.contains(#{keyword}#)";

                    stringQuery = $"orderdate>=(\"{lstRange[0].ToShortDateString()}\") && orderdate<(\"{lstRange[1].ToShortDateString()}\")";
                }

            


            }

           // stringQuery = $"orderdate>(\"{result}\")";

            var list = context.Orders.Where(stringQuery).ToList();

                Console.WriteLine(list.Count());

                Console.ReadLine();
            }     
        
           public static int GetWords(DateTime date, string keyword)
            {
                char[] split = new char[] { '.', '-', '/'};
                string[] sDateTime = date.ToShortDateString().Split(split);
                string[] sKeyword = keyword.Split(split);

                int num1, num2;
                for (int i = 0; i < sDateTime.Length; i++)
                {

                    if (i > sKeyword.Length-1) return i ;

                    var  b1 = int.TryParse(sDateTime[i], out num1);
                    var  b2 = int.TryParse(sKeyword[i], out num2);

                    if (b1 && b2 == false) return i ;
                    if (num1 != num2) return i;                   
                }
            return sDateTime.Length;
            }

        static List<DateTime> getRange(DateTime date, int i)
        {

            DateTime  date2;
            switch (i)
            {
                case 1:             //year                 
                    date2 = date.AddYears(1);
                    break;
                case 2:             //month                   
                    date2 = date.AddMonths(1);
                    break;
                case 3:             //day
                    date2 = date.AddDays(1);
                    break;
                case 4:             //hour
                    date2 = date.AddHours(1);
                    break;
                case 5:             //minute
                    date2 = date.AddMinutes(1);     
                    break;
                default:
                    date2 = date.AddSeconds(1);
                    break;
            }

            List<DateTime> lstDate = new List<DateTime>();
            lstDate.Add(date);
            lstDate.Add(date2);
            return lstDate;
        }




        }


 }

