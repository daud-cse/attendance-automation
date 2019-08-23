using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    //[MetadataType(typeof(SubjectAcademicClassMappingMetadata))]
    partial class SubjectAcademicClassMapping : Entity
    {
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public List<KeyValuePair<int, string>> AcademicClassList { get; set; }
        public List<KeyValuePair<int, string>> AcademicSessionList { get; set; }
        public List<KeyValuePair<int, string>> SubjectList { get; set; }
        public string SubjectName = "";

    }

    
}
