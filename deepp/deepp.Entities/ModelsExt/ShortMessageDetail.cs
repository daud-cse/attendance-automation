using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{

    public partial class ShortMessageDetail : Entity
    {

        [ScaffoldColumn(false)] 
        public string UserInfoName = "";
       
    }
}
