﻿using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.Models
{
    public partial class FMS_RelatedAccount : Entity
    {
        public List<KeyValuePair<int, string>> kvpFMS_RelatedAccountType { get; set; }
    }
}