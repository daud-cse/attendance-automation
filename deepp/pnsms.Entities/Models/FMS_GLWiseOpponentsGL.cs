using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class FMS_GLWiseOpponentsGL: Entity
    {
        public int ID { get; set; }
        public Nullable<int> GLAccountId { get; set; }
        public Nullable<int> OpponentsGLId { get; set; }
        public int InstituteId { get; set; }
    }
}
