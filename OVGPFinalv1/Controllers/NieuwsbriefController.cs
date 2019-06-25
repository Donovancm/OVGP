using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OVGPFinalv1.Data;
using OVGPFinalv1.Models;
using OVGPFinalv1.Models.Email_Models;

namespace OVGPFinalv1.Controllers
{
    [Authorize(Roles = "Beheerder")]
    public class NieuwsbriefController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;
        private EmailAddress FromAndToEmailAddress;
        private IEmailService EmailService;

        public NieuwsbriefController(ApplicationDbContext context, EmailAddress _fromAddress, IEmailService _emailService, UserManager<Models.User> userManager)
        {
            _userManager = userManager;
            _context = context;
            FromAndToEmailAddress = _fromAddress;
            EmailService = _emailService;
        }

        // GET: Nieuwsbriefs
        public async Task<IActionResult> Index()
        {
            return View(await _context.NieuwsBrief.ToListAsync());
        }

        // GET: Nieuwsbriefs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuwsbrief = await _context.NieuwsBrief
                .FirstOrDefaultAsync(m => m.NieuwsbriefId == id);
            if (nieuwsbrief == null)
            {
                return NotFound();
            }

            return View(nieuwsbrief);
        }

        // GET: Nieuwsbriefs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nieuwsbriefs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NieuwsbriefId,Title,Onderwerp,Text,PostedDate")] Nieuwsbrief nieuwsbrief, EmailAddress emailAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nieuwsbrief);
                var users = _userManager.Users;
                foreach (var item in users)
                {
                    EmailAddress adres = new EmailAddress()
                    {
                        Address = item.Email
                    };
                    if (item.Nieuwsbrief == true)
                    {
                        EmailMessage msgToSend = new EmailMessage
                        {
                            FromAddresses = new List<EmailAddress> { FromAndToEmailAddress },
                            ToAddresses = new List<EmailAddress> { adres },
                            //Hier nog vormgeven email
                            Content = $"{nieuwsbrief.Text}",
                            Subject = $"{nieuwsbrief.Title}"
                        };
                        EmailService.Send(msgToSend);
                    }
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(nieuwsbrief);
        }

        // GET: Nieuwsbriefs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuwsbrief = await _context.NieuwsBrief.FindAsync(id);
            if (nieuwsbrief == null)
            {
                return NotFound();
            }
            return View(nieuwsbrief);
        }

        // POST: Nieuwsbriefs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NieuwsbriefId,Title,Onderwerp,Text,PostedDate")] Nieuwsbrief nieuwsbrief)
        {
            if (id != nieuwsbrief.NieuwsbriefId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nieuwsbrief);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NieuwsbriefExists(nieuwsbrief.NieuwsbriefId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nieuwsbrief);
        }

        // GET: Nieuwsbriefs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nieuwsbrief = await _context.NieuwsBrief
                .FirstOrDefaultAsync(m => m.NieuwsbriefId == id);
            if (nieuwsbrief == null)
            {
                return NotFound();
            }

            return View(nieuwsbrief);
        }

        // POST: Nieuwsbriefs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nieuwsbrief = await _context.NieuwsBrief.FindAsync(id);
            _context.NieuwsBrief.Remove(nieuwsbrief);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NieuwsbriefExists(int id)
        {
            return _context.NieuwsBrief.Any(e => e.NieuwsbriefId == id);
        }
    }
}
