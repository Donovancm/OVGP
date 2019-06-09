using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models.Email_Models
{
    public interface IEmailService
    {
        void Send(EmailMessage message);
    }
}
