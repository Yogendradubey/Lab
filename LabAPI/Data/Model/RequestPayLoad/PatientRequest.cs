using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LaboratoryAPI.Data.Model.Enum;

namespace LaboratoryAPI.Data.Model.RequestModel
{
    public class PatientRequest
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public GenderType Gender { get; set; }
    }
}

