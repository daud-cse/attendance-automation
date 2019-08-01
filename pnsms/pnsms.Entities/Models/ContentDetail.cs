using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class ContentDetail: Entity
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int TagId { get; set; }
        public virtual Content Content { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
