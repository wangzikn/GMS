using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.Models.BindingModels;
using GMS.Models.ModelView;
using GMS.Utility;

namespace GMS.Data.Interfaces
{
    public interface IAccountModelResponsitory
    {
        //Task<bool> SignInAsync(LoginBindingModel loginModel);
        Task SignOut();
    }
}
