using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Gallery: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public Nullable<int> EventId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string GalleryTitle { get; set; }
        public string GallerySubtitle { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual Event Event { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
