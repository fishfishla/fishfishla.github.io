using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjCooking.ViewModel;

namespace prjCooking.Models
{
    public class C聚會相關操作
    {
        private dbCookingEntities _db;

        public C聚會相關操作()
        {
            _db = new dbCookingEntities();
        }
        public void 修改黑名單的聚會狀態(int 會員Id)
        {
            List<t聚會> 聚會 = _db.Cooking查詢某會員聚會ListBy會員Id(會員Id);

            if (聚會 != null)
            {
                foreach(var p in 聚會)
                {
                    p.f聚會垃圾桶 = true;
                    p.f聚會狀態 = 5;
                    _db.Cooking修改聚會資料(p);
                }
            }
        }
        public void 修改會員權限(int 會員Id)
        {
            t會員 會員 = _db.Cooking查詢某會員的資料By會員Id(會員Id);

            if (會員 != null)
            {
                會員.f權限 = 2;
                _db.Cooking修改會員資料(會員);
            }
        }
        public void 恢復會員權限(int 會員Id)
        {
            t會員 會員 = _db.Cooking查詢某會員的資料By會員Id(會員Id);

            if (會員 != null)
            {
                會員.f權限 = 0;
                _db.Cooking修改會員資料(會員);
            }
        }

        public void 取消報名(int 會員Id, int 聚會Id)
        {
            t參加者 某參加者報名紀錄 = 某一個參加者(聚會Id, 會員Id);

            if (某參加者報名紀錄 != null)
            {
                某參加者報名紀錄.f報名 = true;
                _db.Cooking修改參加者資料(某參加者報名紀錄);
            }  
        }

        public void 取消活動(int 聚會Id)
        {
            t聚會 取消的活動 = _db.Cooking查詢某聚會資訊By聚會Id(聚會Id);

            if(取消的活動 != null)
            {
                取消的活動.f聚會垃圾桶 = Convert.ToBoolean(聚會垃圾桶.刪除);
                _db.Cooking修改聚會資料(取消的活動);
            }  
        }
        public void 取消檢舉聚會(int 聚會Id)
        {
            t聚會 取消的活動 = _db.Cooking查詢某聚會資訊By聚會Id(聚會Id);

            if (取消的活動 != null)
            {
                取消的活動.f聚會狀態 = 5;
                取消的活動.f聚會垃圾桶 = Convert.ToBoolean(聚會垃圾桶.刪除);
                _db.Cooking修改聚會資料(取消的活動);
            }
        }

        public void 核准參加者(int 聚會Id, int 參加者Id) 
        {
            t參加者 參加者 = 某一個參加者(聚會Id, 參加者Id);

            if (參加者 != null)
            {
                參加者.f審核狀態 = Convert.ToBoolean(參加者審核狀態.通過);

                _db.Cooking修改參加者資料(參加者);
            }
        }

        private t參加者 某一個參加者(int 聚會Id, int 參加者Id)
        {
            List<t參加者> 某會員的報名紀錄 = _db.Cooking查詢某會員報名紀錄ListBy會員Id(參加者Id);
            某會員的報名紀錄.Reverse();
            foreach (t參加者 某參加者報名紀錄 in 某會員的報名紀錄)
            {
                if (某參加者報名紀錄.f聚會Id == 聚會Id)
                {
                    return 某參加者報名紀錄;
                }
            }

            return null;
        }

        public C聚會資訊For頁面ViewModel 撈取單一聚會資訊(int 聚會Id)
        {
            C聚會資訊For頁面ViewModel 單一聚會資訊 = new C聚會資訊For頁面ViewModel();
            單一聚會資訊.聚會資訊 = _db.Cooking查詢某聚會資訊By聚會Id(聚會Id);
            if(單一聚會資訊.聚會資訊 != null)
            {
                單一聚會資訊.主辦人資訊 = _db.Cooking查詢某會員的資料By會員Id(單一聚會資訊.聚會資訊.f主辦人);
                單一聚會資訊.食材資訊List = _db.Cooking查詢某聚會的食材ListBy聚會Id(聚會Id);
                單一聚會資訊.最新聚會 = _db.Cooking查詢所有沒被刪除聚會List();
                單一聚會資訊.最新聚會.Reverse();
                單一聚會資訊.參加者資訊List = Get參加者資訊List(聚會Id);
                單一聚會資訊.聚會核准人數 = _db.Cooking查詢某聚會核准參與者ListBy聚會Id(聚會Id).Count;
                if(!string.IsNullOrEmpty(單一聚會資訊.聚會資訊.f聚會關鍵字) && 單一聚會資訊.聚會資訊.f聚會關鍵字 != "")
                    單一聚會資訊.聚會關鍵字 = 單一聚會資訊.聚會資訊.f聚會關鍵字.Substring(1).Split('#').ToList();

                return 單一聚會資訊;
            }

            return null;
        }

        private List<C參加者資訊For聚會頁面> Get參加者資訊List(int 聚會Id)
        {
            List<C參加者資訊For聚會頁面> 參加者資訊List = new List<C參加者資訊For聚會頁面>();

            List<t參加者> 參加者List = _db.Cooking查詢某聚會所有參與者ListBy聚會Id(聚會Id);
            List<t評價> 參加者評論List = _db.Cooking查詢某聚會的評價ListBy聚會Id(聚會Id);
            foreach (t參加者 參加者 in 參加者List)
            {
                C參加者資訊For聚會頁面 temp = new C參加者資訊For聚會頁面();
                temp.參加者資料 = 參加者;
                temp.參加者資訊 = _db.Cooking查詢某會員的資料By會員Id(參加者.f會員Id);
                
                foreach(t評價 評論 in 參加者評論List)
                {
                    if(評論.f聚會Id == 聚會Id && 評論.f參加者Id == 參加者.f參加者Id)
                    {
                        temp.評論 = 評論;
                        break;
                    }
                }

                參加者資訊List.Add(temp);
            }

            return 參加者資訊List;
        }
    }
}