using System.ComponentModel.DataAnnotations;

namespace LabDemo.Models
{
    public class LabReport
    {
        public int LRId { get; set; }
        [Required(ErrorMessage = "Report Type is required")]
        public TestTypeEnum Type { get; set; }
        [Required(ErrorMessage = "Report Type is required")]
        public string Result { get; set; }
        [Required(ErrorMessage = "Test Time is required")]
        public DateTime TestTime { get; set; }
        [Required(ErrorMessage = "Entered Time is required")]
        public DateTime EnteredTime { get; set; }
        [Required(ErrorMessage = "Patient Name is required")]
        public int PId { get; set; }  
    }
}
