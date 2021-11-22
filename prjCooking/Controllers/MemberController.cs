using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjCooking.Models;
using prjCooking.ViewModel;

namespace prjCooking.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit_Password(int id)
        {

            dbCookingEntities db = new dbCookingEntities();
            t會員 prod = db.t會員.FirstOrDefault(p => p.f會員Id == id);
            if (prod == null)
                return RedirectToAction("Show個人頁面");

            return View(new C個人頁面ViewModel() { member = prod });


        }
        [HttpPost]
        public ActionResult Edit_Password(string txt_Nowpassword, string txt_Newpassword1, string txt_Newpassword2)
        {
            ViewBag.nowpwd = "";
            ViewBag.newpwd = "";
            string a = Request.Form["blur_pwd"];
            int id = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
            dbCookingEntities db = new dbCookingEntities();
            t會員 prod = db.t會員.FirstOrDefault(p => p.f會員Id == id);
            if (prod != null)
            {
                if (txt_Nowpassword == a)
                {
                    if (txt_Newpassword1 == txt_Newpassword2)
                    {
                        prod.f會員密碼 = txt_Newpassword1;
                        db.SaveChanges();
                        return RedirectToAction("Show個人頁面");
                    }
                    else
                    {
                        ViewBag.nowpwd = "請重新輸入密碼，新密碼需輸入一致,請檢查";
                    }
                }
                else
                {
                    ViewBag.nowpwd = "密碼輸入不正確，請重新輸入密碼";
                   
                }

            }



            return View(new C個人頁面ViewModel() { member = prod });

        }
        public ActionResult Edit_info(int id)
        {
            dbCookingEntities db = new dbCookingEntities();
            t會員 prod = db.t會員.FirstOrDefault(p => p.f會員Id == id);
            if (prod == null)
                return RedirectToAction("Show個人頁面");

            return View(new C個人頁面ViewModel() { member = prod });


        }
        [HttpPost]
        public ActionResult Edit_info(C個人頁面ViewModel editProduct)
        {
            dbCookingEntities db = new dbCookingEntities();
            t會員 prod = db.t會員.FirstOrDefault(p => p.f會員Id == editProduct.f會員Id);
            if (prod != null)
            {
                if (editProduct.image != null)
                {
                    //把照片重新命名
                    //讓名稱為唯一值
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    prod.f會員照片 = photoName;
                    editProduct.image.SaveAs(Server.MapPath("../../Image/" + photoName));
                }

                prod.f自我介紹 = editProduct.f自我介紹;
                prod.f會員電話 = editProduct.f會員電話;

                db.SaveChanges();
                Session[CSessionKey.登入會員_t會員] = prod;
            }

            return RedirectToAction("Show個人頁面");
        }

        public ActionResult Show個人頁面(int? id)
        {
            dbCookingEntities db = new dbCookingEntities();
            t會員 member_select = new t會員();
            if (id == null)
            {
                id = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
                member_select = db.t會員.FirstOrDefault(p => p.f會員Id == id);
            }
            else
            {

                member_select = db.t會員.FirstOrDefault(p => p.f會員Id == id);
                //List<t聚會> party = db.Cooking查詢某會員聚會ListBy會員Id(id);
            }
            C個人頁面ViewModel list = new C個人頁面ViewModel() { member = member_select };
            list.會員聚會資訊 = db.Cooking查詢某會員沒被刪除的聚會ListBy會員Id(id);
            list.最新聚會 = db.Cooking查詢所有沒被刪除聚會List();
            list.最新聚會.Reverse();

            if (Session[CSessionKey.登入會員_t會員] != null)
            {
                list.目前會員id = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
            }
            else
            {
                return RedirectToAction("登入");
            }



            return View(list);
        }

        public ActionResult 登入()
        {
            Session.Clear();
            Session.Abandon();
            return View();
        }
        [HttpPost]
        public ActionResult 登入(string txtEmail, string txtPwd)
        {
            dbCookingEntities db = new dbCookingEntities();

            
            t會員 會員 = null;            
            if (txtEmail != "" && txtPwd != "")
            {
                會員 = db.Cooking查詢某會員的資料By信箱And密碼(txtEmail, txtPwd);
                List<t聚會> 會員的聚會 = new List<t聚會>();
              
              
              
                    if (會員 != null)
                    {
                    if (會員.f權限 == 2)
                    {
                        ViewBag.通知訊息 = "⛔ 此帳號已停權，無法登入";
                        return View();
                    }
                    else
                    {
                        Session[CSessionKey.登入會員_t會員] = 會員;
                        return RedirectToAction("Index", "Home");
                    }
                 
                    }
                    else
                    {
                        ViewBag.通知訊息 = "⛔ 帳號或密碼錯誤";
                        return View();
                    }
                
            }
            else
            {
                ViewBag.通知訊息 = "⛔ 請輸入帳號密碼";
                return View();
            }
        }

        public ActionResult 忘記密碼()
        {
            ViewBag.通知訊息 = Session[CSessionKey.忘記密碼_通知訊息];
            Session.Clear();
            return View();
        }
        [HttpPost]
        public ActionResult 忘記密碼(string txtEmail, string txtPwd, string txtRePwd, string check)
        {            
            t會員 會員 = new t會員() ;
            dbCookingEntities db = new dbCookingEntities();
            if (txtEmail != "")
            {
                string seEmail = (string)Session[CSessionKey.註冊會員_fEmail];

                if (!string.IsNullOrEmpty(txtEmail))
                {
                    會員 = db.t會員.Where(m => m.f會員信箱 == txtEmail).FirstOrDefault();
                    if(會員 == null)
                    {
                        Session[CSessionKey.忘記密碼_通知訊息] = "此帳號尚未註冊";
                        return RedirectToAction("忘記密碼");
                    }
                }                    
                else
                    會員 = db.t會員.Where(m => m.f會員信箱 == seEmail).FirstOrDefault();

                if ((string)Session[CSessionKey.驗證碼_int] != null)
                {
                    if ((string)Session[CSessionKey.驗證碼_int] == check) 
                    {
                        if (txtPwd == txtRePwd)
                        {
                            會員.f會員密碼 = txtPwd;
                            db.Cooking修改會員資料(會員);

                            return RedirectToAction("登入");
                        }
                        else
                        {
                            Session[CSessionKey.忘記密碼_通知訊息] = "新密碼兩次輸入需一致，請檢察";
                            return RedirectToAction("忘記密碼");
                        }           
                    }
                    else
                    {
                        Session[CSessionKey.忘記密碼_通知訊息] = "驗證碼錯誤";
                        return RedirectToAction("忘記密碼");
                    }               
                }
                else
                {
                    Maill();
                    ViewBag.dis = "disabled";
                    ViewBag.mail = (string)Session[CSessionKey.註冊會員_fEmail];
                }
            }
            else
            {
                Session[CSessionKey.忘記密碼_通知訊息] = "請輸入帳號";
                return RedirectToAction("忘記密碼");
            }
                
            return View();
        }

        //請從信箱驗證頁面跳到註冊頁面
        public ActionResult 註冊()
        {
            ViewBag.dis = "disabled";
            ViewBag.驗證mail = (string)Session[CSessionKey.註冊會員_fEmail];
            return View();
        }
        [HttpPost]
        public ActionResult 註冊(string txtPwd, string txtName, int txtGender)
        {
            string txtEmail = (string)Session[CSessionKey.註冊會員_fEmail];
            dbCookingEntities db = new dbCookingEntities();
            t會員 註冊 = new t會員();

            註冊.f會員信箱 = txtEmail;
            註冊.f會員密碼 = txtPwd;
            註冊.f會員姓名 = txtName;
            註冊.f性別 = txtGender;
            註冊.f權限 = 0;
            註冊.f會員建立日期 = DateTime.Now;
            註冊.f會員照片 = "person.svg";

            db.Cooking新增某表格資料<t會員>(註冊);

            t會員 會員 = db.Cooking查詢某會員的資料By信箱And密碼(txtEmail, txtPwd);
            Session[CSessionKey.登入會員_t會員] = 會員;
            return RedirectToAction("Index", "Home");            
        }

        public ActionResult 信箱驗證()
        {
            dbCookingEntities db = new dbCookingEntities();
            if (Request.Form["txtEmail"] != null)
            {
                List<t會員> m = db.Cooking查詢所有會員List();
                foreach (t會員 t in m)
                {
                    if (t.f會員信箱 == Request.Form["txtEmail"])
                    {
                        ViewBag.帳號已存在 = "帳號已存在";
                        return View();
                    }                    
                }
                ViewBag.dis = "disabled";
                ViewBag.mail = Request.Form["txtEmail"];
                Maill();

            }

            if (Session[CSessionKey.驗證碼_int] != null)
            {
                var ch = Request.Form["check"];
                if (!string.IsNullOrEmpty(ch))
                {
                    if (((string)Session[CSessionKey.驗證碼_int]) != ch)
                    {
                        ViewBag.帳號已存在 = "驗證碼錯誤";
                    }
                    else
                    {
                        return RedirectToAction("註冊");
                    }
                }
                
            }

            return View();
        }

        public string random()
        {
            int i;
            Random rnd = new Random();
            i = rnd.Next(1000, 9999);
            return i.ToString();
        }

        private void Maill()
        {
            string rnd = random();
            MailSend maill = new MailSend();

            maill.Path = @"C:\maillAccount.txt";
            maill.FromFileAccountInfo();
            maill.MailSendFromName = "煮播";
            maill.MailSendToName = "To親愛的會員";
            maill.MailSendToAddress = Request.Form["txtEmail"];

            maill.MailTitle = "煮播平台驗證碼";
            maill.MailContent = "您的驗證碼:" + rnd + @"










                ";

            // 寄件
            maill.SendMaill();
            //沒取值
            Session[CSessionKey.註冊會員_fEmail] = Request.Form["txtEmail"];
            Session[CSessionKey.驗證碼_int] = rnd;
        }
    }
}