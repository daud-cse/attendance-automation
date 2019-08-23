using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(UserInfoSecurityMetadata))]
    public partial class UserInfoSecurity
    {
        public string NewPassword;
        public string ConfirmPassword;
    }
}
