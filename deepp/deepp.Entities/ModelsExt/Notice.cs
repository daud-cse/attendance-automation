using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.Models
{
    
    public partial class Notice : Entity
    {
        public IEnumerable<Image> ImageList;
        public IEnumerable<int> ExtImageIdList;
        public List<KeyValuePair<int, string>> NoticeTypeList { get; set; }
    }
}
