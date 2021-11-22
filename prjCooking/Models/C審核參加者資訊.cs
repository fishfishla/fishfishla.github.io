using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjCooking.Models
{
    public class C審核參加者資訊
    {
        private t參加者 _參加者;
        private t會員 _會員;

        public C審核參加者資訊()
        {
            _參加者 = new t參加者();
            _會員 = new t會員();
        }

        public int? 聚會Id
        {
            get
            {
                return _參加者.f聚會Id;
            }

            set
            {
                _參加者.f聚會Id = value.Value;
            }
        }

        public int? 參加者Id
        {
            get
            {
                return _參加者.f參加者Id;
            }

            set
            {
                _參加者.f參加者Id = value.Value;
            }
        }

        public string 會員姓名
        {
            get
            {
                return _會員.f會員姓名;
            }

            set
            {
                _會員.f會員姓名 = value;
            }
        }

        public string 會員照片
        {
            get
            {
                return _會員.f會員照片;
            }

            set
            {
                _會員.f會員照片 = value;
            }
        }
    }
}