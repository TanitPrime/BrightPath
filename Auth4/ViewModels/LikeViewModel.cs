using BrightPathDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrightPathDev.ViewModels
{
    public class LikeViewModel
    {
        public Article Article { get; set; }
        public List<LikeModel> LikeModels { get; set; } = new List<LikeModel>();
        public List<DislikeModel> DislikeModels { get; set; } = new List<DislikeModel>();
    }
}
