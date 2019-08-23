using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class NotificationTag: Entity
    {
        [ScaffoldColumn(false)]
        public string NotificationGroupName = "";
       
    }
}
