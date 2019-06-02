using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models
{
    public class User
    {
        [Key]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Stad { get; set; }
        public string Land { get; set; }
        public string Postcode { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int TelefoonNummer { get; set; }
    }
}
