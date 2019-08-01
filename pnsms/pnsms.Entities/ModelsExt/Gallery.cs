using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{

    public partial class Gallery : Entity
    {
       public List<Image> Images ;
       public Image Image;
       public List<KeyValuePair<int, string>> EventList { get; set; }
    }
}
