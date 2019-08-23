using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(InstituteMetadata))]
    public partial class Institute : Entity
    {
        [ScaffoldColumn(false)] 
        public List<Image> ImagesList = null;
        public List<KeyValuePair<int, string>> PackageList { get; set; }
        public List<KeyValuePair<int, string>> GlobalCountryList { get; set; }
        public List<KeyValuePair<int, string>> GlobalDivisionList { get; set; }
        public List<KeyValuePair<int, string>> GlobalDistrictList { get; set; }
        public List<KeyValuePair<int, string>> GlobalSubDistrictList { get; set; }
        public List<KeyValuePair<int, string>> GlobalInstituteTypeList { get; set; }

    }
}
