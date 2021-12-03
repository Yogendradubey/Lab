using AutoMapper;
using LabAPI.Models;
using LabAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabAPI.LabMapper
{
    public class LabMappings : Profile
    {
        public LabMappings()
        {            
            CreateMap<PatientDetails, PatientDetailsDto>().ReverseMap();
            CreateMap<LabTests, LabTestDto>().ReverseMap();
            CreateMap<TestResult, TestResultDto>().ReverseMap();
        }
    }
}
