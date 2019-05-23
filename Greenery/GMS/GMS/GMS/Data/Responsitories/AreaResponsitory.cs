using GMS.Data.Interfaces;
using GMS.Models;
using GMS.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Data.Responsitories
{
   
    public class District
    {
        public string nameDistrict;
        public List<string> GetWards=new List<string>();
    }
   
    public class AreaResponsitory :IAreaRepository
    {
        private readonly ApplicationDbContext _ProjectContext;

        public AreaResponsitory(ApplicationDbContext ProjectContext)
        {
            _ProjectContext = ProjectContext;
        }



        public IEnumerable<AreaConfig> Areas => _ProjectContext.AreaConfig;
        public List<Area_Temp> GetAreas()
        {
            var res = new List<Area_Temp>();
            if (File.Exists("area.json"))
            {
                var data = File.ReadAllText("area.json");
                res = JsonConvert.DeserializeObject<List<Area_Temp>>(data);
            }
            return res;
        }
        private static ICollection<AreaConfig> areas;
        public void UpdateAreas(List<Area_Temp> areaData)
        {
            File.WriteAllText("area.json", JsonConvert.SerializeObject(areaData));
        }
        //public void AddAsync(Area area)
        //{
        //    var list = GetAreas();
            
        //    var areas = list.FirstOrDefault(x => x.Text == area.Province);

        //    if (areas == null)
        //    {
        //        var ProvinceId = Guid.NewGuid();
        //        ICollection<District> area_Temps = new List<District>();
        //        District d = new District();
        //        d.GetWards.Add(area.Ward);
        //        d.nameDistrict = area.District;
        //        area_Temps.Add(d);
        //        Area_Temp province = new Area_Temp
        //        {
        //            Id = ProvinceId.ToString(),
        //            Text = area.Province,
        //            Children = area_Temps
        //        };
        //        list.Add(province);
                
        //        UpdateAreas(list);
        //    }
        //    else
        //    {
        //        var District = areas.Children.FirstOrDefault(x => x.nameDistrict == area.District);
        //        if (District == null )
        //        {
        //            list.Remove(areas);
        //            District d = new District();
        //            d.GetWards.Add(area.Ward);
        //            d.nameDistrict = area.District;
        //            areas.Children.Add(d);
        //            list.Add(areas);
        //        }
        //        else
        //        {
        //            bool check = District.GetWards.Contains(area.Ward);
        //            if (check == false)
        //            {
        //                list.Remove(areas);
        //                areas.Children.Remove(District);
        //                District.GetWards.Add(area.Ward);
        //                areas.Children.Add(District);
        //                list.Add(areas);
        //                UpdateAreas(list);
        //            }
        //        }
        //    }
        //}
        
    }

}
