using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OVGPFinalv1.Data;
using OVGPFinalv1.Models;

namespace OVGPFinalv1.Areas.Identity.Pages.Admin
{
    [Authorize(Roles = "Beheerder")]
    public class DeleteModel : BasePageModel
    {
        public DeleteModel(ApplicationDbContext context, UserManager<User> userManager) : base(context, userManager)
        {
        }
        [BindProperty]
        public User User { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            User = await Context.User.FirstOrDefaultAsync(
                                                 m => m.Id== id);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            User = await Context.User.FindAsync(id);

            var contact = await Context
                .User.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (contact == null)
            {
                return NotFound();
            }


            Context.User.Remove(User);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}