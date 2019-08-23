using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(DistrictOrStateMetadata))]
    public partial class DistrictOrState : Entity
    {
        
    }
}
