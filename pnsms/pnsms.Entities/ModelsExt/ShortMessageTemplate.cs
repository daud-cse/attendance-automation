using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    
    public partial class ShortMessageTemplate: Entity
    {

        [ScaffoldColumn(false)] 
        public List<NotificationTag> NotificationTags = null;
        public List<KeyValuePair<int, string>> NotificationTagGroupList { get; set; }
    }
}
