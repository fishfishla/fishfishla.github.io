// 撈取報名、舉辦的活動
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace prjCooking.Models
{
    public class CCaptureRecords
    {
        dbCookingEntities db;

        public CCaptureRecords()
        {
            db = new dbCookingEntities();
        }

        public List<CCaptureMeetInfo> 撈取報名記錄(int? memberId, int? sort, int? statu)
        {
            if(memberId.HasValue && sort.HasValue && statu.HasValue)
            {
                bool is刪除 = Convert.ToBoolean(聚會垃圾桶.刪除);
                List<CCaptureMeetInfo> data = db.t參加者.Where(m => m.f會員Id == memberId.Value)
                .Where(m => m.t聚會.f聚會垃圾桶 != is刪除)
                .OrderBy(m => m.t聚會.f聚會開始時間)
                .Select(m => new CCaptureMeetInfo
                {
                    聚會ID = m.f聚會Id,
                    主辦人 = m.t聚會.t會員.f會員姓名,
                    聚會名稱 = m.t聚會.f聚會名稱,
                    聚會日期 = m.t聚會.f聚會開始時間,
                    聚會狀態Number = m.t聚會.f聚會狀態.Value,
                    Has評價 = m.t評價.Where(t => t.f聚會Id == m.f聚會Id).Count() > 0 ? true : false,
                    Is核准 = m.f審核狀態,
                    Is報名 = m.f報名
                }).ToList();

                Get聚會狀態Name(data);
                Sort(data, sort);

                return data;
            }

            return new List<CCaptureMeetInfo>();
        }

        private void Get聚會狀態Name(List<CCaptureMeetInfo> data)
        {
            foreach(CCaptureMeetInfo cmi in data)
            {
                cmi.聚會狀態Name = Enum.GetName(typeof(聚會狀態), cmi.聚會狀態Number);
            }
        }

        private void 選擇聚會狀態(List<CCaptureMeetInfo> data, int statu)
        {
            List<CCaptureMeetInfo> cmiTemp = new List<CCaptureMeetInfo>();
            foreach(CCaptureMeetInfo cmi in data)
            {
                if(cmi.聚會狀態Number.Value == statu)
                {
                    cmiTemp.Add(cmi);
                }
            }

            data = cmiTemp;

            /*switch (statu)
            {
                case (int)聚會狀態.可報名:
                    data = data.Where(info => 聚會狀態.可報名.CompareTo(info.聚會狀態Number.Value) == 0).ToList();
                    break;
                case (int)聚會狀態.進行中:
                    data = data.Where(info => 聚會狀態.進行中.CompareTo(info.聚會狀態Number.Value) == 0).ToList();
                    break;
                case (int)聚會狀態.已結束:
                    data = data.Where(info => 聚會狀態.已結束.CompareTo(info.聚會狀態Number.Value) == 0).ToList();
                    break;
            }*/
        }

        private void Sort(List<CCaptureMeetInfo> data, int? sortNumber)
        {
            if(data.Count > 0)
            {
                // 排序 0新 1舊
                if (sortNumber.Value == 0)
                {
                    data.Reverse();
                }

                //// 狀態 3 全部
                //if (statu.HasValue)
                //    if (statu.Value < 3)
                //        選擇聚會狀態(data, statu.Value);
            }
        }

        public List<CCaptureMeetInfo> 撈取主辦記錄(int? memberId, int? sort)
        {
            if(memberId.HasValue && sort.HasValue)
            {
                bool is刪除 = Convert.ToBoolean(聚會垃圾桶.刪除);
                List<CCaptureMeetInfo> data = db.t聚會.Where(meet => meet.f主辦人 == memberId.Value)
                .Where(meet => meet.f聚會垃圾桶 != is刪除)
                .OrderBy(meet => meet.f聚會建立日期)
                .Select(meet => new CCaptureMeetInfo
                {
                    聚會ID = meet.f聚會Id,
                    主辦人 = meet.t會員.f會員姓名,
                    聚會名稱 = meet.f聚會名稱,
                    聚會日期 = meet.f聚會日期,
                    聚會狀態Number = meet.f聚會狀態.Value,
                    人數上限 = meet.f名額.Value,
                    目前人數 = meet.t參加者.Where(t => t.f聚會Id == meet.f聚會Id).Where(m => m.f審核狀態).Count()
                }).ToList();

                Sort(data, sort);

                return data;
            }

            return new List<CCaptureMeetInfo>();
        }

        /// <summary>
        /// 取得分頁條
        /// </summary>
        /// <returns></returns>
        public IPagedList<CCaptureMeetInfo> GetPageList(List<CCaptureMeetInfo> data, int page)
        {
            // 分頁條
            if (data.Count > 0)
            {
                int pageSize = 10;
                int currentPage = page < 1 ? 1 : page;
                return data.ToPagedList(currentPage, pageSize);
            }

            return null;
        }
    }
}