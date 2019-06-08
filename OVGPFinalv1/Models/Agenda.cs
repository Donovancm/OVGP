using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models
{
    public class Agenda
    {
        [Required]
        public int AgendaId { get; set; }
        [Required]
        public string Titel { get; set; }
        public string Tekst { get; set; }

        public string Label { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
    }
}
