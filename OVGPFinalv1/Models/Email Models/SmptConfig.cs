using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models.Email_Models
{
    public class SmptConfig
    {
        public int port { get; set; }
        public string server { get; set; }
        public string smtpUsername { get; set; }
        public string smtpPassword { get; set; }
        public string from { get; set; }
    }
}