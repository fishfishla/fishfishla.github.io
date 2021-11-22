using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjCooking.Models
{
    public class C上傳圖片檔案重新命名
    {
        public static string Get新名字(HttpPostedFileBase file) 
        {
            string 副檔名 = "." + file.ContentType.Substring(6);
            string newName = Guid.NewGuid().ToString() + 副檔名;
            return newName;
        }
    }
}