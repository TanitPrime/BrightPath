using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrightPath.ViewModels
{
    public class AddImageViewModel
    {
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();

        [Required]
        public string Title { get; set; }
    }
}