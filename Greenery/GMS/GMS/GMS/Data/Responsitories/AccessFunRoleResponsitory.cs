using GMS.Data.Interfaces;
using GMS.Models;
using GMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Data.Responsitories
{
    public class AccessFunRoleResponsitory:IAccessFuncRoleResponsitory
    {
        private readonly ApplicationDbContext _ProjectContext;

        public AccessFunRoleResponsitory(ApplicationDbContext ProjectContext)
        {
            _ProjectContext = ProjectContext;
        }

        public IEnumerable<AccessFuncRoleOrg> AccessFuncRoleOrgs => _ProjectContext.AccessFuncRoleOrg;

        public async Task<ResultUtility> Add(AccessFuncRoleOrg accessFuncRoleOrg)
        {
            bool status;
            List<String> errors = new List<string>();
            ResultUtility ResultUtility;

            var accessFuncRole = _ProjectContext.AccessFuncRoleOrg.FirstOrDefault(x => x.FunctionName == accessFuncRoleOrg.FunctionName);

            if (accessFuncRole != null)
            {
                status = false;
                errors.Add("Object existed!");
                ResultUtility = new ResultUtility(status, errors);
                return ResultUtility;
            }

            try
            {
                _ProjectContext.AccessFuncRoleOrg.Add(accessFuncRole);
                await _ProjectContext.SaveChangesAsync();
                status = true;

            }
            catch (Exception e)
            {
                status = false;
                errors.Add(e.Message);

            }

            ResultUtility = new ResultUtility(status, errors);
            return ResultUtility;

        }
        public async Task<ResultUtility> Delete(string id)
        {

            bool status;
            List<String> errors = new List<string>();
            ResultUtility ResultUtility;

            var accessFuncRole = _ProjectContext.AccessFuncRoleOrg.FirstOrDefault(p => p.UserId == id);

            if (accessFuncRole == null)
            {
                status = false;
                errors.Add("Object not existed!");
                ResultUtility = new ResultUtility(status, errors);
                return ResultUtility;
            }

            try
            {
                _ProjectContext.AccessFuncRoleOrg.Remove(accessFuncRole);
                await _ProjectContext.SaveChangesAsync();
                status = true;

            }
            catch (Exception e)
            {
                status = false;
                errors.Add(e.Message);

            }

            ResultUtility = new ResultUtility(status, errors);
            return ResultUtility;
        }
        public async Task<ResultUtility> Edit(string id, AccessFuncRoleOrg accessFuncRole)
        {
            bool status;
            List<String> errors = new List<string>();
            ResultUtility ResultUtility;

            var itemToUpdate = _ProjectContext.AccessFuncRoleOrg.FirstOrDefault(x => x.UserId == id);

            if (itemToUpdate == null)
            {
                status = false;
                errors.Add("Object not existed!");
                ResultUtility = new ResultUtility(status, errors);
                return ResultUtility;
            }

            try
            {
                itemToUpdate.FunctionName = accessFuncRole.FunctionName;
                itemToUpdate.View = accessFuncRole.View;
                itemToUpdate.Delete = accessFuncRole.Delete;
                itemToUpdate.Edit = accessFuncRole.Edit;
                itemToUpdate.Add = accessFuncRole.Add;
                itemToUpdate.Export = accessFuncRole.Export;
                itemToUpdate.Import = accessFuncRole.Import;
                _ProjectContext.AccessFuncRoleOrg.Update(itemToUpdate);
                status = true;

            }
            catch (Exception e)
            {
                status = false;
                errors.Add(e.Message);

            }

            ResultUtility = new ResultUtility(status, errors);
            return ResultUtility;
        }
    }
}
