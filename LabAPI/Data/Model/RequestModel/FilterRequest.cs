using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LaboratoryAPI.Data.Model.Enum;

namespace LaboratoryAPI.Data.Model
{
    public class ReportFilterRequest
    {
        public TestType Type { get; set; }
        public DateTime CreatedOnStart { get; set; }
        public DateTime CreatedOnEnd { get; set; }

    }
}
