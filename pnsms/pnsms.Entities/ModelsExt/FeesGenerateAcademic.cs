using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    [MetadataType(typeof(FeesGenerateAcademicMetadata))]
    public partial class FeesGenerateAcademic : Entity
    {
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public List<KeyValuePair<int, string>> AcademicClassList { get; set; }
        public List<KeyValuePair<int, string>> AcademicGroupList { get; set; }
        public List<KeyValuePair<int, string>> AcademicVerssionList { get; set; }
        public int InstituteId;
    }
}
