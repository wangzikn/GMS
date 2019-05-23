using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.Data.Interfaces;
using GMS.Models.ModelView;
using GMS.Utility;
using GMS.Models.BindingModels;
using GMS.Models;

namespace GMS.Data.Responsitories
{
    public class AccountModelResponsitory : IAccountModelResponsitory
    {
        private readonly SignInManager<Individual> _signInManager;

        public AccountModelResponsitory(SignInManager<Individual> signInManager)
        {
            _signInManager = signInManager;
        }

        //public async Task<SignInResult> SignInAsync(LoginBindingModel loginModel)
        //{
        //    var check= await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, true, lockoutOnFailure: true);
        //    if (check.Succeeded)
        //    {

        //        return check.;
        //    }
        //    return false;
        //}
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
            
           
        }
    }
}
