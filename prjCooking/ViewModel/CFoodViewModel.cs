using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using prjCooking.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjCooking.ViewModel
{
    public class CFoodViewModel
    {
        public t建議食材 party_food { get; set; } = null;
        public CFoodViewModel()
        {
        
            this.party_food = new t建議食材();
            
        }
        public int f建議食材Id
        {
            get { return this.party_food.f建議食材Id; }
            set { this.party_food.f建議食材Id = value; }
        }
       
        public int f聚會Id
        {
            get { return this.party_food.f聚會Id; }
            set { this.party_food.f聚會Id = value; }
        }
        [DisplayName("食材名稱")]
        [Required(ErrorMessage = " ")]
        public string f食材名稱
        {
            get { return this.party_food.f食材名稱; }
            set { this.party_food.f食材名稱 = value; }
        }
        [DisplayName("份量")]
        public Nullable<int> f數量
        {
            get { return this.party_food.f數量; }
            set { this.party_food.f數量 = value; }
        }
        [DisplayName("單位")]
        public string f單位
        {
            get { return this.party_food.f單位; }
            set { this.party_food.f單位 = value; }
        }
        public t建議食材 食材list { get; set; }
    }
}