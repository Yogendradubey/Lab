using System;
using System.ComponentModel.DataAnnotations;

namespace LabAPI.Models.Dtos
{
    public class PatientDetailsDto
    {
        public int PatientID { get; set; }
        [Required]
        public string PatientName { get; set; }
        [Required]
        public string Doctor { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public byte[] PatientPicture { get; set; }
    }
}
