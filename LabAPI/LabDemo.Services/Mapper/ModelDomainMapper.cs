using AutoMapper;
using LabDemo.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabDemo.Services.Mapper
{
    public class ModelDomainMapper : Profile
    {
        public ModelDomainMapper()
        {
            CreateMap<LabReport, Models.LabReport>();
            CreateMap<Models.LabReport, LabReport>();
            CreateMap<Patient, Models.Patient>();
            CreateMap<Models.Patient, Patient>();
        }
    }
}
