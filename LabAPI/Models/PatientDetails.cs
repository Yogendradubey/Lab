using System;
using System.ComponentModel.DataAnnotations;

namespace LabAPI.Models
{
    public class PatientDetails
    {
        [Key]
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
