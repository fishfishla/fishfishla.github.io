using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjCooking.Models
{
    public class C撈取資格審核名單
    {
        private dbCookingEntities _db;
        private delegate List<C審核參加者資訊> 撈取委派();
        private 撈取委派 _撈取;

        public C撈取資格審核名單()
        {
            _db = new dbCookingEntities();
            _撈取 = () => new List<C審核參加者資訊>();
        }

        public List<C審核參加者資訊> Get()
        {
            return _撈取();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="會員Id"></param>
        /// <param name="聚會Id"></param>
        /// <param name="Is核准"></param>
        /// <returns>設定成功回傳true</returns>
        public bool Set撈取(int? 會員Id, int? 聚會Id, bool? Is核准)
        {
            if(會員Id.HasValue && 聚會Id.HasValue)
            {
                _撈取 = () =>
                {
                    List<C審核參加者資訊> data =
                    _db.t參加者.Where(t => t.f聚會Id == 聚會Id.Value)
                    .Where(t => t.t聚會.f主辦人 == 會員Id.Value)
                    .Where(t => t.f審核狀態 == Is核准)
                    .Where(t => t.f報名 == false)
                    .Select(t => new C審核參加者資訊
                    {
                        參加者Id = t.f會員Id,
                        聚會Id = t.f聚會Id,
                        會員姓名 = t.t會員.f會員姓名,
                        會員照片 = t.t會員.f會員照片
                    }).ToList();

                    return data;
                };

                return true;
            }

            return false;
        }
    }
}