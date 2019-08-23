using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Image: Entity
    {
        public int Id { get; set; }
        public int RefTypeId { get; set; }
        public int RefPrimaryKey { get; set; }
        public byte[] ImageBinaryData { get; set; }
        public string ImageCaption { get; set; }
        public string ImageUrl { get; set; }
        public string FileExt { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdatedTime { get; set; }
        public string ImageDescription { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public virtual ImageType ImageType { get; set; }
    }
}
