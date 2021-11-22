using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjCooking.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjCooking.ViewModel
{
    public class C個人頁面ViewModel
    {

        public t會員 member { get; set; } = null;
        public HttpPostedFileBase image { get; set; }

        public C個人頁面ViewModel()
        {
            this.member = new t會員();

        }
        [DisplayName(" ")]
        public int f會員Id
        {
            get { return this.member.f會員Id; }
            set { this.member.f會員Id = value; }
        }

        [DisplayName("帳號")]

        public string f會員信箱
        {
            get { return this.member.f會員信箱; }
            set { this.member.f會員信箱 = value; }
        }
        [DisplayName("密碼")]
        [Required(ErrorMessage = "密碼不可空白")]
        public string f會員密碼
        {
            get { return this.member.f會員密碼; }
            set { this.member.f會員密碼 = value; }
        }
        [DisplayName("姓名")]
        public string f會員姓名
        {
            get { return this.member.f會員姓名; }
            set { this.member.f會員姓名 = value; }
        }
        [DisplayName("電話")]
        [Range(0900000001, 10000000000, ErrorMessage = "電話格式為0900000000")]
        public string f會員電話
        {
            get { return this.member.f會員電話; }
            set { this.member.f會員電話 = value; }
        }
        [DisplayName("性別")]
        [Required(ErrorMessage = "性別不可空白")]
        public Nullable<int> f性別
        {
            get { return this.member.f性別; }
            set { this.member.f性別 = value; }
        }
        [DisplayName("自我介紹")]
        [StringLength(20, ErrorMessage = "字數不可大於20")]
        public string f自我介紹
        {
            get { return this.member.f自我介紹; }
            set { this.member.f自我介紹 = value; }
        }
        [DisplayName("個人照片")]
        public string f會員照片
        {
            get { return this.member.f會員照片; }
            set { this.member.f會員照片 = value; }
        }
        [DisplayName("權限")]
        public Nullable<int> f權限
        {
            get { return this.member.f權限; }
            set { this.member.f權限 = value; }
        }
        [DisplayName("建立日期")]
        public System.DateTime f會員建立日期
        {
            get { return this.member.f會員建立日期; }
            set { this.member.f會員建立日期 = value; }
        }
        public int 目前會員id
        {
            get; set;
        }


        public List<t聚會> 會員聚會資訊 { get; set; }
        public List<t聚會> 最新聚會 { get; set; }
    }
}