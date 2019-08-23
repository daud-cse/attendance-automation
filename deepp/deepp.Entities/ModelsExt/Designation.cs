using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(DesignationMetadata))]
    public partial class Designation : Entity
    {
        public int TotalStuff;
    }
}
