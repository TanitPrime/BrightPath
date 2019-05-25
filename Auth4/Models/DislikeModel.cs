using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrightPathDev.Models
{
    public class DislikeModel
    {
        [Key]
        public int DislikeId { get; set; }
        public int ArticleId { get; set; }
        public string UserName { get; set; }
    }
}
    