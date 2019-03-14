using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BrightPath.Models
{
    public class Image : IdentityUser
    {
        public int ImageId { get; set; }

        public string Title { get; set; }

        [DisplayName("Upload Image")]
        public string ImagePath { get; set; }

        public byte[] ImageFile { get; set; }
    }
}
