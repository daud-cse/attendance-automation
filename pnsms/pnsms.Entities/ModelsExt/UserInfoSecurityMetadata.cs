using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
    public partial class UserInfoSecurityMetadata
    {
        [Required(ErrorMessage = "required.")]
        public string PasswordHash { get; set; }
        //[NotMapped]
        //[Required(ErrorMessage = "required.")]
        //public string NewPassword { get; set; }
        //[Required(ErrorMessage = "required.")]
        //[NotMapped]
        //public string ConfirmPassword { get; set; }



    }
}
