using System;
using System.ComponentModel.DataAnnotations;

namespace AutoHistoryCore
{
    public class HistoryBaseModel
    {
        [Display(Name ="تاریخ ثبت")]
        public DateTime? CrearedDateTime { get; set; }
        [Display(Name = "تاریخ حذف")]
        public DateTime? DeletedDatTime { get; set; }
        [Display(Name = "تاریخ آخرین ویرایش")]
        public DateTime? LastEditedDateTime { get; set; }
        [Display(Name = "حذف شده ؟")]
        public bool IsDeleted { get; set; }
    }
}
