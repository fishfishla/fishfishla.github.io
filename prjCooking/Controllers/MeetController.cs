using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjCooking.Models;
using prjCooking.ViewModel;
using PagedList;

namespace prjCooking.Controllers
{
    public class MeetController : Controller
    {
        // GET: Meet
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult add主辦人影片(int 聚會Id, string f主辦人URL)
        {

            dbCookingEntities db = new dbCookingEntities();
            t聚會 Addparty = db.t聚會.FirstOrDefault(p => p.f聚會Id == 聚會Id);
            if (Addparty != null)
            {
                Addparty.f主辦人影片URL = f主辦人URL;
                db.SaveChanges();
            }
            return RedirectToAction("showParty", new { id = 聚會Id });
        }
        public ActionResult Party_Reported(int 檢舉聚會Id,string f檢舉原因)
        {
            dbCookingEntities db = new dbCookingEntities();
            t檢舉 Wprob = new t檢舉();
            
            Wprob.f被檢舉的聚會Id = 檢舉聚會Id;
            Wprob.f檢舉人Id= ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
            Wprob.f檢舉建立日期 = DateTime.Now;
            Wprob.f檢舉原因 = f檢舉原因;
            
            db.t檢舉.Add(Wprob);
            db.SaveChanges();
            
            return RedirectToAction("showParty", new { id = 檢舉聚會Id });
        }
        public ActionResult Edit_party(int id, int party_boss)
        {

            dbCookingEntities db = new dbCookingEntities();
            t聚會 prod = db.t聚會.FirstOrDefault(p => p.f聚會Id == id);
            t會員 prod_boss = db.t會員.FirstOrDefault(p => p.f會員Id == party_boss);
            string a = prod.f聚會日期.ToString("yyyy-MM-dd");
            string b = prod.f聚會開始時間.ToString("HH:mm");
            string c = prod.f聚會結束時間.Value.ToString("HH:mm");
            ViewBag.聚會id = id;
            ViewBag.partydate = a;
            ViewBag.partystart = b;
            ViewBag.partyend = c;
            if (prod == null)
                return RedirectToAction("showParty");
            CPartyViewModel select_party = new CPartyViewModel() { party = prod, party_boss = prod_boss };


            return View(select_party);

        }
        [HttpPost]
        public ActionResult Edit_party(CPartyViewModel newparty,string f聚會日期,string 聚會開始時間,string 聚會結束時間,int f聚會id)
        {
            dbCookingEntities db = new dbCookingEntities();
            t聚會 Addparty = db.t聚會.FirstOrDefault(p => p.f聚會Id == f聚會id);

            if (Addparty != null)
            {
                Addparty.f聚會Id = f聚會id;
                Addparty.f聚會名稱 = newparty.f聚會名稱;
                Addparty.f聚會內容 = newparty.f聚會內容;
                Addparty.f聚會關鍵字 = newparty.f聚會關鍵字;
                Addparty.f聚會軟體 = newparty.f聚會軟體;
                Addparty.f聚會軟體URL = newparty.f聚會軟體URL;
                Addparty.f聚會日期 = Convert.ToDateTime(f聚會日期);
                Addparty.f名額 = Convert.ToInt32(newparty.f名額);
                Addparty.f聚會通訊軟體 = newparty.f聚會通訊軟體;
                Addparty.f聚會通訊軟體帳號 = newparty.f聚會通訊軟體帳號;
                Addparty.f聚會垃圾桶 = false;
                Addparty.f聚會建立日期 = DateTime.Now;
                Addparty.f聚會開始時間 = Convert.ToDateTime(f聚會日期 + " " + 聚會開始時間);
                Addparty.f聚會結束時間 = Convert.ToDateTime(f聚會日期 + " " + 聚會結束時間);

                if (Addparty.f聚會開始時間 > DateTime.Now)
                    Addparty.f聚會狀態 = Convert.ToInt32(聚會狀態.可報名);
                else if ((Addparty.f聚會開始時間 < DateTime.Now) && (Addparty.f聚會結束時間 > DateTime.Now))
                    Addparty.f聚會狀態 = Convert.ToInt32(聚會狀態.進行中);
                else
                    Addparty.f聚會狀態 = Convert.ToInt32(聚會狀態.已結束);

                if (newparty.image != null)
                {
                    //把照片重新命名
                    //讓名稱為唯一值
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    Addparty.f聚會照片 = photoName;
                    newparty.image.SaveAs(Server.MapPath("~/image/" + photoName));

                }
                

                db.SaveChanges();
            }
         

            return RedirectToAction("showParty", new{id= newparty.f聚會Id});
        }
        public ActionResult showParty(int? id)
        {
            if (id.HasValue)
            {
                C聚會資訊For頁面ViewModel vmodel = (new C聚會相關操作()).撈取單一聚會資訊(id.Value);

                if(vmodel != null)
                {
                    if (Session[CSessionKey.登入會員_t會員] != null)
                        vmodel.當前登入會員資訊 = (t會員)Session[CSessionKey.登入會員_t會員];
                    else
                    {
                        vmodel.當前登入會員資訊 = new t會員();
                        vmodel.當前登入會員資訊.f會員Id = 0;
                    }

                    vmodel.參加者資訊List.Reverse();
                    foreach (C參加者資訊For聚會頁面 參加者 in vmodel.參加者資訊List)
                    {
                        if (參加者.參加者資訊.f會員Id == vmodel.當前登入會員資訊.f會員Id)
                        {
                            vmodel.當前會員參加者Id = 參加者.參加者資料.f參加者Id;
                            if(!參加者.參加者資料.f報名)
                                vmodel.Is當前會員報名 = true;
                            if (參加者.評論 != null)
                                vmodel.Is成果發表 = true;
                            if (參加者.參加者資料.f審核狀態)
                                vmodel.Is核准參與 = true;

                            break;
                        }
                    }

                    return View(vmodel);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateParty()
        {
          
            if (Session[CSessionKey.登入會員_t會員] != null)
            {
                ViewBag.email = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員信箱;

                return View();
            }
            return RedirectToAction("登入", "Member");

        }

        [HttpPost]
        public ActionResult CreateParty(CPartyViewModel newparty)
        {
            dbCookingEntities db = new dbCookingEntities();
            t聚會 Addparty = new t聚會();

            Addparty.f主辦人 = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
            Addparty.f聚會名稱 = newparty.f聚會名稱;
            Addparty.f聚會內容 = newparty.f聚會內容;
            Addparty.f聚會關鍵字 = newparty.f聚會關鍵字;
            Addparty.f聚會軟體 = newparty.f聚會軟體;
            Addparty.f聚會軟體URL = newparty.f聚會軟體URL;
            Addparty.f聚會日期 = Convert.ToDateTime(newparty.f聚會日期);
            Addparty.f名額 = Convert.ToInt32(newparty.f名額);
            Addparty.f聚會通訊軟體 = newparty.f聚會通訊軟體;
            Addparty.f聚會通訊軟體帳號 = newparty.f聚會通訊軟體帳號;
            Addparty.f聚會垃圾桶 = false;
            Addparty.f聚會建立日期 = DateTime.Now;
            Addparty.f聚會開始時間 = Convert.ToDateTime(newparty.f聚會日期 + " " + newparty.f聚會開始時間);
            Addparty.f聚會結束時間 = Convert.ToDateTime(newparty.f聚會日期 + " " + newparty.f聚會結束時間);

            if (Addparty.f聚會開始時間 > DateTime.Now)
                Addparty.f聚會狀態 = Convert.ToInt32(聚會狀態.可報名);
            else if ((Addparty.f聚會開始時間 < DateTime.Now) && (Addparty.f聚會結束時間 > DateTime.Now))
                Addparty.f聚會狀態 = Convert.ToInt32(聚會狀態.進行中);
            else
                Addparty.f聚會狀態 = Convert.ToInt32(聚會狀態.已結束);

            if (newparty.image != null)
            {
                //把照片重新命名
                //讓名稱為唯一值
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                Addparty.f聚會照片 = photoName;
                newparty.image.SaveAs(Server.MapPath("~/image/" + photoName));

            }
            else
                Addparty.f聚會照片 = "party_02.jpg";
            
            
            db.t聚會.Add(Addparty);
            db.SaveChanges();

            
            return RedirectToAction("CreateFood", "Food");
        }
        public ActionResult 報名紀錄(int? sort = 0, int? statu = 3, int page = 1)
        {
            // 排序 0新 1舊
            // 狀態 3 全部
            if(Session[CSessionKey.登入會員_t會員] != null)
            {
                // session取得會員資料
                int 會員Id = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
                CCaptureRecords crs = new CCaptureRecords();
                C主辦Or報名ViewModel vmodel = new C主辦Or報名ViewModel();
                List<CCaptureMeetInfo> data = crs.撈取報名記錄(會員Id, sort, statu);
                vmodel.Info = crs.GetPageList(data, page);

                if (sort != null && statu != null) 
                {
                    vmodel.CurrentSort = sort.ToString();
                    vmodel.CurrentStatu = statu.ToString();
                }
                
                return View(vmodel);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult 主辦紀錄(int? sort = 0, int page = 1)
        {
            if (Session[CSessionKey.登入會員_t會員] != null)
            {
                int 會員Id = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
                CCaptureRecords crs = new CCaptureRecords();
                C主辦Or報名ViewModel vmodel = new C主辦Or報名ViewModel();
                List<CCaptureMeetInfo> data = crs.撈取主辦記錄(會員Id, sort);
                vmodel.Info = crs.GetPageList(data, page);

                if (sort != null)
                {
                    vmodel.CurrentSort = sort.ToString();
                }

                return View(vmodel);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult 資格審核(int? meetId)
        {
            // 判斷Session
            if(Session[CSessionKey.登入會員_t會員] != null)
            {
                if(meetId != null)
                {
                    C資格審核ViewModel vmodel = new C資格審核ViewModel();
                    C撈取資格審核名單 撈取 = new C撈取資格審核名單();
                    int 主辦人Id = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
                    vmodel.當前聚會 = (new dbCookingEntities()).Cooking查詢某聚會資訊By聚會Id(meetId.Value);

                    // 撈取已核准名單
                    bool 核准 = Convert.ToBoolean(參加者審核狀態.通過);
                    if (撈取.Set撈取(主辦人Id, meetId, 核准))
                        vmodel.核准 = 撈取.Get();

                    // 撈取未審核名單
                    bool? 未審核 = Convert.ToBoolean(參加者審核狀態.未通過);
                    if(撈取.Set撈取(主辦人Id, meetId, 未審核))
                        vmodel.未審核 = 撈取.Get();

                    return View(vmodel);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult 取消報名(int? 聚會Id)
        {
            // session 抓會員id
            if(Session[CSessionKey.登入會員_t會員] != null)
            {
                int 會員Id = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
                (new C聚會相關操作()).取消報名(會員Id, 聚會Id.Value);
                return RedirectToAction("報名紀錄");
            }

            return RedirectToAction("index", "Home");
        }

        public ActionResult 取消活動(int? 聚會Id)
        {
            // session 抓會員id
            if(Session[CSessionKey.登入會員_t會員] != null)
            {
                (new C聚會相關操作()).取消活動(聚會Id.Value);
                return RedirectToAction("主辦紀錄");
            }

            return RedirectToAction("index", "Home");
        }

        public ActionResult 核准參加者(int? 聚會Id, int? 參加者Id)
        {
            // 判斷是否有登入
            if(Session[CSessionKey.登入會員_t會員] != null)
            {
                (new C聚會相關操作()).核准參加者(聚會Id.Value, 參加者Id.Value);

                return RedirectToAction("資格審核", new { meetId = 聚會Id.Value });
            }

            return RedirectToAction("index", "Home");
        }

        public ActionResult 報名(int? 聚會Id)
        {
            if(Session[CSessionKey.登入會員_t會員] != null)
            {
                if (聚會Id.HasValue)
                {
                    dbCookingEntities db = new dbCookingEntities();
                    int 聚會名額 = db.Cooking查詢某聚會資訊By聚會Id(聚會Id).f名額.Value;
                    int 當前參加人數 = db.Cooking查詢某聚會核准參與者ListBy聚會Id(聚會Id).Count;
                    if(當前參加人數 < 聚會名額)
                    {
                        int 會員Id = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
                        t參加者 新參加者 = new t參加者();
                        新參加者.f聚會Id = 聚會Id.Value;
                        新參加者.f會員Id = 會員Id;
                        新參加者.f參加者建立日期 = DateTime.Now;
                        db.Cooking新增某表格資料<t參加者>(新參加者);
                    }

                    return RedirectToAction("showParty", new { id = 聚會Id.Value });
                }
            }

            return RedirectToAction("登入", "Member");
        }

        [HttpPost]
        public ActionResult 新增評論(C新增評論for聚會頁面ViewModel vmodel)
        {
            if (vmodel != null)
            {
                t評價 新評論 = new t評價();

                if (vmodel.上傳圖片 != null)
                {
                    string new圖片名字 = C上傳圖片檔案重新命名.Get新名字(vmodel.上傳圖片);
                    新評論.f照片 = new圖片名字;
                    vmodel.上傳圖片.SaveAs(Server.MapPath("~/Image/" + new圖片名字));
                }

                新評論.f參加者Id = vmodel.參加者Id.Value;
                新評論.f聚會Id = vmodel.聚會Id.Value;
                新評論.f留言 = vmodel.留言;
                新評論.f評價建立日期 = DateTime.Now.ToString("yyyy/MM/dd");

                (new dbCookingEntities()).Cooking新增某表格資料<t評價>(新評論);
            }

            return RedirectToAction("showParty", new { id = vmodel.聚會Id});
        }

        [HttpPost]
        public ActionResult 新增留言(int? 聚會Id, string 留言文字)
        {
            if (!string.IsNullOrEmpty(留言文字))
            {
                t留言 新留言 = new t留言();
                新留言.f留言人Id = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
                新留言.f留言的聚會Id = 聚會Id.Value;
                新留言.f留言文字 = 留言文字;
                新留言.f留言建立日期 = DateTime.Now;

                (new dbCookingEntities()).Cooking新增某表格資料<t留言>(新留言);
            }

            return RedirectToAction("showParty", new { id = 聚會Id });
        }

        public ActionResult Delete留言(int? 留言Id, int? 聚會Id)
        {
            if (留言Id.HasValue)
            {
                (new dbCookingEntities()).Cooking刪除一則留言By留言Id(留言Id);
            }

            return RedirectToAction("showParty", new { id = 聚會Id });
        }
    }
}