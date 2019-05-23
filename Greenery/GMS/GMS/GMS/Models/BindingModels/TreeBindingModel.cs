using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GMS.Models.BindingModels
{
    public class TreeBindingModel
    {
        
        public IFormFile File { get; set; }
        public string TreeName { get; set; }
        public string ScientificName { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
