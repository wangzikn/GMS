using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.Data;
using GMS.Data.Interfaces;
using GMS.Data.Responsitories;
using GMS.Models;
using GMS.Models.BindingModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GMS.Controllers
{
    public class UserController : Controller
    {

        private UserManager<Individual> _userManager;
        private ApplicationDbContext _context;

        public UserController(UserManager<Individual> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, ActionName("information")]
        public IActionResult RenderUsers(UserBindingModel model)
        {
            IIndividualResponsitory individual = new IndividualResponsitory(_userManager, _context);
            var id = _userManager.GetUserId(HttpContext.User);
            var users = individual.GetIndividualsByManagerId(id).Select(x => new
            {
                userName = x.UserName,
                fullName = x.Name,
                address = x.Address,
                phoneNumber = x.PhoneNumber,
                id = x.Id
            });

            return Ok(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [Route("[controller]/Edit/{username}")]
        public IActionResult Edit(string username)
        {

            return View("Edit");
        }

        [Route("[controller]/GetInfo/{username}")]
        public IActionResult GetInfo(string username)
        {
            IIndividualResponsitory userRepository = new IndividualResponsitory(_userManager, _context);
            var userInfo = userRepository.GetIndividualsByName(username);
            var viewModel = new UserBindingModel
            {
                UserName = userInfo.UserName,
                FullName = userInfo.Name,
                PhoneNumber = userInfo.PhoneNumber,
                Birthday = userInfo.Birthday,
                Identity = userInfo.Identity,
                DateOfIssue = userInfo.DateOfIssue,
                PlaceOfIssue = userInfo.PlaceOfIssue,
                Address = userInfo.Address,
                RolePersonal = userInfo.RolePersonal,
                Organization = userInfo.Organization,
                OrgAddress = userInfo.OrgAddress,
                Email = userInfo.Email
            };
            return Ok(viewModel);

        }



        [HttpPost]
        public async Task<IActionResult> AddUser(UserBindingModel model)
        {
            if (ModelState.IsValid)
            {
                IIndividualResponsitory users = new IndividualResponsitory(_userManager, _context);
                model.ManagerId = _userManager.GetUserId(HttpContext.User);
                var result = await users.AddAsync(model);
                return Redirect("User/Index");
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserBindingModel model)
        {

            IIndividualResponsitory individual = new IndividualResponsitory(_userManager, _context);
            var user = individual.GetIndividualsByName(model.UserName);
            await individual.EditAsync(user.Id, model);
            return View("Edit");
        }

        public IActionResult Trees()
        {
            return View();
        }
        public IActionResult RegisterTree()
        {
            return View();
        }
        public IActionResult AddTree()
        {
            return View();
        }
        [HttpGet, ActionName("getId")]
        public IActionResult GetRolebyId(string id)
        {
            IRoleResponsitory individual = new RoleReponsitory(_context);
            //var id = _userManager.GetUserId(HttpContext.User);
            Role mRole = individual.GetRoleById(id);
            List<long> role = new List<long>() { mRole.UserRole, mRole.DecentralizationRole, mRole.TreeCatalogRole };
            return Ok(role);
        }

        [HttpPost, ActionName("addRole")]
        public IActionResult AddRole([FromBody] Role[] things)
        {
            IRoleResponsitory individual = new RoleReponsitory(_context);

            foreach (Role role in things)
            {
                individual.AddAsync(role);
            }
            return Ok();
        }

        public IActionResult AssignRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync([FromBody]List<string> id)
        {
            IIndividualResponsitory individualResponsitory = new IndividualResponsitory(_context);
            await individualResponsitory.DeleteAsync(id);

            return Ok();
        }

    }
}
