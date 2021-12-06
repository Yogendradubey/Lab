using System.ComponentModel.DataAnnotations;
using LabDemo.Models;
namespace LabDemo.DataProvider
{
    public class Patient
    {
        [Key]
        public int PId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DOB { get; set; }
        public GenderEnum Gender { get; set; }
        public virtual List<LabReport> LabReports { get; set; }

    }
}
