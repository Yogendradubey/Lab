using LabDemo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabDemo.DataProvider
{
    public class LabReport
    {
        [Key]
        public int LRId { get; set; }
        public TestTypeEnum Type { get; set; }
        public string Result { get; set; }
        public DateTime TestTime { get; set; }
        public DateTime EnteredTime { get; set; }
        [ForeignKey("Patient")]
        public int PId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
