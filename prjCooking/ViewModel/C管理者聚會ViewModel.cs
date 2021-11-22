using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjCooking.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjCooking.ViewModel
{
    public class C管理者聚會ViewModel
    {
        public t聚會 party { get; set; } = null;


        public C管理者聚會ViewModel()
        {
            this.party = new t聚會();
        }

        public int f聚會Id
        {
            get { return this.party.f聚會Id; }
            set { this.party.f聚會Id = value; }
        }

        [DisplayName("聚會名稱")]
        public string f聚會名稱
        {
            get { return this.party.f聚會名稱; }
            set { this.party.f聚會名稱 = value; }
        }
        [DisplayName("內容")]
        public string f聚會內容
        {
            get { return this.party.f聚會內容; }
            set { this.party.f聚會內容 = value; }
        }

        public string f聚會照片
        {
            get { return this.party.f聚會照片; }
            set { this.party.f聚會照片 = value; }
        }
        [DisplayName("日期")]
        public System.DateTime f聚會日期
        {
            get { return this.party.f聚會日期; }
            set { this.party.f聚會日期 = value; }
        }
        [DisplayName("開始時間")]
        public System.DateTime f聚會開始時間
        {
            get { return this.party.f聚會開始時間; }
            set { this.party.f聚會開始時間 = value; }
        }
        [DisplayName("結束時間")]
        public Nullable<System.DateTime> f聚會結束時間
        {
            get { return this.party.f聚會結束時間; }
            set { this.party.f聚會結束時間 = value; }
        }
        [DisplayName("名額")]
        public Nullable<int> f名額
        {
            get { return this.party.f名額; }
            set { this.party.f名額 = value; }
        }
        [DisplayName("共煮平台")]
        public string f聚會軟體
        {
            get { return this.party.f聚會軟體; }
            set { this.party.f聚會軟體 = value; }
        }
        [DisplayName("連結")]
        public string f聚會軟體URL
        {
            get { return this.party.f聚會軟體URL; }
            set { this.party.f聚會軟體URL = value; }
        }
        [DisplayName("關鍵字")]
        public string f聚會關鍵字
        {
            get { return this.party.f聚會關鍵字; }
            set { this.party.f聚會關鍵字 = value; }
        }
        [DisplayName("通訊軟體")]
        public string f聚會通訊軟體
        {
            get { return this.party.f聚會通訊軟體; }
            set { this.party.f聚會通訊軟體 = value; }
        }
        [DisplayName("帳號")]
        public string f聚會通訊軟體帳號
        {
            get { return this.party.f聚會通訊軟體帳號; }
            set { this.party.f聚會通訊軟體帳號 = value; }
        }
        public Nullable<int> f聚會狀態
        {
            get { return this.party.f聚會狀態; }
            set { this.party.f聚會狀態 = value; }
        }
        public Nullable<bool> f聚會垃圾桶
        {
            get { return this.party.f聚會垃圾桶; }
            set { this.party.f聚會垃圾桶 = value; }
        }



        public string 聚會狀態
        {
            get; set;
        }
        [DisplayName(" ")]
        public string 聚會垃圾桶
        {
            get; set;
        }

        public System.DateTime f聚會建立日期
        {
            get { return this.party.f聚會建立日期; }
            set { this.party.f聚會建立日期 = value; }
        }
        [DisplayName("聚會建立日期")]
        public string 聚會建立日期
        { get; set; }
        [DisplayName("煮辦人")]
        public int f主辦人
        {
            get { return this.party.f主辦人; }
            set { this.party.f主辦人 = value; }
        }
        [DisplayName("煮辦人")]
        public string 煮辦人姓名
        { get; set; }
        public string 聚會檢舉狀態 { get; set; }
        public string 聚會檢舉狀態顯示 { get; set; }
        public List<t檢舉> 聚會檢舉資訊 { get; set; }
    }
}
