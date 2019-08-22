using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class ImageType: Entity
    {
        public ImageType()
        {
            this.Images = new List<Image>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
