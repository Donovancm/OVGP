using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models
{
    public class Chat
    {
        public Chat()
        {
            Posted = DateTime.Now;
            //this.NamePostedUser = "Name of the logged in user"
        }
        public int ChatId { get; set; }
        [Display(Name = "Name")]
        public string PersonName { get; set; }
        public string Text { get; set; }
        [Display(Name = "Posted by")]
        public DateTime Posted { get; set; }

        //public List<User> User {get; set;}
    }
}
