using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabAPI.Models
{
    public class TestResult
    {
        [ForeignKey("LabTestID")]
        public LabTests LabTests { get; set; }

        [Required]
        public int LabTestID { get; set; }
        [Key]
        public int TestResultID { get; set; }
        [Required]
        public string TestName { get; set; }

        [Required]
        public string Result { get; set; }

        [Required]
        public string NormalRange { get; set; }

        public string Units { get; set; }
    }
}
