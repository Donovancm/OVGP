using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Naam vereist")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email vereist")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Onderwerp vereist")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Bericht vereist")]
        public string Message { get; set; }
    }
}
