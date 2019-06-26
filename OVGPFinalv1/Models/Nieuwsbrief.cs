using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models
{
    public class Nieuwsbrief
    {
        public Nieuwsbrief()
        {
            PostedDate = DateTime.Now;
        }

        public int NieuwsbriefId { get; set; }
        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Onderwerp")]
        public string Onderwerp { get; set; }
        [Required]
        [Display(Name = "Email inhoud")]
        public string Text { get; set; }
        [Display(Name = "Gepost op")]
        public DateTime PostedDate { get; set; }
    }
}
