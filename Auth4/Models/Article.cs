using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BrightPathDev.Models
{
    public class Article
    {



        [Key]
        [DisplayName("Article Id")]
        public int ArticleId { get; set; }
        [Required]
        [DisplayName("Article Title")]
        [Column(TypeName = "nvarchar(150)")]
        public string ArticleTitle { get; set; }
        [DisplayName("Short Descreption")]
        [Column(TypeName = "nvarchar(250)")]
        public string desc_mini { get; set; }
        [DisplayName("Full Descreption")]
        [Column(TypeName = "nvarchar(750)")]
        public string desc { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Article Adress")]
        public string ArticleAdress { get; set; }
        [DisplayName("Article Map Latitude")]
        [Column(TypeName = "nvarchar(150)")]
        public float ArticleLat { get; set; }
        [DisplayName("Article Map Langitiude")]
        [Column(TypeName = "nvarchar(150)")]
        public float ArticleLng{ get; set; }
        [DisplayName("Article Contact")]
        [Column(TypeName = "nvarchar(100)")]
        public string ArticleContact { get; set; }
        // [DisplayName("Article_Date")]
        //[Required]
        //public DateTime ArtcicleDate { get; set; }

        [DisplayName("Image Path")]
        public string ImagePath { get; set; }

        [DisplayName("Image Name")]
        public string ImageName { get; set; }
       
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int FlagCount { get; set; }

        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        public ContactStatus Status { get; set; }
        public string Category { get; set; }
    }

    
    public enum ContactStatus
    {
        Pending,
        Approved,
        Edited
    }
}

