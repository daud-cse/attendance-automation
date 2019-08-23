using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace deepp.Entities.Models
{
    public partial class Event:Entity
    {
        public List<Image> Images;
        public IEnumerable<Image> ImageList;
        public Image Image;
        public IEnumerable<int> ExtImageIdList;
    }
}
