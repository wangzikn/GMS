using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Data
{
    public interface IDbInitializer
    {
         Task Initialize();
    }
}
