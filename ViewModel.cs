using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResumeManagement.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            this.QualificationList=new List<int>();
        }
        public int EmployeeId { get; set; }
        [Required, StringLength(20), Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Required, Display(Name = "Join Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ValidDate]
        public DateTime JoinDate { get; set; }
        public string Picture { get; set; }
        public bool isActive { get; set; }
        public int salary { get; set; }
        public HttpPostedFileBase PicturePath { get; set; }
        public List<int> QualificationList { get; set; }
    }
}
