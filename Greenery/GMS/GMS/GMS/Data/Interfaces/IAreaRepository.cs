using GMS.Data.Responsitories;
using GMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Data.Interfaces
{
    public interface IAreaRepository
    {
        List<Area_Temp> GetAreas();
        void UpdateAreas(List<Area_Temp> data);
        //void AddAsync(Area area);
    }

    public class Area_Temp
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public ICollection<Area_Temp> Children { get; set; }
    }
}
