using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjCooking.Models;
using prjCooking.ViewModel;

namespace prjCooking.Controllers
{
    public class FoodController : Controller
    {
        // GET: Food
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult queryCreateFood(int id)
        {
            dbCookingEntities db = new dbCookingEntities();

            int query主辦人id = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
            var query聚會id = (from prod in db.t聚會
                             where prod.f主辦人 == query主辦人id
                             select prod.f聚會Id==id);
            ViewBag.Boss = query主辦人id;
            ViewBag.Partyid = id;
            return View();
        }
        [HttpPost]
        public ActionResult queryCreateFood(CFoodViewModel query聚會food)
        {
            dbCookingEntities db = new dbCookingEntities();

            t建議食材 addfood = new t建議食材();

            addfood.f聚會Id = Convert.ToInt32(Request.Form["f聚會Id"]);
            addfood.f食材名稱 = query聚會food.f食材名稱;
            addfood.f數量 = Convert.ToInt32(query聚會food.f數量);
            addfood.f單位 = query聚會food.f單位;

            db.t建議食材.Add(addfood);

            db.SaveChanges();

            return RedirectToAction("queryFoodList",new { id= query聚會food.f聚會Id});
        }
        public ActionResult queryEditFood(int id)
        {
            dbCookingEntities db = new dbCookingEntities();
            t建議食材 prod = db.t建議食材.FirstOrDefault(p => p.f建議食材Id == id);


            if (prod == null)
                return RedirectToAction("queryFoodList");

            return View(new CFoodViewModel() { party_food = prod });

        }
        [HttpPost]
        public ActionResult queryEditFood(CFoodViewModel editfood)
        {
            dbCookingEntities db = new dbCookingEntities();
            t建議食材 prod = db.t建議食材.FirstOrDefault(p => p.f建議食材Id == editfood.f建議食材Id);
            
            if (prod != null)
            {
               
                    prod.f聚會Id = Convert.ToInt32(Request.Form["f聚會Id"]);
                    prod.f食材名稱 = editfood.f食材名稱;
                    prod.f數量 = editfood.f數量;
                    prod.f單位 = editfood.f單位;

                    db.SaveChanges();
               
        
                
            }

            return RedirectToAction("queryFoodList",new { id=prod.f聚會Id});


        }

        public ActionResult EditFood(int id)
        {
            dbCookingEntities db = new dbCookingEntities();
            t建議食材 prod = db.t建議食材.FirstOrDefault(p => p.f建議食材Id == id);


            if (prod == null)
                return RedirectToAction("FoodList");

            return View(new CFoodViewModel() { party_food = prod });

        }
        [HttpPost]
        public ActionResult EditFood(CFoodViewModel editfood)
        {
            dbCookingEntities db = new dbCookingEntities();
            t建議食材 prod = db.t建議食材.FirstOrDefault(p => p.f建議食材Id == editfood.f建議食材Id);

            if (prod != null)
            {
                prod.f聚會Id = Convert.ToInt32(Request.Form["f聚會Id"]);
                prod.f食材名稱 = editfood.f食材名稱;
                prod.f數量 = editfood.f數量;
                prod.f單位 = editfood.f單位;

                db.SaveChanges();
            }

            return RedirectToAction("FoodList");


        }
        public ActionResult queryDelete_food(int id)
        {
            dbCookingEntities db = new dbCookingEntities();
            t建議食材 prod = db.t建議食材.FirstOrDefault(p => p.f建議食材Id == id);


            if (prod != null)
            {
                db.t建議食材.Remove(prod);
                db.SaveChanges();
            }

            return RedirectToAction("queryFoodList",new { id=prod.f聚會Id});

        }
        public ActionResult Delete_food(int id)
        {
            dbCookingEntities db = new dbCookingEntities();
            t建議食材 prod = db.t建議食材.FirstOrDefault(p => p.f建議食材Id == id);


            if (prod != null)
            {
                db.t建議食材.Remove(prod);
                db.SaveChanges();
            }
            return RedirectToAction("FoodList");

        }
        public ActionResult CreateFood()
        {
            dbCookingEntities db = new dbCookingEntities();

            int query主辦人id = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
            var query聚會id = (from prod in db.t聚會
                             where prod.f主辦人 == query主辦人id
                             select prod.f聚會Id).Max();
            ViewBag.Boss = query主辦人id;
            ViewBag.Partyid = query聚會id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateFood(CFoodViewModel query聚會food)
        {
            dbCookingEntities db = new dbCookingEntities();

            t建議食材 addfood = new t建議食材();

            addfood.f聚會Id = Convert.ToInt32(Request.Form["f聚會Id"]);
            addfood.f食材名稱 = query聚會food.f食材名稱;
            addfood.f數量 = Convert.ToInt32(query聚會food.f數量);
            addfood.f單位 = query聚會food.f單位;

            db.t建議食材.Add(addfood);

            db.SaveChanges();

            return RedirectToAction("FoodList");
        }
        public ActionResult FoodList()
        {

            dbCookingEntities db = new dbCookingEntities();

            int query主辦人id = ((t會員)Session[CSessionKey.登入會員_t會員]).f會員Id;
            var query聚會id = (from prod in db.t聚會
                             where prod.f主辦人 == query主辦人id
                             select prod.f聚會Id).Max();

            IEnumerable<t建議食材> datas = null;


            ViewBag.f聚會id = query聚會id;

            datas = from p in (new dbCookingEntities()).t建議食材
                    where p.f聚會Id == query聚會id
                    select p;

            List<CFoodViewModel> list = new List<CFoodViewModel>();

            foreach (t建議食材 p in datas)
                list.Add(new CFoodViewModel() { party_food = p });

            return View(list);

        }
        public ActionResult queryFoodList(int id)
        {
            int a = id;
            ViewBag.f聚會id = a;
            dbCookingEntities db = new dbCookingEntities();
            IEnumerable<t建議食材> datas = null;

            datas = from p in (new dbCookingEntities()).t建議食材
                    where p.f聚會Id == id
                    select p;

            List<CFoodViewModel> list = new List<CFoodViewModel>();

            foreach (t建議食材 p in datas)
                list.Add(new CFoodViewModel() { party_food = p });

            return View(list);

        }
       
    }
}