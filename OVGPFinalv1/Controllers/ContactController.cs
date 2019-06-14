using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OVGPFinalv1.Models;
using OVGPFinalv1.Models.Email_Models;

namespace OVGPFinalv1.Controllers
{
    public class ContactController : Controller
    {
        private EmailAddress FromAndToEmailAddress;
        private IEmailService EmailService;
        public ContactController(EmailAddress _fromAddress, IEmailService _emailService)
        {
            FromAndToEmailAddress = _fromAddress;
            EmailService = _emailService;
        }
        public ViewResult Index()
        {
            return View();
        }
        public IActionResult ContactResult()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                EmailMessage msgToSend = new EmailMessage
                {
                    FromAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                    ToAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                    Content = $"Here is your message: Name: {model.Name}, " +
                        $"Email: {model.Email}, Message: {model.Message}",
                    Subject = "Contact Form - Basic Contact Form App"
                };

                EmailService.Send(msgToSend);
                return RedirectToAction("ContactResult");
            }
            else
            {
                return Index();
            }
        }
    }
}