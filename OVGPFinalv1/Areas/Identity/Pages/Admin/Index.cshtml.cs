using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OVGPFinalv1.Data;
using OVGPFinalv1.Models;

namespace OVGPFinalv1.Areas.Identity.Pages.Admin
{
    [Authorize(Roles = "Beheerder")]
    public class IndexModel : BasePageModel
    {
        public IndexModel(ApplicationDbContext context, UserManager<User> userManager) : base(context, userManager)
        {
        }
        public IList<User> User { get; set; }

        public async Task OnGetAsync()
        {
            var users = UserManager.Users;
            User = await users.ToListAsync();
        }
    }
}