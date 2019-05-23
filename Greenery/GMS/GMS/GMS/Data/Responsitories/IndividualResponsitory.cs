using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GMS.Data.Interfaces;
using GMS.Models;
using GMS.Models.BindingModels;
using GMS.Models.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GMS.Data.Responsitories
{

    public class IndividualResponsitory : IIndividualResponsitory
    {
        private readonly ApplicationDbContext _ProjectContext;

        private UserManager<Individual> _userManager;

        public IndividualResponsitory(ApplicationDbContext ProjectContext)
        {
            _ProjectContext = ProjectContext;
        }

        public IndividualResponsitory(UserManager<Individual> userManager, ApplicationDbContext ProjectContext)
        {
            _ProjectContext = ProjectContext;
            _userManager = userManager;
        }

        // public IEnumerable<Individual> Individuals => _ProjectContext.Individual;
        public async Task<IdentityResult> AddAsync(UserBindingModel user)
        {
            //string error = "";
            //try
            //{

            //}
            //catch(Exception e)
            //{
            //    error=e.Message;
            //}
            //return IdentityResult.Failed();

            var User = new Individual
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.FullName,
                Address = user.Address,
                OrgAddress = user.OrgAddress,
                Organization = user.Organization,
                Identity = user.Identity,
                IndivType = user.IndivType,
                ManagerId = user.ManagerId,
                Birthday = user.Birthday,
                DateOfIssue = user.DateOfIssue,
                PhoneNumber = user.PhoneNumber,
                PlaceOfIssue = user.PlaceOfIssue,
                RolePersonal = user.RolePersonal

            };
            var result = await _userManager.CreateAsync(User, user.Password);


            return result;

        }
        public async Task DeleteAsync(List<string> id)
        {

            foreach (string i in id)
            {
                Individual individual = (Individual)_ProjectContext.Users.FirstOrDefault(p => p.Id == i);
                if (individual != null)
                {
                    _ProjectContext.Users.Remove(individual);
                    await _ProjectContext.SaveChangesAsync();

                }

            }


        }
        public async Task EditAsync(string Id, UserBindingModel individual)
        {
            //_ProjectContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var itemToUpdate = _ProjectContext.Users.FirstOrDefault(x => x.Id == Id);
            if (itemToUpdate != null)
            {

                itemToUpdate.Identity = individual.Identity;
                itemToUpdate.IndivType = individual.IndivType;
                itemToUpdate.Name = individual.FullName;
                itemToUpdate.PhoneNumber = individual.PhoneNumber;
                itemToUpdate.Birthday = individual.Birthday;
                itemToUpdate.Address = individual.Address;
                itemToUpdate.Email = individual.Email;
                itemToUpdate.DateOfIssue = individual.DateOfIssue;
                itemToUpdate.PlaceOfIssue = individual.PlaceOfIssue;
                itemToUpdate.RolePersonal = individual.RolePersonal;
                itemToUpdate.OrgAddress = individual.OrgAddress;
                itemToUpdate.Organization = individual.Organization;
                if (individual.Password == null)
                {
                    var a = "";
                    try
                    {
                         _ProjectContext.Users.Update(itemToUpdate);
                    }catch(Exception e)
                    {
                        a = e.Message;
                    }
                    _ProjectContext.SaveChanges();


                }
                else
                {
                    _ProjectContext.Users.Update(itemToUpdate);
                    _ProjectContext.SaveChanges();
                    var code = await _userManager.GeneratePasswordResetTokenAsync(itemToUpdate);
                    var user = await _userManager.FindByNameAsync(itemToUpdate.UserName);
                    var result = await _userManager.ResetPasswordAsync(user, code, individual.Password);
                   
                   

                }
               

            }

        }

        public List<Individual> GetIndividualsByManagerId(string ManangerId)
        {

            return _ProjectContext.Users.Where(x => x.ManagerId == ManangerId).ToList();
        }
        public List<Individual> GetIndividualsById(string id)
        {
            return _ProjectContext.Users.Where(x => x.Id == id).ToList();
        }
        public Individual GetIndividualsByName(string Name)
        {
            return _ProjectContext.Users.FirstOrDefault(x => x.UserName == Name);
        }
    }

}
