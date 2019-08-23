using System;
using System.Collections.Generic;
using deepp.Entities.Models;
using Repository.Pattern.Ef6;

namespace deepp.Entities.Models
{
    public partial class SSO: Entity
    {
        public int Id { get; set; }
        public string Tokenkey { get; set; }
        public int InstituteId { get; set; }
        public int AcademicSessionId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public Nullable<int> UserTypeId { get; set; }
        public string DeviceInfo { get; set; }
        public string IPAddress { get; set; }
        public string Country { get; set; }
        public string Subject { get; set; }
        public string ClientId { get; set; }
        public Nullable<System.DateTime> IssuedUtc { get; set; }
        public Nullable<System.DateTime> ExpiresUtc { get; set; }
        public string SessionId { get; set; }
        public Nullable<System.DateTime> LogDate { get; set; }
    }
}
