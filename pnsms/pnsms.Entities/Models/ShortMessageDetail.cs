using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class ShortMessageDetail: Entity
    {
        public int Id { get; set; }
        public int ShortMessageId { get; set; }
        public string SmsText { get; set; }
        public string MobileNumber { get; set; }
        public Nullable<int> UserInfoId { get; set; }
        public Nullable<int> SmsCount { get; set; }
        public bool IsSent { get; set; }
        public Nullable<int> ShortMessageStatusId { get; set; }
        public string GatewayIdentificationNo { get; set; }
        public Nullable<bool> IsStatusUpdated { get; set; }
        public Nullable<System.DateTime> StatusUpdatedAt { get; set; }
        public string Comments { get; set; }
        public Nullable<int> StudentId { get; set; }
        public virtual ShortMessage ShortMessage { get; set; }
        public virtual ShortMessageStatus ShortMessageStatus { get; set; }
        public virtual Student Student { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
