using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OVGPFinalv1.Data;
using OVGPFinalv1.Models;
using OVGPFinalv1.Models.Email_Models;

namespace OVGPFinalv1.Controllers
{
    [Authorize]
    public class ContentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly UserManager<Models.User> _userManager;
        private EmailAddress FromAndToEmailAddress;
        private IEmailService EmailService;

        public ContentsController(ApplicationDbContext context, IHostingEnvironment hostingEnv, EmailAddress _fromAddress, IEmailService _emailService, UserManager<Models.User> userManager)
        {
            _userManager = userManager;
            _hostingEnv = hostingEnv;
            _context = context;
            FromAndToEmailAddress = _fromAddress;
            EmailService = _emailService;
        }

        // GET: Contents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Content.OrderBy(x => x.PostedDate).ToListAsync());
        }

        // GET: Contents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Content
                .FirstOrDefaultAsync(m => m.ContentId == id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // GET: Contents/Create
        [Authorize(Roles = "Beheerder")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Beheerder")]
        public IActionResult CreateOptions()
        {
            return View();
        }
        [Authorize(Roles = "Beheerder")]
        public async Task<IActionResult> CreateWithURL([Bind("ContentId,Title,Text,PostedDate,NamePostedUser,ContentType,ContentURL,ContentFile,CommentsAllowed")] Content content)
        {
            if (ModelState.IsValid)
            {
                _context.Add(content);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(content);
        }
        // POST: Contents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Beheerder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ContentId,Title,Text,PostedDate,NamePostedUser,ContentType,ContentURL,ContentFile,CommentsAllowed")] Content content, ContentViewModel contentViewModel)
        {
            if (ModelState.IsValid)
            {
                if (contentViewModel.ContentFile != null)
                {
                    //upload files to wwwroot
                    var fileName = Path.GetFileName(contentViewModel.ContentFile.FileName);
                    var filePath = Path.Combine(_hostingEnv.WebRootPath, "stream", fileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await contentViewModel.ContentFile.CopyToAsync(fileSteam);
                    }
                    //your logic to save filePath to database, for example
                    filePath = Path.Combine("stream", contentViewModel.ContentFile.FileName);
                    Content contents = new Content();

                    contents.Title = contentViewModel.Title;
                    contents.Text = contentViewModel.Text;
                    contents.PostedDate = contentViewModel.PostedDate;
                    contents.NamePostedUser = contentViewModel.NamePostedUser;
                    contents.ContentType = contentViewModel.ContentType;
                    contents.ContentURL = contentViewModel.ContentURL;
                    content.ContentFile = contents.ContentFile;

                    //contents.ContentFile = filePath.Remove(0, 55);
                    contents.ContentFile = filePath;
                    _context.Content.Add(contents);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(content);

        }
        //public async Task<IActionResult> CreateNieuwsbrief(
        //    [Bind("ContentId,Title,Text,PostedDate,NamePostedUser,ContentType,ContentURL,ContentFile,CommentsAllowed")] Content content, EmailAddress emailAddress)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(content);
        //        var users = _userManager.Users;
        //        foreach (var item in users)
        //        {
        //            EmailAddress adres = new EmailAddress()
        //            {
        //                Address = item.Email
        //            };
        //            if (item.Nieuwsbrief == true)
        //            {
        //                EmailMessage msgToSend = new EmailMessage
        //                {
        //                    FromAddresses = new List<EmailAddress> { FromAndToEmailAddress },
        //                    ToAddresses = new List<EmailAddress> { adres },
        //                    //Hier nog vormgeven email
        //                    Content = $"Here is your message: Name: {item.UserName}, " +
        //                    $"Email: {item.Email}, Message: {content.Text}",
        //                    Subject = $"Nieuwsbrief, {content.Title}"
        //                };
        //                EmailService.Send(msgToSend);
        //            }
        //        }
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }
            
        //    return View(content);
        //}

        // GET: Contents/Edit/5
        [Authorize(Roles = "Beheerder")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Content.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,Title,Text,PostedDate,NamePostedUser,ContentType,ContentURL,ContentFile")] Content content)
        {
            if (id != content.ContentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(content);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentExists(content.ContentId))
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
            return View(content);
        }

        // GET: Contents/Delete/5
        [Authorize(Roles = "Beheerder")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Content
                .FirstOrDefaultAsync(m => m.ContentId == id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var content = await _context.Content.FindAsync(id);
            _context.Content.Remove(content);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentExists(int id)
        {
            return _context.Content.Any(e => e.ContentId == id);
        }
    }
}
