using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Models
{
    public class RoleConfig
    {
        public string id { get; set; }
        public long UserRole { get; set; }       
        public long DecentralizationRole { get; set; }
        public long TreeCatalogRole { get; set; }
    }
    
}
