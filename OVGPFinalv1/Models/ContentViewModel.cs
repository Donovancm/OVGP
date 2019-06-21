using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models
{
    public class ContentViewModel
    {
        public ContentViewModel()
        {
            PostedDate = DateTime.Now;
            //this.NamePostedUser = "Name of the logged in user"
        }

        public int ContentId { get; set; }
        [Required]
        [Display(Name = "Titel")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Beschrijving")]
        public string Text { get; set; }
        [Display(Name = "Gepost op")]
        public DateTime PostedDate { get; set; }
        [Display(Name = "Gepost Door")]
        public string NamePostedUser { get; set; }

        // IMG / VID
        [Display(Name = "Content Type")]
        [StringLength(3)]
        public string ContentType { get; set; }
        //Depends what file type it is, example PDF/URL/Cursus
        [Display(Name = "Video of afbeelding url")]
        public string ContentURL { get; set; }
        public IFormFile ContentFile { get; set; }
        [Display(Name = "Comments toegestaan op dit nieuwsbericht")]
        public bool CommentsAllowed { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
