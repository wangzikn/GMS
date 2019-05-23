using GMS.Data.Responsitories;
using GMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Data.Interfaces
{
    public interface IRoleResponsitory
    {

        Role GetRoleById(string id);
        void AddAsync(Role newRole);
        bool HasPermission(Individual user, PermissionGroup permissionGroup, Permission permission);
        bool HasCurrentUserPermission(PermissionGroup permissionGroup, Permission permission);
      
    }
}
