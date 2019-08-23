using deepp.Entities.ModelsExt;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(UserAttendanceDetailMetadata))]
    public partial class UserAttendanceDetail : Entity
    {
        public string UserName;
        public string UserPin;
        public string UserDesignation;
        public int? UserDesignationOrderBy;
        public string Status;
        public string StatusColor;
        public IEnumerable<UserAttendanceTypeModel> AttendanceTypes { get; set; }
    }
    public class UserAttendanceTypeModel
    {
        public int AttTypeId { get; set; }
        public bool IsDefaultAsSelected { get; set; }
        public bool IsPresent { get; set; }
        public string Flag { get; set; }
        public string colorCode { get; set; }
    }
}
