using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjCooking.Models;

namespace prjCooking.ViewModel
{
    public class C聚會資訊For頁面ViewModel
    {
        public t聚會 聚會資訊 { get; set; }
        public t會員 主辦人資訊 { get; set; }
        public List<t建議食材> 食材資訊List { get; set; }
        public List<C參加者資訊For聚會頁面> 參加者資訊List { get; set; }
        public List<t聚會> 最新聚會 { get; set; }
        public List<string> 聚會關鍵字 { get; set; }
        public bool Is當前會員報名 { get; set; } = false;
        public t會員 當前登入會員資訊 { get; set; }
        public int 當前會員參加者Id { get; set; }
        public bool Is成果發表 { get; set; } = false;
        public bool Is核准參與 { get; set; } = false;
        public int 聚會核准人數 { get; set; } = 0;
    }
}