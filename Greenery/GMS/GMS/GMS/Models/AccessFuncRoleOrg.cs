using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Models
{
    public class AccessFuncRoleOrg
    {
        [Required,Key]    
        public string UserId { get; set; }
        [Required,Key]
        [StringLength(100)]
        public string FunctionName { get; set; }

        public bool? View { get; set; }

        public bool? Delete { get; set; }

        public bool? Edit { get; set; }

        public bool? Add { get; set; }
        public bool? Import { get; set; }
        public bool? Export { get; set; }
        public Individual User { get; set; }
    }
}
