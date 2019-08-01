using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    [MetadataType(typeof(StudentAttendanceDetailMetadata))]
    
    public partial class StudentAttendanceDetail : Entity
    {
        public string StudentName;
        public string StudentRoll;
        public string Status;
        public string StatusColor;
        public IEnumerable<AttendanceTypeModel> AttendanceTypes { get; set; }

       
    }
    public class AttendanceTypeModel
    {
        public int AttTypeId { get; set; }
        public bool IsDefaultAsSelected { get; set; }
        public bool IsPresent { get; set; }
        public string Flag { get; set; }
        public string colorCode { get; set; }
    }

}
