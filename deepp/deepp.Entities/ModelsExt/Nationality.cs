using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(NationalityMetadata))]
    public partial class Nationality : Entity
    {
        
    }
}
