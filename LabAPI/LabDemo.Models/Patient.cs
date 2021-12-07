using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabDemo.Models
{
    public class Patient
    {
        public int PId { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mobile is required")]        
        public string Mobile { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "DOB is required")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public GenderEnum Gender { get; set; }
    }
}
