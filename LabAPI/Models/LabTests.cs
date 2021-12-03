using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabAPI.Models
{
    public class LabTests
    {
        [Key]
        public int LabTestID { get; set; }
        [ForeignKey("PatientID")]
        public PatientDetails Patients { get; set; }

        [Required]
        public string LabTestName { get; set; }

        [Required]
        public int PatientID { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
