using System;
using System.Collections.Generic;
using Repository.Pattern.Ef6;
using System.ComponentModel.DataAnnotations;

namespace pnsms.Entities.Models
{
    public partial class StudentMetadata 
    {
          [Display(Name = "Roll No")]
        public string CurrentRollNo { get; set; }

          [Display(Name = "Class")]
        public int CurrentAcademicClassId { get; set; }
    }
}
