﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models.Email_Models
{
    public class EmailServerConfiguration
    {
        public EmailServerConfiguration(int _smtpPort = 587)
        {
            //587 for Outlook
            //465 for Email
            SmtpPort = _smtpPort;
        }

        public string SmtpServer { get; set; }
        public int SmtpPort { get; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
    }
}
