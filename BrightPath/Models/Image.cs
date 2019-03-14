using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BrightPath.Models
{
    public class Image : IdentityUser
    {
        public int ImageId { get; set; }

        public string Title { get; set; }

        //public List<IFormFile> Files { get; set; } = new List<IFormFile>();

        [DisplayName("Upload Image")]
        public string ImagePath { get; set; }
    }
}