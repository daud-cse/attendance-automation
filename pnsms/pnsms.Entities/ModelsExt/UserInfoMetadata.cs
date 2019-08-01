using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace pnsms.Entities.Models
{
    public class UserInfoMetadata
    {
        //[JsonIgnore]
        //public virtual ICollection<Student> Students { get; set; }

        public string Name { get; set; }
         [Display(Name = "Contact Number")]
        public string ContactNumber1 { get; set; }
         [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
    }
}
