using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class DynamicPageBody: Entity
    {
        public int DynamicPageId { get; set; }
        public string HtmlText { get; set; }
        public virtual DynamicPage DynamicPage { get; set; }
    }
}
