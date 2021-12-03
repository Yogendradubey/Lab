using System.ComponentModel.DataAnnotations;

namespace LabAPI.Models.Dtos
{
    public class TestResultDto
    {
        public LabTestDto LabTests { get; set; }

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
