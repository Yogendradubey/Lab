using System;
using System.ComponentModel.DataAnnotations;

namespace LabAPI.Models.Dtos
{
    public class LabTestDto
    {
        public int LabTestID { get; set; }
        public PatientDetailsDto Patients { get; set; }
        [Required]
        public int PatientID { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        [Required]
        public string LabTestName { get; set; }
    }
}
