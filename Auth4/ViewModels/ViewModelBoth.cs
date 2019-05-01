using BrightPathDev.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrightPathDev.ViewModels
{
    public class ViewModelBoth
    {
        public Article Article { get; set; }
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();


    }
}