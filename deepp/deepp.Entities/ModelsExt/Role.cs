using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace deepp.Entities.Models
{
    public partial class Role : Entity
    {
        [ScaffoldColumn(false)]
        public List<Right> RightsList = null;

    }
}
