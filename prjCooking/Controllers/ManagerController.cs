using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjCooking.Models;
using prjCooking.ViewModel;

namespace prjCooking.Controllers
{
    public class ManagerController : Controller
    {
        public ActionResult query檢舉()
        {
            dbCookingEntities db = new dbCookingEntities();
            var query檢舉 = (from prod in db.t檢舉
                           select prod.f被檢舉的聚會Id).Distinct().ToList();

            IEnumerable<t聚會> datas = null;
            int id = 0;
            List<C管理者聚會ViewModel> Plist = new List<C管理者聚會ViewModel>();
            string keyword = Request.Form["txtKeyword"];
            if (string.IsNullOrEmpty(keyword))
            {
                foreach (int m in query檢舉)
                {
                    //m.f被檢舉的聚會Id = id;
                    datas = (from p in (new dbCookingEntities()).t聚會 where p.f聚會Id == m select p);
                    //聚會資料放進viewmodel
                    foreach (t聚會 p in datas)
                    {
                        Plist.Add(new C管理者聚會ViewModel() { party = p });

                    }
                }
            }
            else
            {
                datas = from p in (new dbCookingEntities()).t聚會 where p.f聚會名稱.Contains(keyword) select p;
                foreach (t聚會 p in datas)
                {
                    Plist.Add(new C管理者聚會ViewModel() { party = p });

                }
            }
                

            

        

            //修正部分viewmodel顯示資訊
            foreach (C管理者聚會ViewModel p in Plist)
            {
                t會員 member = (new dbCookingEntities()).Cooking查詢某會員的資料By會員Id(p.f主辦人);

                t檢舉 query檢舉p = new t檢舉();
                query檢舉p = db.t檢舉.FirstOrDefault(m => m.f被檢舉的聚會Id == p.f聚會Id);

                p.聚會建立日期 = Convert.ToDateTime(p.f聚會建立日期).ToShortDateString();

                p.煮辦人姓名 = member.f會員姓名;

                string party狀態;
                if (p.f聚會狀態 == Convert.ToInt32(聚會狀態.可報名))
                    party狀態 = "可報名";
                else if (p.f聚會狀態 == Convert.ToInt32(聚會狀態.進行中))
                    party狀態 = "進行中";
                else
                    party狀態 = "已結束";
                p.聚會狀態 = party狀態;
                if (p.f聚會垃圾桶 == false)
                    p.聚會垃圾桶 = " ";
                else
                    p.聚會垃圾桶 = "✘";

                if (query檢舉p != null)
                {

                  
                        p.聚會檢舉狀態顯示 = "已檢舉";

                }
                else
                {
                    p.聚會檢舉狀態顯示 = " ";

                }

            }
            return View(Plist);
        }
        public ActionResult 檢舉頁面(int? id)
        {
            ViewBag.f聚會id = id;
            dbCookingEntities db = new dbCookingEntities();
            t檢舉 query檢舉 = new t檢舉();
            query檢舉 = db.t檢舉.FirstOrDefault(m => m.f被檢舉的聚會Id == id);
            if (query檢舉 != null)
            {
                ViewBag.f檢舉日期 = query檢舉.f檢舉建立日期;
                ViewBag.f檢舉原因 = query檢舉.f檢舉原因;
            }
               
            return View();
        }
        public ActionResult 下架檢舉的活動(int? id)
        {
            
            (new C聚會相關操作()).取消檢舉聚會(id.Value);
            dbCookingEntities db = new dbCookingEntities();
            var query檢舉 = (from prod in db.t檢舉
                           where prod.f被檢舉的聚會Id == id
                           select prod).ToList();


            foreach (t檢舉 p in query檢舉)
            {
                //p.f檢舉原因 = DateTime.Today.ToString()+"已處理";
                db.t檢舉.Remove(p);
                db.SaveChanges();
            }


            return RedirectToAction("PartyList");

        }
        public ActionResult no下架檢舉的活動(int? id)
        {
            dbCookingEntities db = new dbCookingEntities();
            var query檢舉 = (from prod in db.t檢舉
                           where prod.f被檢舉的聚會Id == id
                           select prod).ToList();


            foreach (t檢舉 p in query檢舉)
            {
                //p.f檢舉原因 = DateTime.Today.ToString()+"已處理";
                db.t檢舉.Remove(p);
                db.SaveChanges();
            }

            return RedirectToAction("PartyList");

        }

