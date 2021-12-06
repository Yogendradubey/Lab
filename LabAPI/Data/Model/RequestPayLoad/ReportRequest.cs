using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LaboratoryAPI.Data.Model.Enum;

namespace LaboratoryAPI.Data.Model.RequestModel
{
    public class ReportRequest
    {
        public TestType Type { get; set; }
        public ResultType Result { get; set; }
        public DateTime SampleCollectionDateTime { get; set; }
        public int PatientId { get; set; }
    }   
}
