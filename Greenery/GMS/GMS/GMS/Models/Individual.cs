using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Models
{
    public class Individual: IdentityUser
    {

        [StringLength(100)]
        public string Identity { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int? AreaId { get; set; }
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? DateOfIssue { get; set; }
        [StringLength(100)]
        public string PlaceOfIssue { get; set; }
        public string ManagerId { get; set; }
        public string IndivType { get; set; }
        public string RolePersonal { get; set; }
        public string Organization { get; set; }
        public string OrgAddress { get; set; }
        public AreaConfig Area { get; set; }
        public Role Role { get; set; }
        public ICollection<AccessFuncRoleOrg> AccessFuncRoleOrg { get; set; }
    }
}
