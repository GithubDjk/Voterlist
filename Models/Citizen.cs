using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VoterListApp.Models
{
    public class Citizen 
    {
        [Key]
        public int id { get; set; }


        [Required(ErrorMessage ="Name is Invalid")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Father's Name is Invalid")]
        [DisplayName("Father's Name")]
        public string fatherName { get; set; }


        public string Sex { get; set; }


        [Range(18, int.MaxValue, ErrorMessage = "Age must be above 18")]
        [Required(ErrorMessage = "Age is Invalid")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Address is Invalid")]
        public string Address { get; set; }

    }
}
