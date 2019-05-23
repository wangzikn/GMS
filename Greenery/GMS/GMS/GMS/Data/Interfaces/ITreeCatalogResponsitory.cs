using GMS.Models;
using GMS.Models.BindingModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Data.Interfaces
{
    public interface ITreeCatalogResponsitory
    {
        IEnumerable<TreeCatalog> TreeCatalogs { get; }
        Task AddAsync(TreeBindingModel newTree);
        TreeCatalog GetTreeCatalogFromId(int id);
        Task DeleteAsync(List<int>  id);
        Task EditAsync(TreeBindingModel treeCatalog);
        TreeCatalog GetTreeCatalogFromName(string NameTree);
        List<TreeCatalog> GetTreeCatalogs();
       
    }
}
