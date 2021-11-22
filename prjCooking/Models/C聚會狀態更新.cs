using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjCooking.Models
{
    public class C聚會狀態更新
    {
        public static void Update()
        {
            dbCookingEntities db = new dbCookingEntities();
            List<t聚會> 聚會List = db.Cooking查詢所有沒被刪除聚會List();

            foreach (t聚會 聚會 in 聚會List)
                更新規則(聚會);
        }

        private static void 更新規則(t聚會 重整聚會狀態)
        {
            if (重整聚會狀態.f聚會開始時間 > DateTime.Now)
                重整聚會狀態.f聚會狀態 = Convert.ToInt32(聚會狀態.可報名);
            else if ((重整聚會狀態.f聚會開始時間 < DateTime.Now) && (重整聚會狀態.f聚會結束時間 > DateTime.Now))
                重整聚會狀態.f聚會狀態 = Convert.ToInt32(聚會狀態.進行中);
            else
                重整聚會狀態.f聚會狀態 = Convert.ToInt32(聚會狀態.已結束);

            (new dbCookingEntities()).Cooking修改聚會資料(重整聚會狀態);
        }
    }
}