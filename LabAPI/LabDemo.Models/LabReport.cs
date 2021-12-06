namespace LabDemo.Models
{
    public class LabReport
    {
        public int LRId { get; set; }
        public TestTypeEnum Type { get; set; }
        public string Result { get; set; }
        public DateTime TestTime { get; set; }
        public DateTime EnteredTime { get; set; }
        public int PId { get; set; }  
    }
}
