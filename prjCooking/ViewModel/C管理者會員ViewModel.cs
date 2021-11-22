using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjCooking.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjCooking.ViewModel
{
    public class C管理者會員ViewModel
    {
        public t會員 member { get; set; } = null;
        public HttpPostedFileBase image { get; set; }

        public C管理者會員ViewModel()
        {
            this.member = new t會員();

        }
       
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
        public string f會員電話
        {
            get { return this.member.f會員電話; }
            set { this.member.f會員電話 = value; }
        }
        [DisplayName("性別")]
        public Nullable<int> f性別 {
            get { return this.member.f性別; }
            set { this.member.f性別 = value; }
        }
        [DisplayName("自我介紹")]
        public string f自我介紹 {
            get { return this.member.f自我介紹; }
            set { this.member.f自我介紹 = value; }
        }
        [DisplayName("個人照片")]
        public string f會員照片 {
            get { return this.member.f會員照片; }
            set { this.member.f會員照片 = value; }
        }
        [DisplayName("權限")]
        public Nullable<int> f權限 {
            get { return this.member.f權限; }
            set { this.member.f權限 = value; }
        }
        [DisplayName("建立日期")]
        public System.DateTime f會員建立日期 {
            get { return this.member.f會員建立日期; }
            set { this.member.f會員建立日期 = value; }
        }
        [DisplayName("權限")]
        public string 權限
        { get;set;}
        [DisplayName("性別")]
        public string 性別
        { get; set; }
        public string 會員建立日期 { get; set; }
        public List<t聚會> 檢舉下架次數 { get; set; }
    }
}