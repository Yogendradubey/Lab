using System.ComponentModel.DataAnnotations;

namespace LabDemo.DataProvider
{
    public class Patient
    {
        [Key]
        public int PId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public virtual List<LabReport> LabReports { get; set; }

    }
}
