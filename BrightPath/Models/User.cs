using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrightPath.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Login Is Required")]
        [DisplayName("User Name")]
        [Column(TypeName = "nvarchar(150)")]
        public string UserName { get; set; }
        [DisplayName("User LastName")]
        [Column(TypeName = "nvarchar(150)")]
        public string UserLastName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        [DisplayName("User Email")]
        public string UserEmail { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("User Pw")]
        public string UserPw { get; set; }
    }
}
