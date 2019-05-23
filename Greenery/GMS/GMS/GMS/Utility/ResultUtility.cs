using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GMS.Utility
{
    public class ResultUtility
    {
        public bool status;
        public List<string> errorsList;
        public ResultUtility(bool status, List<string> errorsList)
        {
            this.status = status;
            this.errorsList = errorsList;
        }
    }
}
