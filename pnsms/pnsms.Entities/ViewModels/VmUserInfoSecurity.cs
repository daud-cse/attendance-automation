using pnsms.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels
{
   public class VmUserInfoSecurity
    {
       public UserInfoSecurity userInfoSecurity { get; set; }
     
       [Required(ErrorMessage = "required.")]
       public string NewPassword { get; set; }
     
       [Required(ErrorMessage = "required.")]
       public string ConfirmPassword { get; set; }
    }
}
