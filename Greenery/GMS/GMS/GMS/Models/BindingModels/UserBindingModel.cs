using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Models.BindingModels
{
  
    public class UserBindingModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
        public string FullName { get; set; }
      
        public string Email { get; set; }

        public string Identity { get; set; }

        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? DateOfIssue { get; set; }
        [StringLength(100)]
        public string PlaceOfIssue { get; set; }
        public string PhoneNumber { get; set; }
        public string ManagerId { get; set; }
        public string IndivType { get; set; }
        public string RolePersonal { get; set; }
        public string Organization { get; set; }
        public string OrgAddress { get; set; }
    }
}
