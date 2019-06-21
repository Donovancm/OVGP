using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models
{
    public class Comment
    {
        // 1 content heeft meerdere comments
        public int CommentId { get; set; }
        public string Person { get; set; }
        public string CommentText { get; set; }

        public int? ContentId { get; set; }
        public Content Content { get; set; }
    }
}
