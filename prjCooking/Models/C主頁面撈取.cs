using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjCooking.Models
{
    public class C主頁面撈取
    {
        private List<t聚會> _聚會List;
        private string _keyword;


        public C主頁面撈取(string keyword)
        {
            dbCookingEntities db = new dbCookingEntities();
            _聚會List = db.Cooking查詢所有沒被刪除聚會List();
            _聚會List.Reverse();
            _keyword = keyword;

        }

        public List<t聚會> 所有聚會()
        {
            List<t聚會> 所有聚會List = new List<t聚會>();
            所有聚會List.AddRange(_聚會List);


            if (!string.IsNullOrEmpty(_keyword))
            {
                所有聚會List = 尋找關鍵字聚會(_聚會List);
            }

            return 所有聚會List;
        }


        public List<t聚會> 進行中聚會()
        {
            List<t聚會> 進行中聚會List = new List<t聚會>();

            foreach(t聚會 聚會 in _聚會List)
            {
                if (聚會.f聚會狀態.Value == (int)聚會狀態.進行中)
                    進行中聚會List.Add(聚會);
            }

            if (!string.IsNullOrEmpty(_keyword))
            {
                進行中聚會List = 尋找關鍵字聚會(進行中聚會List);
            }

            return 進行中聚會List;
        }

        public List<t聚會> 未開始聚會()
        {
            List<t聚會> 未開始聚會List = new List<t聚會>();

            foreach (t聚會 聚會 in _聚會List)
            {
                if (聚會.f聚會狀態.Value == (int)聚會狀態.可報名)
                    未開始聚會List.Add(聚會);
            }

            if (!string.IsNullOrEmpty(_keyword))
            {
                未開始聚會List = 尋找關鍵字聚會(未開始聚會List);
            }

            return 未開始聚會List;
        }

        public List<t聚會> 已結束聚會()
        {
            List<t聚會> 已結束聚會List = new List<t聚會>();

            foreach (t聚會 聚會 in _聚會List)
            {
                if (聚會.f聚會狀態.Value == (int)聚會狀態.已結束)
                    已結束聚會List.Add(聚會);
            }

            if (!string.IsNullOrEmpty(_keyword))
            {
                已結束聚會List = 尋找關鍵字聚會(已結束聚會List);
            }

            return 已結束聚會List;
        }

        private List<t聚會> 尋找關鍵字聚會(List<t聚會> listTemp)
        {
            List<t聚會> 關鍵字聚會List = new List<t聚會>();
            foreach(t聚會 聚會 in listTemp)
            {
                if (!string.IsNullOrEmpty(聚會.f聚會名稱))
                {
                    if(聚會.f聚會名稱.Contains(_keyword))
                        關鍵字聚會List.Add(聚會);
                }

                if (!string.IsNullOrEmpty(聚會.f聚會內容))
                {
                    if (!關鍵字聚會List.Contains(聚會))
                    {
                        if(聚會.f聚會內容.Contains(_keyword))
                            關鍵字聚會List.Add(聚會);
                    }    
                }

                if (!string.IsNullOrEmpty(聚會.f聚會關鍵字))
                {
                    if (!關鍵字聚會List.Contains(聚會))
                    {
                        if (聚會.f聚會關鍵字.Contains(_keyword))
                            關鍵字聚會List.Add(聚會);
                    }
                }
            }
            return 關鍵字聚會List;

        }
    }
}