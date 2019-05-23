using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Models
{
    public class Role
    {
        [Key]
        public string UserId { get; set; }
        //[Display(Name = "Quyền người dùng")]
        public long UserRole { get; set; }
        //[Display(Name = "Quyền phân quyền")]
        public long DecentralizationRole { get; set; }
        //[Display(Name = "Quyền danh mục cây xanh")]
        public long TreeCatalogRole { get; set; }
        public Individual User { get; set; }

    }

    
}
