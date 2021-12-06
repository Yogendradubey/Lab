using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryAPI.Data.Model
{
    public class Patient
    {
        [Key]
        public int Id {get; set;}

        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public string Gender { get; set; }

        public DateTime CretedOn { get; set; }
    }
}
