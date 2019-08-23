using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class DynamicPageBody: Entity
    {
        public int DynamicPageId { get; set; }
        public string HtmlText { get; set; }
        public virtual DynamicPage DynamicPage { get; set; }
    }
}
