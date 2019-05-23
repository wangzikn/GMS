using System.Collections.Generic;
using System.Threading.Tasks;
using GMS.Data;
using GMS.Data.Interfaces;
using GMS.Data.Responsitories;
using GMS.Models;
using GMS.Models.BindingModels;
using GMS.Models.ModelView;
using GMS.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GMS.Controllers
{
    public class AccountController : Controller
    {
        private IAccountModelResponsitory _accountModelResponsitory;
        private readonly SignInManager<Individual> _signInManager;

        private ApplicationDbContext _context;
       

        public AccountController(SignInManager<Individual> signInManager, IAccountModelResponsitory accountModelResponsitory, ApplicationDbContext context)
        {
            var a = User;
            _signInManager = signInManager;
            _accountModelResponsitory = accountModelResponsitory;
            this._context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            // IAccountModelResponsitory login = new AccountModelResponsitory(_signInManager);
            //login.SignIn(model);
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/User/Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginBindingModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, lockoutOnFailure: true);
            if (result.Succeeded)
            {

                var resultUtility = new ResultUtility(true, null);
                return Redirect("/User/Index");
            }

            ViewBag.Error = "Tài khoản hoặc mật khẩu không hợp lệ";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountModelResponsitory.SignOut();
            return Redirect("/Account/Login");
        }


        //[HttpGet]
        //public IActionResult Users()
        //{
        //    var users = new List<AccountModel>();/*
        //    users.Add(new AccountModel()
        //    {
        //        Username = "messi.lionel",
        //        FUllName = "Lionel Messi",
        //        PhoneNumber = 0274020200,
        //        Address = "Dãy phố thời thượng - Sài Gòn"
        //    });
        //    users.Add(new AccountModel()
        //    {
        //        Username = "ronaldo.cristiano",
        //        FUllName = "Cristiano Ronaldo",
        //        PhoneNumber = 0274020200,
        //        Address = "Dãy phố thời thượng - Sài Gòn"
        //    });
        //    */
        //    return Ok(users);
        //}
    }
}
