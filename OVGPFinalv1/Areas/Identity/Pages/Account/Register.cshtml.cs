using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace OVGPFinalv1.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Beheerder")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Models.User> _signInManager;
        private readonly UserManager<Models.User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<Models.User> userManager,
            SignInManager<Models.User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Bedrijfnaam is vereist")]
            [DataType(DataType.Text)]
            [Display(Name = "Full name")]
            public string Bedrijf { get; set; }
            [Required(ErrorMessage = "KvK Nummer vereist")]
            [PersonalData]
            [Display(Name = "KvK Nummer")]
            public int KvKnummer { get; set; }
            [Required(ErrorMessage = "Naam contact persoon vereist")]
            [DataType(DataType.Text)]
            [Display(Name = "Contact persoon")]
            public string ContactPersoon { get; set; }
            [Required(ErrorMessage = "Adres nodig")]
            [DataType(DataType.Text)]
            [Display(Name = "Adres")]
            public string Adres { get; set; }
            [Required(ErrorMessage = "Postcode vereist")]
            [DataType(DataType.Text)]
            [Display(Name = "Postcode")]
            public string PostCode { get; set; }
            [Required(ErrorMessage = "Plaats vereist")]
            [DataType(DataType.Text)]
            [Display(Name = "Plaats")]
            public string Plaats { get; set; }

            [Display(Name = "Inschrijven voor nieuwsbrief")]
            public bool Nieuwsbrief { get; set; }

            [Required(ErrorMessage = "Geef valide email aan")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0} moet tenminsten {2} en maximaal {1} karakters lang zijn. ", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bevestig wachtwoord")]
            [Compare("Password", ErrorMessage = "Komt niet overeen met de wachtwoord")]
            public string ConfirmPassword { get; set; }
            [Phone]
            [Display(Name = "Telefoonnummer")]
            public string PhoneNumber { get; set; }

            [UIHint("IsActive")]
            [Display(Name = "Contributie betaald")]
            public bool ContributieBetaald { get; set; }
            [PersonalData]
            [Display(Name = "Betaaldatum")]
            [DataType(DataType.Date)]
            public DateTime Betaaldatum { get; set; }
            [PersonalData]
            [Display(Name = "Vorige betaaldatum")]
            [DataType(DataType.Date)]
            public DateTime VorigeBetaalDatum { get; set; }
            [PersonalData]
            [Display(Name = "Betaald bedrag")]
            [DataType(DataType.Currency)]
            public decimal BetaalBedrag { get; set; }
            [PersonalData]
            [Display(Name = "Bedrag te voldoen")]
            [DataType(DataType.Currency)]
            public decimal BedragTeVoldoen { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new Models.User {
                    Bedrijf = Input.Bedrijf,
                    KvKnummer = Input.KvKnummer,
                    ContactPersoon = Input.ContactPersoon,
                    Adres = Input.Adres,
                    Postcode = Input.PostCode,
                    Plaats = Input.Plaats,
                    UserName = Input.Email,
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                    Nieuwsbrief = Input.Nieuwsbrief,
                    ContributieBetaald = Input.ContributieBetaald,
                    Betaaldatum = Input.Betaaldatum,
                    VorigeBetaalDatum = Input.VorigeBetaalDatum,
                    BetaalBedrag = Input.BetaalBedrag,
                    BedragTeVoldoen = Input.BedragTeVoldoen,
                    //Voor nu true
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true
                    
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                     //   $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    //Prevent auto log in when registeren
                    ///
                    await _userManager.AddToRoleAsync(user, "Lid");
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("./Succesvol");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
