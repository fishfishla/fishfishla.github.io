using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjCooking.ViewModel
{
    public class C新增評論for聚會頁面ViewModel
    {
        public int? 參加者Id { get; set; }
        public int? 聚會Id { get; set; }
        public HttpPostedFileBase 上傳圖片 { get; set; }
        public string 留言 { get; set; }
    }
}