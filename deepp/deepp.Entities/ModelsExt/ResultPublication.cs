using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;
using System.ComponentModel.DataAnnotations;
using deepp.Entities.ModelsExt;

namespace deepp.Entities.Models
{
    [MetadataType(typeof(ResultPublicationMetadata))]
    public partial class ResultPublication : Entity
    {
        public List<KeyValuePair<int, string>> AcademicSessionList { get; set; }
        public IEnumerable<Image> ImageList;
        public IEnumerable<int> ExtImageIdList;
    }
}
