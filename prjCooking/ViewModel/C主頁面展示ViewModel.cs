using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjCooking.Models;

namespace prjCooking.ViewModel
{
    public class C主頁面展示ViewModel
    {
        public List<t聚會> 全部聚會 { get; set; }
        public List<t聚會> 進行中聚會 { get; set; }
        public List<t聚會> 未開始聚會 { get; set; }
        public List<t聚會> 已結束聚會 { get; set; }
    }
}