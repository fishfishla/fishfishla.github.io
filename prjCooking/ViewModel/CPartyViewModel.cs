using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjCooking.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjCooking.ViewModel
{
    public class CPartyViewModel
    {
        public t聚會 party { get; set; } = null;
        public t會員 party_boss { get; set; } = null;
        public t建議食材 party_food { get; set; } = null;
        public t評價 party_feedback { get; set; } = null;
        public t參加者 party_member { get; set; } = null;
        public HttpPostedFileBase image { get; set; }
        public List<t聚會> 最新聚會 { get; set; }

        public CPartyViewModel()
        {
            this.party = new t聚會();
            this.party_member = new t參加者();
            this.party_food = new t建議食材();
            this.party_boss = new t會員();
            this.party_feedback = new t評價();
        }

        public int f聚會Id
        {
            get { return this.party.f聚會Id; }
            set { this.party.f聚會Id = value; }
        }
        public int f主辦人
        {
            get { return this.party.f主辦人; }
            set { this.party.f主辦人 = value; }
        }
        [Required(ErrorMessage = "必填欄位")]
        [StringLength(20, ErrorMessage = "字數需小於20個字")]
        [DisplayName("聚會名稱")]

        public string f聚會名稱
        {
            get { return this.party.f聚會名稱; }
            set { this.party.f聚會名稱 = value; }
        }
        [StringLength(30, ErrorMessage = "字數需小於30個字")]
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
        [Required(ErrorMessage = "必填欄位")]
        [DisplayName("舉辦日期")]
        public string f聚會日期
        {
            get;
            set;
        }
        [Required(ErrorMessage = "必填欄位")]
        [DisplayName("開始時間")]

        public string f聚會開始時間
        {
            get; set;
        }
        [Required(ErrorMessage = "必填欄位")]
        [DisplayName("結束時間")]

        public string f聚會結束時間
        {
            get; set;
        }
        [Required(ErrorMessage = "必填欄位")]
        [Range(1, int.MaxValue, ErrorMessage = "名額需輸入數字,且最少為1")]
        [DisplayName("名額")]
        public Nullable<int> f名額
        {
            get { return this.party.f名額; }
            set { this.party.f名額 = value; }
        }
        [DisplayName("共煮平台")]
        [Required(ErrorMessage = "必填欄位")]
        public string f聚會軟體
        {
            get { return this.party.f聚會軟體; }
            set { this.party.f聚會軟體 = value; }
        }
        [DisplayName("連結")]
        [Url(ErrorMessage = "需填寫網址")]
        [Required(ErrorMessage = "必填欄位")]
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

        public System.DateTime f聚會建立日期
        {
            get { return this.party.f聚會建立日期; }
            set { this.party.f聚會建立日期 = value; }
        }
        public string 聚會狀態
        {
            get;set;
        }
        public string 聚會時間
        {
            get; set;
        }
        public string 開始時間
        {
            get; set;
        }
        public string 結束時間
        {
            get; set;
        }
        public int f會員Id 
        { 
            get { return this.party_boss.f會員Id; }
            set { this.party_boss.f會員Id = value; }
        }
        public string f會員信箱
        {
            get { return this.party_boss.f會員信箱; }
            set { this.party_boss.f會員信箱 = value; }
        }
        public string f會員姓名
        {
            get { return this.party_boss.f會員姓名; }
            set { this.party_boss.f會員姓名 = value; }
        }
        public string f會員照片
        {
            get { return this.party_boss.f會員照片; }
            set { this.party_boss.f會員照片 = value; }
        }


        public string f食材名稱 { get; set; }
        public Nullable<int> f數量 { get; set; }
        public string f單位 { get; set; }
    }
}