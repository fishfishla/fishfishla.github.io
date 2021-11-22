using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjCooking.Models;

namespace prjCooking.ViewModel
{
    public class C資格審核ViewModel
    {
        public C資格審核ViewModel()
        {
            核准 = new List<C審核參加者資訊>();
            未審核 = new List<C審核參加者資訊>();
        }

        public List<C審核參加者資訊> 核准 { get; set; }
        public List<C審核參加者資訊> 未審核 { get; set; }
        public t聚會 當前聚會 { get; set; }
    }
}