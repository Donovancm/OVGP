using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OVGPFinalv1.Data;

namespace OVGPFinalv1.Areas.Identity.Pages.Admin
{
    public class BasePageModel : PageModel
    {
        protected ApplicationDbContext Context { get; }
        protected UserManager<Models.User> UserManager { get; }

        public BasePageModel(
            ApplicationDbContext context,
            UserManager<Models.User> userManager) : base()
        {
            Context = context;
            UserManager = userManager;
        }
    }
}