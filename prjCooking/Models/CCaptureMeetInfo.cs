// 撈取資料的資訊
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjCooking.Models
{
    public class CCaptureMeetInfo
    {
        public int? 聚會ID { get; set; }
        public string 聚會狀態Name { get; set; }
        public int? 聚會狀態Number { get; set; }
        public DateTime 聚會日期 { get; set; }
        public string 聚會名稱 { get; set; }
        public string 主辦人 { get; set; }
        public int? 人數上限 { get; set; }
        public int? 目前人數 { get; set; }
        public bool Has評價 { get; set; }
        public bool? Is核准 { get; set; }
        public bool? Is報名 { get; set; }
    }
}