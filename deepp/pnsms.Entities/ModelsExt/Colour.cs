using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
     [MetadataType(typeof(ColourMetadata))]
   public partial class Colour:Entity
    {
    }
}
