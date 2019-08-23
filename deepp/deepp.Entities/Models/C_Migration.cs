using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class C_Migration: Entity
    {
        public string LastUpdate { get; set; }
    }
}
