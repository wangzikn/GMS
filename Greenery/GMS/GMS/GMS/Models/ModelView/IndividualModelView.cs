using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Models.ModelView
{
    public class IndividualModelView
    {
        [Display(Name = "Chứng minh nhân dân")]
        public string Identity { get; set; }
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Khu vực")]
        public int? AreaId { get; set; }
        [Display(Name = "Ngày tháng năm sinh")]
        public DateTime? Birthday { get; set; }
        [Display(Name = "Ngày cấp CMND")]
        public DateTime? DateOfIssue { get; set; }
        [Display(Name = "Nơi cấp chứng minh nhân dân")]
        public string PlaceOfIssue { get; set; }
        [Display(Name = "Quản lý trực tiếp")]
        public string ManagerId { get; set; }
        [Display(Name = "Cá nhân/tổ chức")]
        public int? IndivType { get; set; }
        public string Id { get; set; }
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        //public bool EmailConfirmed { get; set; }
        [Display(Name = "Di động")]
        public string PhoneNumber { get; set; }
        //public bool PhoneNumberConfirmed { get; set; }

    }
}
