using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryAPI.Data.Model
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Result { get; set; }
        public DateTime SampleCollectionDateTime { get; set; }
        public DateTime CreatedOn { get; set; }
        public int PatientId { get; set; }
    }
}
