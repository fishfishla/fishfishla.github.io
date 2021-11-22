using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjCooking.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjCooking.Models
{
    public class CPartyForm
    {
        public t聚會 party { get; set; } = null;
        public HttpPostedFileBase image { get; set; }

        public CPartyForm()
        {
            this.party = new t聚會();

        }
        public int f聚會Id { get; set; }
        public int f主辦人 { get; set; }
        public string f聚會名稱 { get; set; }
        public string f聚會內容 { get; set; }
        public string f聚會照片 { get; set; }
        public System.DateTime f聚會日期 { get; set; }
        public System.DateTime f聚會開始時間 { get; set; }
        public Nullable<System.DateTime> f聚會結束時間 { get; set; }
        public Nullable<int> f名額 { get; set; }
        public string f聚會軟體 { get; set; }
        public string f聚會軟體URL { get; set; }
        public string f聚會關鍵字 { get; set; }
        public string f聚會通訊軟體 { get; set; }
        public string f聚會通訊軟體帳號 { get; set; }
        public Nullable<int> f聚會狀態 { get; set; }
        public Nullable<bool> f聚會垃圾桶 { get; set; }
        public System.DateTime f聚會建立日期 { get; set; }
    }
}