using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class ShortMessage: Entity
    {
        public ShortMessage()
        {
            this.ShortMessageDetails = new List<ShortMessageDetail>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public string SmsTemplate { get; set; }
        public bool IsStaticSms { get; set; }
        public System.DateTime SendAt { get; set; }
        public Nullable<int> TotalSmsCount { get; set; }
        public string Mask { get; set; }
        public bool IsSent { get; set; }
        public bool IsGenerated { get; set; }
        public Nullable<bool> IsChecked { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
        public Nullable<bool> IsFromWebPanel { get; set; }
        public string SmsPreview { get; set; }
        public Nullable<bool> IsPayByReceipient { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<ShortMessageDetail> ShortMessageDetails { get; set; }
    }
}
