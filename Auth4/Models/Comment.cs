using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrightPathDev.Models
{
    public class Comment
    {   
        [Key]
        public int CommentId { get; set; }
        [Required]
        public string CommentText { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }

        public int ArticleId { get; set; }

        public int FlagCount { get; set; }

    }
}
