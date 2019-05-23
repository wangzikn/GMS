using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.Models;
using GMS.Models.BindingModels;
using GMS.Models.ModelView;
using Microsoft.AspNetCore.Identity;

namespace GMS.Data.Interfaces
{
    public interface IIndividualResponsitory
    {
       // IEnumerable<Individual> Individuals { get; }
        Task<IdentityResult> AddAsync(UserBindingModel user);
        List<Individual> GetIndividualsById(string id);
        List<Individual> GetIndividualsByManagerId(string ManangerId);
        Individual GetIndividualsByName(string Name);
        Task DeleteAsync(List<string> id);
        Task EditAsync(string Id, UserBindingModel individual);
        
    }
}
