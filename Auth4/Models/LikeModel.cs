using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrightPathDev.Models
{
    public class LikeModel
    {
        public int LikeId { get; set; }
        public int ArticleId { get; set; }
        public string UserId { get; set; }
    }
}
