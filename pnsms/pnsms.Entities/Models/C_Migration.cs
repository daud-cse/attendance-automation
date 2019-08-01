using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class C_Migration: Entity
    {
        public string LastUpdate { get; set; }
    }
}
