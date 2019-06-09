using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OVGPFinalv1.Data;
using OVGPFinalv1.Models;
using OVGPFinalv1.Models.Email_Models;

namespace OVGPFinalv1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private EmailAddress FromAndToEmailAddress;
        private IEmailService EmailService;

        public HomeController(ApplicationDbContext context, EmailAddress _fromAddress, IEmailService _emailService)
        {
            FromAndToEmailAddress = _fromAddress;
            EmailService = _emailService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Content.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public ViewResult Contact()
        {
            return View();
        }
        public IActionResult ContactResult()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                EmailMessage msgToSend = new EmailMessage
                {
                    FromAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                    ToAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                    Content = $"Here is your message: Name: {model.Name}, " +
                        $"Email: {model.Email}, Message: {model.Message}",
                    Subject = "Contact Form - BasicContactForm App"
                };

                EmailService.Send(msgToSend);
                return RedirectToAction("ContactResult");
            }
            else
            {
                return Contact();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
