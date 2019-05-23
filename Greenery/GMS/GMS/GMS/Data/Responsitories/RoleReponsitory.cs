using GMS.Data.Interfaces;
using GMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Data.Responsitories
{
    public enum PermissionGroup
    {
        User,
        Tree,
        Role
    }

    public enum Permission
    {
        VIEW = 1,
        CREATE = 2,
        EDIT = 4,
        DELETE = 8,
        IMPORT = 16,
        EXPORT = 32
    }

    public class RoleReponsitory:IRoleResponsitory
    {       

        private readonly ApplicationDbContext _ProjectContext;

        public RoleReponsitory(ApplicationDbContext ProjectContext)
        {
            _ProjectContext = ProjectContext;
        }

        public IEnumerable<Role> Roles => _ProjectContext.Role;
        public List<RoleConfig> GetRoles()
        {
            var res = new List<RoleConfig>();
            if (File.Exists("role.json"))
            {
                var data = File.ReadAllText("role.json");
                res = JsonConvert.DeserializeObject<List<RoleConfig>>(data);
            }
            return res;
        }
        //private static ICollection<RoleConfig> areas;
        public void UpdateRoles(List<RoleConfig> roleData)
        {
            File.WriteAllText("role.json", JsonConvert.SerializeObject(roleData));
        }
        public void AddAsync(Role newRole)
        {
            
            var role = _ProjectContext.Role.FirstOrDefault(x => x.UserId == newRole.UserId);
            if (role==null)
            {
                _ProjectContext.Role.Add(newRole);
                _ProjectContext.SaveChangesAsync();
            }
            else
            {
                role.UserRole = newRole.UserRole;
                role.TreeCatalogRole = newRole.TreeCatalogRole;
                role.DecentralizationRole = newRole.DecentralizationRole;
                _ProjectContext.Update(role);
                _ProjectContext.SaveChanges();

            }
                                
        }

       public void DeleteAsync(string id)
        {
            var list = GetRoles();
            var role = list.FirstOrDefault(x => x.id == id);
            list.Remove(role);
            UpdateRoles(list);
        }

       


        public Role GetRoleById(string id)
        {
            return _ProjectContext.Role.FirstOrDefault(x => x.UserId == id);
        }
        public bool HasPermission(Individual user, PermissionGroup permissionGroup, Permission permission)
        {
            var list = GetRoles();
            var role = list.FirstOrDefault(x => x.id == user.Id);
            if (role == null)
            {
                return false;
            }
            else
            {
                var pers = Enum.GetValues(typeof(Permission));
                if (permissionGroup == PermissionGroup.Role)
                {
                    foreach(int i in pers)
                    {
                        if(((int)permission & i) > 0)
                        {
                            return true;
                        }
                    }
                   
                }
                else
                {
                    if (permissionGroup == PermissionGroup.Tree)
                    {
                        foreach (int i in pers)
                        {
                            if (((int)permission & i) > 0)
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if(permissionGroup == PermissionGroup.User)
                        {
                            foreach (int i in pers)
                            {
                                if (((int)permission & i) > 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }      
            }
            return false;
         }
        public bool HasCurrentUserPermission(PermissionGroup permissionGroup, Permission permission)
        {
            var pers = Enum.GetValues(typeof(Permission));
            if (permissionGroup == PermissionGroup.Role)
            {
                foreach (int i in pers)
                {
                    if (((int)permission & i) > 0)
                    {
                        return true;
                    }
                }

            }
            else
            {
                if (permissionGroup == PermissionGroup.Tree)
                {
                    foreach (int i in pers)
                    {
                        if (((int)permission & i) > 0)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (permissionGroup == PermissionGroup.User)
                    {
                        foreach (int i in pers)
                        {
                            if (((int)permission & i) > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }                 
    }
}
