using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrightPath.Models
{
    public class Admin
    {

        [Key]
        public int AdminId { get; set; }
        public bool IsRoot { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        [Required(ErrorMessage = "Login Is Required")]
        [DisplayName("Admin Name")]
        public string UserName { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        [DisplayName("Admin Pw")]
        public string AdminPw { get; set; }
    }
}
