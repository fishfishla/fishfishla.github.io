using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjCooking.Models;
using PagedList;

namespace prjCooking.ViewModel
{
    public class C主辦Or報名ViewModel
    {
        private string _currentSort;
        private List<string> _sortList;
        private List<int> _sortNumberList;
        private string _currentStatu;
        private List<string> _statuList;
        private List<int> _statuNumberList;

        public C主辦Or報名ViewModel()
        {
            _currentSort = "";
            _sortList = new List<string>();
            _sortNumberList = new List<int>();
            _currentStatu = "";
            _statuList = new List<string>();
            _statuNumberList = new List<int>();
        }

        /// <summary>
        /// 分頁條+撈取的聚會資訊
        /// </summary>
        public IPagedList<CCaptureMeetInfo> Info { get; set; }

        /// <summary>
        /// 當前排序規則字串
        /// </summary>
        public string CurrentSort
        {
            get 
            {
                return _currentSort;
            }

            set
            {
                int number;
                int? sortNumber = null;
                if (Int32.TryParse(value, out number))
                    sortNumber = number;
                _currentSort = SelectCurrentSort(sortNumber);
                _sortList = AddSortList(sortNumber);
            } 
        }

        /// <summary>
        /// 其他排序規則字串列表
        /// </summary>
        public List<string> SortList { get { return _sortList; } }

        /// <summary>
        /// 其他排序規則數字
        /// </summary>
        public List<int> SortNumberList { get { return _sortNumberList; } }

        /// <summary>
        /// 當前選擇聚會狀態規則字串
        /// </summary>
        public string CurrentStatu 
        {
            get
            {
                return _currentStatu;
            }

            set
            {
                int number;
                int? statuNumber = null;
                if (Int32.TryParse(value, out number))
                    statuNumber = number;

                _currentStatu = SelectCurrentStatu(statuNumber);
                _statuList = AddStatuList(statuNumber);
            }
        }

        /// <summary>
        /// 其他聚會狀態規則字串
        /// </summary>
        public List<string> StatuList { get { return _statuList; } }

        /// <summary>
        /// 其他聚會狀態規則數字
        /// </summary>
        public List<int> StatuNumberList { get { return _statuNumberList; } }

        /// <summary>
        /// 取得當前排序規則
        /// </summary>
        /// <param name="sortNumber">排序 0新 1舊</param>
        /// <returns></returns>
        private string SelectCurrentSort(int? sortNumber)
        {
            if(sortNumber.HasValue)
            {
                if (sortNumber.Value == 0)
                    return "新-舊";
                else
                    return "舊-新";
            }

            return "新-舊";
        }

        /// <summary>
        /// 加入其他排序規則
        /// </summary>
        /// <param name="sortNumber">排序 0新 1舊</param>
        /// <returns>List<string></returns>
        private List<string> AddSortList(int? sortNumber)
        {
            List<string> temp = new List<string>();

            if(sortNumber.HasValue)
            {
                if (sortNumber.Value != 0) 
                {
                    temp.Add("新-舊");
                    _sortNumberList.Add(0);
                }   
                if (sortNumber.Value != 1) 
                {
                    temp.Add("舊-新");
                    _sortNumberList.Add(1);
                }    
            }

            return temp;
        }

        /// <summary>
        /// 取得當前選擇狀態
        /// </summary>
        /// <param name="statuNumber">
        /// 
        /// </param>
        /// <returns></returns>
        private string SelectCurrentStatu(int? statuNumber)
        {
            string tempStatu = "全部";
            if (statuNumber.HasValue && Info != null)
            {
                if (statuNumber.Value == 0)
                    tempStatu = "報名中";
                else if (Info.Count > 0)
                    tempStatu = Info[0].聚會狀態Name;
            }

            return tempStatu;
        }

        /// <summary>
        /// 加入其他狀態列表
        /// </summary>
        /// <param name="statuNumber"></param>
        /// <returns></returns>
        private List<string> AddStatuList(int? statuNumber)
        {
            List<string> temp = new List<string>();

            if (statuNumber.HasValue && Info != null)
            {
                if (statuNumber != 3) 
                {
                    temp.Add("全部");
                    _statuNumberList.Add(3);
                }
                    
                if (statuNumber != 0) 
                {
                    temp.Add("報名中");
                    _statuNumberList.Add((int)聚會狀態.可報名);
                }
                    
                if (statuNumber != 1) 
                {
                    temp.Add(聚會狀態.進行中.ToString());
                    _statuNumberList.Add((int)聚會狀態.進行中);
                }
                    
                if (statuNumber != 2) 
                {
                    temp.Add(聚會狀態.已結束.ToString());
                    _statuNumberList.Add((int)聚會狀態.已結束);
                }
            }

            return temp;
        }
    }
}