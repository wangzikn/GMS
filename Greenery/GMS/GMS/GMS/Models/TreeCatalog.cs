using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace GMS.Models
{
    public class TreeCatalog
    {       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Tên")]
        [StringLength(100)]
        public string Name { get; set; }
        [Display(Name = "Tên khoa học")]
        [StringLength(200)]
        public string ScientificName { get; set; }
        [Display(Name = "Mô tả")]
        [StringLength(5000)]
        public string Description { get; set; }
        [StringLength(200)]
        public string Url { get; set; }
      
    }
}
