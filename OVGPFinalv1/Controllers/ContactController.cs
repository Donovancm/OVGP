using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OVGPFinalv1.Models;
using OVGPFinalv1.Models.Email_Models;

namespace OVGPFinalv1.Controllers
{
    public class ContactController : Controller
    {
        private EmailAddress FromAndToEmailAddress;
        private IEmailService EmailService;
        private readonly UserManager<Models.User> _userManager;
        public ContactController(EmailAddress _fromAddress, IEmailService _emailService, UserManager<Models.User> userManager)
        {
            _userManager = userManager;
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
                    Content = $"{model.Message}",
                    Subject = $"Contact formulier van {model.Name}, {model.Email} met het onderwerp: {model.Subject}"
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