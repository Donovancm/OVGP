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
        [DataType(DataType.MultilineText)]
        public string Tekst { get; set; }
        public string Label { get; set; }
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        [DataType(DataType.Time)]
        public DateTime Tijd { get; set; }
    }
}
