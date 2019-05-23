using GMS.Data.Interfaces;
using GMS.Models;
using GMS.Models.BindingModels;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Data.Responsitories
{
    public class TreeCatalogResponsitory: ITreeCatalogResponsitory
    {
       
        private readonly ApplicationDbContext _ProjectContext;

        public TreeCatalogResponsitory( ApplicationDbContext ProjectContext)
        {
            
            _ProjectContext = ProjectContext;
        }

        public IEnumerable<TreeCatalog> TreeCatalogs => _ProjectContext.TreeCatalog;

        public async Task AddAsync(TreeBindingModel newTreeCatalog)
        {
            var item = _ProjectContext.TreeCatalog.FirstOrDefault(x => x.ScientificName == newTreeCatalog.ScientificName);
            if (item == null)
            {
                TreeCatalog tree = new TreeCatalog
                {
                    Name=newTreeCatalog.TreeName,
                    ScientificName=newTreeCatalog.ScientificName,
                    Url=newTreeCatalog.Url
                };
                _ProjectContext.TreeCatalog.Add(tree);
                await _ProjectContext.SaveChangesAsync();
            }
            
        }

        public async Task DeleteAsync(List<int> id)
        {
            
            foreach (int i in id)
            {
                var item = await _ProjectContext.TreeCatalog.FindAsync(i);
                if (item != null)
                {
                    _ProjectContext.TreeCatalog.Remove(item);

                }
            }
            
            
            await _ProjectContext.SaveChangesAsync();

        }

        public async Task EditAsync(TreeBindingModel treeCatalog)
        {
            var item = await _ProjectContext.TreeCatalog.FindAsync(treeCatalog.ScientificName);

            if (item != null)
            {
                item.Name = treeCatalog.TreeName;
                item.ScientificName = treeCatalog.ScientificName;
                item.Description = treeCatalog.Description;
                item.Url = treeCatalog.Url;
                _ProjectContext.TreeCatalog.Update(item);
                await _ProjectContext.SaveChangesAsync();
            }
            
        }


        public TreeCatalog GetTreeCatalogFromName(string NameTree)
        {
            return _ProjectContext.TreeCatalog.FirstOrDefault(x => x.Name == NameTree);
        }
        public List<TreeCatalog> GetTreeCatalogs()
        {
            return _ProjectContext.TreeCatalog.ToList();
        }

        public TreeCatalog GetTreeCatalogFromId(int id)
        {
            return _ProjectContext.TreeCatalog.FirstOrDefault(x => x.Id == id);
        }
    }
}
