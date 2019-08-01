using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
    public partial class AdmissionFormMetadata
    {
        [Required(ErrorMessage = "required.")]
        [MaxLength(128)]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "required.")]
        //[MaxLength(128)]
        //public string MiddleName { get; set; }
        [Required(ErrorMessage = "required.")]
        [MaxLength(128)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DOB { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        //[Required(ErrorMessage = "required.")]
        //public Nullable<int> GenderId { get; set; }
        //[Required(ErrorMessage = "required.")]
        //public Nullable<int> NationalityId { get; set; }
        //[Required(ErrorMessage = "required.")]
        //public Nullable<int> ReligionId { get; set; }
        //[Required(ErrorMessage = "required.")]
        //public Nullable<int> BloodGroupId { get; set; }
        //[Required(ErrorMessage = "required.")]
        //public Nullable<int> AcademicSessionId { get; set; }
        //[Required(ErrorMessage = "required.")]
        //public Nullable<int> AcademicClassId { get; set; }
        //[Required(ErrorMessage = "required.")]
        //public Nullable<int> AcademicBranchId { get; set; }
        [MaxLength(13)]
        [Required(ErrorMessage = "required.")]
        public string ContactNumber { get; set; }

    }
}
