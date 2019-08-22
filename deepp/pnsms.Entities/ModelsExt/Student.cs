using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;
using System.Web;

namespace pnsms.Entities.Models
{
    [MetadataType(typeof(StudentMetadata))]
    public partial class Student : Entity
    {
        HttpPostedFile PostedFile { get; set; }//for photo
        public List<KeyValuePair<int, string>> AcademicBranchList { get; set; }
        public List<KeyValuePair<int, string>> AcademicClassList { get; set; }
        public List<KeyValuePair<int, string>> AcademicGroupList { get; set; }
        public List<KeyValuePair<int, string>> AcademicSectionList { get; set; }
        public List<KeyValuePair<int, string>> AcademicSessionList { get; set; }
        public List<KeyValuePair<int, string>> AcademicVerssionList { get; set; }
        public List<KeyValuePair<int, string>> AcademicShiftList { get; set; }
        public List<KeyValuePair<int, string>> CoCurricularActivityList { get; set; }
        public List<KeyValuePair<int, string>> ScholarshipList { get; set; }
        public List<int> CoCurricularActivities { get; set; }
        public List<int> Scholarships { get; set; }
    }
}
