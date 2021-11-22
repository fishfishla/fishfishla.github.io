using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjCooking.Models
{
    public enum 聚會狀態
    {
        可報名 = 0,
        進行中 = 1,
        已結束 = 2
    }

    public enum 性別
    {
        不公開 = 0,
        男 = 1,
        女 = 2
    }

    public enum 權限
    {
        會員 = 0,
        管理者 = 1
    }

    public enum 聚會垃圾桶
    {
        顯示 = 0,
        刪除 = 1
    }

    public enum 參加者審核狀態
    {
        未通過 = 0,
        通過 = 1
    }
}