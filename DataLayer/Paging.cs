using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Paging
    {
        public static uint pageSize = 50;
        public static string getPaging(uint pageNumber)
        {
            string page = string.Format("page={0}&pageSize={1}", pageNumber, pageSize);
            return page;
        }
    }
}
