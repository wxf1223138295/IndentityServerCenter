using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServerTwo
{
    public class Test
    {
        

        public static double FunAdd(double d1, double d2)
        {
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            BuildDoubleToList(d1, ref list1, ref list2);
            BuildDoubleToList(d2, ref list1, ref list2); int sumlist1 = list1.Sum();
            int sumlist2 = list2.Sum();
            if (sumlist2 >= 10)
            {
                sumlist1 = sumlist1 + 1;
                sumlist2 = sumlist2 - 10;
            }
            double ret = Convert.ToDouble(sumlist1.ToString() + "." + sumlist2.ToString()); return ret;
        }

        public static void BuildDoubleToList(double value, ref List<int> list1, ref List<int> list2)
        {
            string str = value.ToString();
            System.Console.WriteLine("str:" + str);
            int index = str.IndexOf(".");
            int value1 = 0; int value2 = 0;
            if (index > 0)
            {
                value1 = Convert.ToInt32(str.Substring(0, index));
                value2 = Convert.ToInt32(str.Substring(index + 1));
            }
            else
            {
                value1 = Convert.ToInt32(str);
            }
            list1.Add(value1);
            list2.Add(value2);
        }
    }
}
