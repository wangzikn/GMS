using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GMS.Data;
using GMS.Data.Interfaces;
using GMS.Data.Responsitories;
using GMS.Models;
using GMS.Models.BindingModels;
using GMS.Models.ModelView;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GMS.Controllers
{
    public class TreeController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly HostingEnvironment _hostingEnvironment;

        public TreeController(ApplicationDbContext context, HostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


     

        public IActionResult AddTree()
        {
            return View();
        }
        [HttpGet, ActionName("information")]
        public IActionResult Render()
        {
            ITreeCatalogResponsitory tree = new TreeCatalogResponsitory(_context);
            var trees = tree.GetTreeCatalogs().Select(x => new
            {
                TreeName = x.Name,
                ScitificName = x.ScientificName,
                Id = x.Id,
                Url = x.Url
            });
            return Ok(trees);
        }

        [HttpPost, ActionName("deleteTree")]
        public async Task<IActionResult> DeleteTree([FromBody]List<int> id)
        {
            if (ModelState.IsValid)
            {
                ITreeCatalogResponsitory tree = new TreeCatalogResponsitory(_context);
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                foreach(int i in id)
                {
                    var t = tree.GetTreeCatalogFromId(i);
                    string url = t.Url.Substring(8);
                    System.IO.File.Delete(Path.Combine(uploads, url));
                }              
                await tree.DeleteAsync(id);

                //return Redirect("/User/trees");
            }
            return Ok();
        }
        [Route("[controller]/AddTree/{username}")]
        public IActionResult Edit(string treename)
        {

            return View("AddTree");
        }
        [HttpPost]
        public async Task<IActionResult> EditTree(TreeBindingModel model)
        {
            if (ModelState.IsValid)
            {
                ITreeCatalogResponsitory tree = new TreeCatalogResponsitory(_context);
                string url = model.Url.Substring(8);
                var file = model.File;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                var extension = Path.GetExtension(file.FileName);
                if (System.IO.File.Exists(Path.Combine(uploads, url)))
                {
                    System.IO.File.Delete(Path.Combine(uploads,url));
                }
                var id = Guid.NewGuid().ToString();
                using (var fs = new FileStream(Path.Combine(uploads, id + extension), FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                model.Url = $"//images/{id + extension}".Substring(1);
                await tree.EditAsync(model);
                return Redirect("/Trees");
            }
            return Ok();
        }
        [HttpGet, ActionName("TreeInformation")]
        public IActionResult RenderTrees(TreeCatalog model)
        {
            ITreeCatalogResponsitory tree = new TreeCatalogResponsitory(_context);

            var Trees = tree.GetTreeCatalogs().Select(x => new
            {
                name = x.Name,
                scientificName = x.ScientificName,
                description = x.Description,
                id = x.Id,
                url = x.Url
            });

            return Ok(Trees);
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(TreeBindingModel model)
        {
            if (ModelState.IsValid)
            {
                ITreeCatalogResponsitory tree = new TreeCatalogResponsitory(_context);
                var file = model.File;
                if (file.Length > 0)
                {
                    string extension = Path.GetExtension(file.FileName);
                    string path = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    var id = Guid.NewGuid().ToString();
                    using (var fs = new FileStream(Path.Combine(path, id + extension), FileMode.Create))
                    {
                        file.CopyTo(fs);
                    }
                    model.Url = $"//images/{id + extension}".Substring(1);
                    await tree.AddAsync(model);
                    return Redirect("User/Trees");
                }
                
            }
            return BadRequest();
        }


        [Route("[controller]/GetInfo/{username}")]
        public IActionResult GetInfo(string username)
        {
            ITreeCatalogResponsitory tree = new TreeCatalogResponsitory(_context);
            var t= tree.GetTreeCatalogFromName(username);
            var tr = new TreeBindingModel
            {
                TreeName = t.Name,
                ScientificName = t.ScientificName,
                Description = t.Description,
                Url = t.Url
            };
            return Ok(tr);
        }
    }
}