        // GET: Manager
        public ActionResult PartyList()
        {
            IEnumerable<t聚會> datas = null;
            string keyword = Request.Form["txtKeyword"];
            if (string.IsNullOrEmpty(keyword))
                datas = from p in (new dbCookingEntities()).t聚會 select p;
            else
                datas = from p in (new dbCookingEntities()).t聚會 where p.f聚會名稱.Contains(keyword) select p;

            List<C管理者聚會ViewModel> Plist = new List<C管理者聚會ViewModel>();

            //聚會資料放進viewmodel
            foreach (t聚會 p in datas)
            {
                Plist.Add(new C管理者聚會ViewModel() { party = p });

            }
            //修正部分viewmodel顯示資訊
            foreach (C管理者聚會ViewModel p in Plist)
            {
                t會員 member = (new dbCookingEntities()).Cooking查詢某會員的資料By會員Id(p.f主辦人);
                dbCookingEntities db = new dbCookingEntities();
                t檢舉 query檢舉 = new t檢舉();
                query檢舉 = db.t檢舉.FirstOrDefault(m => m.f被檢舉的聚會Id == p.f聚會Id);

                p.聚會建立日期 = Convert.ToDateTime(p.f聚會建立日期).ToShortDateString();

                p.煮辦人姓名 = member.f會員姓名;

                string party狀態;
                if (p.f聚會狀態 == Convert.ToInt32(聚會狀態.可報名))
                    party狀態 = "可報名";
                else if (p.f聚會狀態 == Convert.ToInt32(聚會狀態.進行中))
                    party狀態 = "進行中";
                else
                    party狀態 = "已結束";
                p.聚會狀態 = party狀態;
                if (p.f聚會垃圾桶 == false)
                    p.聚會垃圾桶 = " ";
                else
                    p.聚會垃圾桶 = "✘";

                if (query檢舉 != null)
                {                
                        p.聚會檢舉狀態顯示 = "已檢舉";
                }
                else
                {
                    p.聚會檢舉狀態顯示 = " ";
                }



            }


            return View(Plist);
        }
        public ActionResult Details(int id,string 權限,string 性別)
        {
 
            dbCookingEntities db = new dbCookingEntities();
            t會員 member_select = db.t會員.FirstOrDefault(p => p.f會員Id == id);
            if (member_select == null)
                return RedirectToAction("List");

            return View(new C管理者會員ViewModel() { member = member_select });

        }
        public ActionResult Delete(int id)
        {
            dbCookingEntities db = new dbCookingEntities();
            t會員 member_select = db.t會員.FirstOrDefault(p => p.f會員Id == id);
            if (member_select != null)
            {
                db.t會員.Remove(member_select);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            dbCookingEntities db = new dbCookingEntities();
            IEnumerable<t會員> datas = null;
            string keyword = Request.Form["txtKeyword"];
            if (string.IsNullOrEmpty(keyword))
                datas = from p in (new dbCookingEntities()).t會員 select p;
            else
                datas = from p in (new dbCookingEntities()).t會員 where p.f會員姓名.Contains(keyword) || p.f會員信箱.Contains(keyword) select p;


            List<C管理者會員ViewModel> list = new List<C管理者會員ViewModel>();
            foreach (t會員 p in datas)
                list.Add(new C管理者會員ViewModel() { member = p });

            //修正部分viewmodel顯示資訊
            foreach (C管理者會員ViewModel p in list)
            {
                string a = p.f會員建立日期.ToString("yyyy/MM/dd");
                p.會員建立日期= a;
                if (p.f性別 == 0)
                    p.性別 = "不公開";
                else if (p.f性別 == 1)
                    p.性別 = "男";
                else
                    p.性別 = "女";

                if (p.f權限 == 1)
                    p.權限 = "管理者";
                else if (p.f權限 == 2)
                    p.權限 = "黑名單";
                else
                    p.權限 = "一般會員";

                p.檢舉下架次數 = db.Cooking查詢某會員被檢舉下架的聚會ListBy會員Id(p.f會員Id);

                
                   
            
                
            }


            return View(list);

        }
        public ActionResult 用戶恢復(int id)
        {
            (new C聚會相關操作()).恢復會員權限(id);
            return RedirectToAction("List");
        }
        public ActionResult 用戶停權(int id)
        {
            (new C聚會相關操作()).修改會員權限(id);
            return RedirectToAction("List");
        }
        public ActionResult query黑名單()
        {
            dbCookingEntities db = new dbCookingEntities();
            IEnumerable<t會員> datas = null;
            string keyword = Request.Form["txtKeyword"];
            if (string.IsNullOrEmpty(keyword))
                datas = from p in (new dbCookingEntities()).t會員 where p.f權限==2 select p;
            else
                datas = from p in (new dbCookingEntities()).t會員 where p.f會員姓名.Contains(keyword) || p.f會員信箱.Contains(keyword) select p;


            List<C管理者會員ViewModel> list = new List<C管理者會員ViewModel>();
            foreach (t會員 p in datas)
                list.Add(new C管理者會員ViewModel() { member = p });

            //修正部分viewmodel顯示資訊
            foreach (C管理者會員ViewModel p in list)
            {
                string a = p.f會員建立日期.ToString("yyyy/MM/dd");
                p.會員建立日期 = a;
                if (p.f性別 == 0)
                    p.性別 = "不公開";
                else if (p.f性別 == 1)
                    p.性別 = "男";
                else
                    p.性別 = "女";

                if (p.f權限 == 1)
                    p.權限 = "管理者";
                else
                    p.權限 = "一般會員";

                p.檢舉下架次數 = db.Cooking查詢某會員被檢舉下架的聚會ListBy會員Id(p.f會員Id);

                if (p.檢舉下架次數.Count >= 3)
                {
                    (new C聚會相關操作()).修改會員權限(p.f會員Id);
                }

            }


            return View(list);

        }
    }
}