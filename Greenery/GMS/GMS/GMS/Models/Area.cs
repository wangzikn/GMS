using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Models
{
    public class Area
    {
        

        [Required,Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AreaId { get; set; } 
        [StringLength(100)]
        public string Province { get; set; }
        [StringLength(100)]
        public string District { get; set; }
        [StringLength(100)]
        public string Ward { get; set; }

        public Area()
        {
           
        }

        public Area(string province, string district, string ward)
        {
            Province = province;
            District = district;
            Ward = ward;
        }

       
    }
}
