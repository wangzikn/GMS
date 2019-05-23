using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.Data.Interfaces;
using GMS.Data.Responsitories;
using GMS.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GMS.Controllers
{
    public class AreaAPIController : Controller
    {
        private IAreaRepository _areaRepository;

        public AreaAPIController(IAreaRepository areaResponsitory)
        {
            this._areaRepository = areaResponsitory;
        }

        [HttpGet]
        public IActionResult GetAreaTrees(int? id = null)
        {

            ///var trees = _areaRepository.GetAreaTree(id);
            return Ok(new
            {
                id = 1,
                name = "Binh Duong",
                children = new[] {
                    new { id = 1, name = "Binh Duong", children = new object[]{ } },
                    new { id = 1, name = "Binh Duong", children = new object []{ } }
                   }
            });

        }
    }
}
