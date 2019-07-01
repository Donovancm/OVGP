using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class EditModel : BasePageModel
    {
        private readonly UserManager<Models.User> _userManager;
        public EditModel(ApplicationDbContext context,
            UserManager<Models.User> userManager) : base(context, userManager)
        {
            _userManager = userManager;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Bedrijfsnaam")]
            public string Bedrijf { get; set; }
            [Required]
            [PersonalData]
            [Display(Name = "KvK Nummer")]
            public int KvKnummer { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Contact persoon")]
            public string ContactPersoon { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Adres")]
            public string Adres { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Postcode")]
            public string PostCode { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Plaats")]
            public string Plaats { get; set; }
            [Required]
            public bool Nieuwsbrief { get; set; }

            [Phone]
            [Display(Name = "Telefoonnummer")]
            public string PhoneNumber { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [PersonalData]
            [UIHint("IsActive")]
            [Display(Name = "Contributie betaald")]
            public bool ContributieBetaald { get; set; }
            [PersonalData]
            [Display(Name = "Betaaldatum")]
            public DateTime Betaaldatum { get; set; }
            [PersonalData]
            [Display(Name = "Vorige betaaldatum")]
            public DateTime VorigeBetaalDatum { get; set; }
            [PersonalData]
            [Display(Name = "Betaald bedrag")]
            public decimal BetaalBedrag { get; set; }
            [PersonalData]
            [Display(Name = "Bedrag te voldoen")]
            public decimal BedragTeVoldoen { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await Context.User.FirstOrDefaultAsync(
                                                 m => m.Id == id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Bedrijf = user.Bedrijf,
                KvKnummer = user.KvKnummer,
                ContactPersoon = user.ContactPersoon,
                Adres = user.Adres,
                PostCode = user.Postcode,
                Plaats = user.Plaats,
                Nieuwsbrief = user.Nieuwsbrief,
                ContributieBetaald = user.ContributieBetaald,
                Betaaldatum = user.Betaaldatum,
                VorigeBetaalDatum = user.VorigeBetaalDatum,
                BetaalBedrag = user.BetaalBedrag,
                BedragTeVoldoen = user.BedragTeVoldoen,
                Email = email,
                PhoneNumber = phoneNumber
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await Context.User.FirstOrDefaultAsync(
                                                 m => m.Id == id);
            if (Input.Bedrijf != user.Bedrijf)
            {
                user.Bedrijf = Input.Bedrijf;
            }
            if (Input.KvKnummer != user.KvKnummer)
            {
                user.KvKnummer = Input.KvKnummer;
            }
            if (Input.ContactPersoon != user.ContactPersoon)
            {
                user.ContactPersoon = Input.ContactPersoon;
            }
            if (Input.Adres != user.Adres)
            {
                user.Adres = Input.Adres;
            }
            if (Input.PostCode != user.Postcode)
            {
                user.Postcode = Input.PostCode;
            }
            if (Input.Plaats != user.Plaats)
            {
                user.Plaats = Input.Plaats;
            }
            if (Input.Nieuwsbrief != user.Nieuwsbrief)
            {
                user.Nieuwsbrief = Input.Nieuwsbrief;
            }
            if (Input.ContributieBetaald != user.ContributieBetaald)
            {
                user.ContributieBetaald = Input.ContributieBetaald;
            }
            if (Input.Betaaldatum != user.Betaaldatum)
            {
                user.Betaaldatum = Input.Betaaldatum;
            }
            if (Input.VorigeBetaalDatum != user.VorigeBetaalDatum)
            {
                user.VorigeBetaalDatum = Input.VorigeBetaalDatum;
            }
            if (Input.BetaalBedrag != user.BetaalBedrag)
            {
                user.BetaalBedrag = Input.BetaalBedrag;
            }
            if (Input.BedragTeVoldoen != user.BedragTeVoldoen)
            {
                user.BedragTeVoldoen = Input.BedragTeVoldoen;
            }
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }
            await _userManager.UpdateAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage("./Index");
        }
        //[BindProperty]
        //public User User { get; set; }

        //public async Task<IActionResult> OnGetAsync(string id)
        //{
        //    User = await Context.User.FirstOrDefaultAsync(
        //                                         m => m.Id == id);

        //    if (User == null)
        //    {
        //        return NotFound();
        //    }
        //    return Page();
        //}

        //public async Task<IActionResult> OnPostAsync(string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    var user = await Context.User.FirstOrDefaultAsync(m => m.Id == id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    User.Id = user.Id;

        //    Context.Attach(User).State = EntityState.Modified;

        //    Context.User.Update(User);
        //    await Context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}

        //private bool UserExists(string id)
        //{
        //    return Context.User.Any(e => e.Id == id);
        //}
    }
}