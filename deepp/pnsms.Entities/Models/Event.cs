using System;
using System.Collections.Generic;
using pnsms.Entities.Models;
using Repository.Pattern.Ef6;

namespace pnsms.Entities.Models
{
    public partial class Event: Entity
    {
        public Event()
        {
            this.Galleries = new List<Gallery>();
        }

        public int Id { get; set; }
        public int InstituteId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> EventStartAt { get; set; }
        public Nullable<System.DateTime> EventEndAt { get; set; }
        public string EventTitle { get; set; }
        public string EventBody { get; set; }
        public string EventBriefInfo { get; set; }
        public string EventLocation { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string WebAddress { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }
    }
}
