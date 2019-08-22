using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    [MetadataType(typeof(FeesGenerateHeadMetadata))]
    public partial class FeesGenerateHead : Entity
    {
        public string HeadName;
    }
}
