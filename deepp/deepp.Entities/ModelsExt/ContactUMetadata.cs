using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.Models
{
    public class ContactUMetadata
    {
        [Required(ErrorMessage = "required.")]
        [MaxLength(500)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
