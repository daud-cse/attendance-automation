using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{

    public partial class ShortMessageDetail : Entity
    {

        [ScaffoldColumn(false)] 
        public string UserInfoName = "";
       
    }
}
