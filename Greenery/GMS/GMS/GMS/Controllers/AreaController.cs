using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GMS.Controllers
{
    public class AreaController : Controller
    {
        private IAreaRepository _areaRepository;

        public AreaController(IAreaRepository areaRepository)
        {
            this._areaRepository = areaRepository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTree()
        {
            return Ok(_areaRepository.GetAreas());
        }

        public IActionResult SaveTree([FromBody] List<Area_Temp> data)
        {
            _areaRepository.UpdateAreas(data);
            return Ok();
        }
    }
}
