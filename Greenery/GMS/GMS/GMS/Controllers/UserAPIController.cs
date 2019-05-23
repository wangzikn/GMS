using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.Data.Interfaces;
using GMS.Models;
using GMS.Models.BindingModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GMS.Controllers
{
    public class UserAPIController : Controller
    {
        //private IIndividualResponsitory _userRepository;

        //public UserAPIController(IIndividualResponsitory userRepository)
        //{
        //    this._userRepository = userRepository;
        //}
        //[HttpPost]
        //public async Task<IActionResult> AddUserAsync(UserBindingModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var invidiual = new Individual { Email = model.Email, UserName = model.UserName, Name = model.FullName, PasswordHash = model.Password };
        //            var result = await _userRepository.AddAsync(invidiual);
        //            return Ok(result);
        //        }
        //        catch (Exception e)
        //        {

        //        }
        //    }
        //    return BadRequest();
        //}

        [HttpGet]
        public IActionResult Users()
        {
            var users = new List<UserBindingModel>();
            users.Add(new UserBindingModel()
            {
                UserName = "messi.lionel",
                FullName = "Lionel Messi",
                PhoneNumber = "0274020200",
                Address = "Dãy phố thời thượng - Sài Gòn"
            });
            users.Add(new UserBindingModel()
            {
                UserName = "ronaldo.cristiano",
                FullName = "Cristiano Ronaldo",
                PhoneNumber = "0274020200",
                Address = "Dãy phố thời thượng - Sài Gòn"
            });
            users.Add(new UserBindingModel()
            {
                UserName = "ronaldo.cristiano2",
                FullName = "Cristiano Ronaldo",
                PhoneNumber = "0274020200",
                Address = "Dãy phố thời thượng - Sài Gòn"
            });
            users.Add(new UserBindingModel()
            {
                UserName = "ronaldo.cristiano3",
                FullName = "Cristiano Ronaldo",
                PhoneNumber = "0274020200",
                Address = "Dãy phố thời thượng - Sài Gòn"
            });
            users.Add(new UserBindingModel()
            {
                UserName = "ronaldo.cristiano4",
                FullName = "Cristiano Ronaldo",
                PhoneNumber = "0274020200",
                Address = "Dãy phố thời thượng - Sài Gòn"
            });
            users.Add(new UserBindingModel()
            {
                UserName = "ronaldo.cristiano5",
                FullName = "Cristiano Ronaldo",
                PhoneNumber = "0274020200",
                Address = "Dãy phố thời thượng - Sài Gòn"
            });
            return Ok(users);
        }
    }
    
}
