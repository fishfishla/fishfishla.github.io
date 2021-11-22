using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjCooking.Models
{
    public static class CdbCookingEntity擴充語法
    {
        // Reader
        public static List<t聚會> Cooking查詢某會員被檢舉下架的聚會ListBy會員Id(this dbCookingEntities db, int? 會員Id)
        {
            List<t聚會> 某會員聚會List = db.t聚會.Where(聚會 => (聚會.f主辦人 == 會員Id.Value) && (聚會.f聚會垃圾桶 == true)&&(聚會.f聚會狀態==5)).ToList();

            return 某會員聚會List;
        }
        public static List<t聚會> Cooking查詢某會員沒被刪除的聚會ListBy會員Id(this dbCookingEntities db, int? 會員Id)
        {
            List<t聚會> 某會員聚會List = db.t聚會.Where(聚會 => (聚會.f主辦人 == 會員Id.Value) && (聚會.f聚會垃圾桶 == false)).ToList();

            return 某會員聚會List;
        }
        public static List<t會員> Cooking查詢所有會員List(this dbCookingEntities db)
        {
            List<t會員> 所有會員List = db.t會員.Select(會員 => 會員).ToList();

            return 所有會員List;
        }

        public static t會員 Cooking查詢某會員的資料By信箱And密碼(this dbCookingEntities db, string 會員信箱, string 會員密碼)
        {
            t會員 某會員 = db.t會員.Where(會員 => 會員.f會員信箱 == 會員信箱)
                                 .Where(會員 => 會員.f會員密碼 == 會員密碼)
                                 .FirstOrDefault();

            return 某會員;
        }

        public static t會員 Cooking查詢某會員的資料By會員Id(this dbCookingEntities db, int? 會員Id)
        {
            t會員 某會員 = db.t會員.Where(會員 => 會員.f會員Id == 會員Id.Value).FirstOrDefault();

            return 某會員;
        }

        public static List<t聚會> Cooking查詢所有聚會List(this dbCookingEntities db)
        {
            List<t聚會> 所有聚會List = db.t聚會.Select(聚會 => 聚會).ToList();

            return 所有聚會List;
        }

        public static List<t聚會> Cooking查詢所有沒被刪除聚會List(this dbCookingEntities db)
        {
            bool is聚會刪除 = Convert.ToBoolean(聚會垃圾桶.刪除);
            List<t聚會> 所有聚會List = db.t聚會.Where(聚會 => 聚會.f聚會垃圾桶 != is聚會刪除).Select(聚會 => 聚會).ToList();

            return 所有聚會List;
        }

        public static List<t聚會> Cooking查詢某會員聚會ListBy會員Id(this dbCookingEntities db, int? 會員Id)
        {
            List<t聚會> 某會員聚會List = db.t聚會.Where(聚會 => 聚會.f主辦人 == 會員Id.Value).ToList();

            return 某會員聚會List;
        }

        public static t聚會 Cooking查詢某聚會資訊By聚會Id(this dbCookingEntities db, int? 聚會Id)
        {
            t聚會 某聚會 = db.t聚會.Where(聚會 => 聚會.f聚會Id == 聚會Id.Value).FirstOrDefault();

            return 某聚會;
        }

        public static List<t建議食材> Cooking查詢某聚會的食材ListBy聚會Id(this dbCookingEntities db, int? 聚會Id)
        {
            List<t建議食材> 某聚會食材List = db.t建議食材.Where(食材 => 食材.f聚會Id == 聚會Id.Value).ToList();

            return 某聚會食材List;
        }

        public static List<t參加者> Cooking查詢某會員報名紀錄ListBy會員Id(this dbCookingEntities db, int? 會員Id)
        {
            List<t參加者> 某會員的報名紀錄 = db.t參加者.Where(參加者 => 參加者.f會員Id == 會員Id.Value).ToList();

            return 某會員的報名紀錄;
        }

        public static List<t參加者> Cooking查詢某聚會所有參與者ListBy聚會Id(this dbCookingEntities db, int? 聚會Id)
        {
            bool 通過 = Convert.ToBoolean(參加者審核狀態.通過);
            List<t參加者> 某聚會的參與者 = db.t參加者.Where(參加者 => 參加者.f聚會Id == 聚會Id)
                                                  .ToList();

            return 某聚會的參與者;
        }

        public static List<t參加者> Cooking查詢某聚會核准參與者ListBy聚會Id(this dbCookingEntities db, int? 聚會Id)
        {
            bool 通過 = Convert.ToBoolean(參加者審核狀態.通過);
            List<t參加者> 某聚會的參與者 = db.t參加者.Where(參加者 => 參加者.f聚會Id == 聚會Id)
                                                  .Where(參加者 => 參加者.f審核狀態 == 通過)
                                                  .Where(參加者 => 參加者.f報名 != true)
                                                  .ToList();

            return 某聚會的參與者;
        }

        public static List<t評價> Cooking查詢某聚會的評價ListBy聚會Id(this dbCookingEntities db, int? 聚會Id)
        {
            List<t評價> 某聚會 = db.t評價.Where(評價 => 評價.f聚會Id == 聚會Id.Value).ToList();

            return 某聚會;
        }

        // Create
        public static void Cooking新增某表格資料<t表格>(this dbCookingEntities db ,t表格 新增的一筆資料)
        {
            if(新增的一筆資料 != null)
            {
                db.Set(typeof(t表格)).Add(新增的一筆資料);
                db.SaveChanges();
            }
        }

        // Update
        public static void Cooking修改會員資料(this dbCookingEntities db, t會員 會員更新資料)
        {
            t會員 某會員 = db.t會員.Where(會員 => 會員.f會員Id == 會員更新資料.f會員Id).FirstOrDefault();

            if (某會員 != null)
            {
                if (某會員.f性別 != 會員更新資料.f性別)
                    某會員.f性別 = 會員更新資料.f性別;
                if (某會員.f會員姓名 != 會員更新資料.f會員姓名)
                    某會員.f會員姓名 = 會員更新資料.f會員姓名;
                if (某會員.f會員密碼 != 會員更新資料.f會員密碼)
                    某會員.f會員密碼 = 會員更新資料.f會員密碼;
                if (某會員.f會員照片 != 會員更新資料.f會員照片)
                    某會員.f會員照片 = 會員更新資料.f會員照片;
                if (某會員.f會員電話 != 會員更新資料.f會員電話)
                    某會員.f會員電話 = 會員更新資料.f會員電話;
                if (某會員.f權限 != 會員更新資料.f權限)
                    某會員.f權限 = 會員更新資料.f權限;
                if (某會員.f自我介紹 != 會員更新資料.f自我介紹)
                    某會員.f自我介紹 = 會員更新資料.f自我介紹;

                db.SaveChanges();
            }
        }

        public static void Cooking修改聚會資料(this dbCookingEntities db, t聚會 聚會更新資料)
        {
            t聚會 某聚會 = db.t聚會.Where(聚會 => 聚會.f聚會Id == 聚會更新資料.f聚會Id).FirstOrDefault();

            if(某聚會 != null)
            {
                if (某聚會.f名額 != 聚會更新資料.f名額)
                    某聚會.f名額 = 聚會更新資料.f名額;
                if (某聚會.f聚會內容 != 聚會更新資料.f聚會內容)
                    某聚會.f聚會內容 = 聚會更新資料.f聚會內容;
                if (某聚會.f聚會名稱 != 聚會更新資料.f聚會名稱)
                    某聚會.f聚會名稱 = 聚會更新資料.f聚會名稱;
                if (某聚會.f聚會垃圾桶 != 聚會更新資料.f聚會垃圾桶)
                    某聚會.f聚會垃圾桶 = 聚會更新資料.f聚會垃圾桶;
                if (某聚會.f聚會日期 != 聚會更新資料.f聚會日期)
                    某聚會.f聚會日期 = 聚會更新資料.f聚會日期;
                if (某聚會.f聚會照片 != 聚會更新資料.f聚會照片)
                    某聚會.f聚會照片 = 聚會更新資料.f聚會照片;
                if (某聚會.f聚會狀態 != 聚會更新資料.f聚會狀態)
                    某聚會.f聚會狀態 = 聚會更新資料.f聚會狀態;
                if (某聚會.f聚會結束時間 != 聚會更新資料.f聚會結束時間)
                    某聚會.f聚會結束時間 = 聚會更新資料.f聚會結束時間;
                if (某聚會.f聚會軟體 != 聚會更新資料.f聚會軟體)
                    某聚會.f聚會軟體 = 聚會更新資料.f聚會軟體;
                if (某聚會.f聚會軟體URL != 聚會更新資料.f聚會軟體URL)
                    某聚會.f聚會軟體URL = 聚會更新資料.f聚會軟體URL;
                if (某聚會.f聚會通訊軟體帳號 != 聚會更新資料.f聚會通訊軟體帳號)
                    某聚會.f聚會通訊軟體帳號 = 聚會更新資料.f聚會通訊軟體帳號;
                if (某聚會.f聚會開始時間 != 聚會更新資料.f聚會開始時間)
                    某聚會.f聚會開始時間 = 聚會更新資料.f聚會開始時間;
                if (某聚會.f聚會關鍵字 != 聚會更新資料.f聚會關鍵字)
                    某聚會.f聚會關鍵字 = 聚會更新資料.f聚會關鍵字;

                db.SaveChanges();
            }
        }

        public static void Cooking修改參加者資料(this dbCookingEntities db, t參加者 參加者更新資料)
        {
            t參加者 某參加者 = db.t參加者.Where(參加者 => 參加者.f參加者Id == 參加者更新資料.f參加者Id).FirstOrDefault();

            if(某參加者 != null)
            {
                if (某參加者.f報名 != 參加者更新資料.f報名)
                    某參加者.f報名 = 參加者更新資料.f報名;
                if (某參加者.f審核狀態 != 參加者更新資料.f審核狀態)
                    某參加者.f審核狀態 = 參加者更新資料.f審核狀態;

                db.SaveChanges();
            }
        }

        // Delete
        public static void Cooking刪除一則留言By留言Id(this dbCookingEntities db, int? 留言Id)
        {
            if (留言Id.HasValue)
            {
                t留言 某留言 = db.t留言.Where(留言 => 留言.f留言Id == 留言Id.Value).FirstOrDefault();

                if(某留言 != null)
                {
                    db.t留言.Remove(某留言);
                    db.SaveChanges();
                }
            }
        }
    }
}