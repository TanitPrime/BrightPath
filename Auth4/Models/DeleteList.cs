using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BrightPathDev.Models
{
    public class DeleteList
    {
        [Key]
        [DisplayName("Delete Request List Id")]
        public int DListId { get; set; }
        public int ArticleId { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Reason { get; set; }
        public DateTime DateOfRequest { get; set; }

    }
}
