using System.Collections.Generic;
using System.Threading.Tasks;
using GMS.Models;
using GMS.Utility;

namespace GMS.Data.Interfaces
{
    public interface IAccessFuncRoleResponsitory
    {
        IEnumerable<AccessFuncRoleOrg> AccessFuncRoleOrgs { get; }
        Task<ResultUtility> Add(AccessFuncRoleOrg accessFuncRoleOrg);
        Task<ResultUtility> Delete(string id);
        Task<ResultUtility> Edit(string id, AccessFuncRoleOrg accessFuncRoleOrg);
    }
}