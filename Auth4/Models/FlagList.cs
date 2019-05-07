using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrightPathDev.Models
{
    public class FlagList
    {   
        [Key]
        [DisplayName("Flag List Id")]
        public int FListId { get; set; }
        public int ArticleId { get; set; }
        public int CommentId { get; set; }
        public int FlaggerId { get; set; }
    }
}
