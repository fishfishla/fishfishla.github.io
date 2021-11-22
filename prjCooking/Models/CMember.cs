using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjCooking.Models
{
    public class CMember
    {
        public int fId { get; set; }
        public string 信箱 { get; set; }
        public string 密碼 { get; set; }
        public string 姓名 { get; set; }
        public int 性別 { get; set; }

        
    }
}