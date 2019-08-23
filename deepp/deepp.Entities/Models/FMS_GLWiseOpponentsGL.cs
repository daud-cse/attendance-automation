using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class FMS_GLWiseOpponentsGL: Entity
    {
        public int ID { get; set; }
        public Nullable<int> GLAccountId { get; set; }
        public Nullable<int> OpponentsGLId { get; set; }
        public int InstituteId { get; set; }
    }
}
