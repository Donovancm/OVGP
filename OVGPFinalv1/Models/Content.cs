using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models
{
    public class Content
    {
        public Content()
        {
            PostedDate = DateTime.Now;
            //this.NamePostedUser = "Name of the logged in user"
        }

        public int ContentId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Text { get; set; }
        [Display(Name = "Posted on")]
        public DateTime PostedDate { get; set; }
        [Display(Name = "Posted by")]
        public string NamePostedUser { get; set; }

        // IMG / VID
        [Display(Name = "Content Type")]
        [StringLength(3)]
        public string ContentType { get; set; }
        //Depends what file type it is, example PDF/URL/Cursus
        [Display(Name = "Video or Image link")]
        public string ContentURL { get; set; }
        public byte[] ContentFile { get; set; }
    }
}
