using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class Testimonial: Entity
    {
        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string TestimonialBody { get; set; }
        public string TestimonialBy { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
