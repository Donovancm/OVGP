using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models
{
    public class User : IdentityUser
    {
        [Required]
        [PersonalData]
        public string Bedrijf { get; set; }
        [Required]
        [PersonalData]
        public string ContactPersoon { get; set; }
        [Required]
        [PersonalData]
        public string Adres { get; set; }
        [Required]
        [PersonalData]
        public string Postcode { get; set; }
        [Required]
        [PersonalData]
        public string Plaats { get; set; }
        [Required]
        [PersonalData]
        public bool Nieuwsbrief { get; set; }
    }
}
