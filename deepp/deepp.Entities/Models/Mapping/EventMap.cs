using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class EventMap : EntityTypeConfiguration<Event>
    {
        public EventMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.EventTitle)
                .HasMaxLength(512);

            this.Property(t => t.EventLocation)
                .HasMaxLength(128);

            this.Property(t => t.ContactPerson)
                .HasMaxLength(128);

            this.Property(t => t.ContactNumber)
                .HasMaxLength(128);

            this.Property(t => t.ContactEmail)
                .HasMaxLength(128);

            this.Property(t => t.WebAddress)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("Events");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.EventStartAt).HasColumnName("EventStartAt");
            this.Property(t => t.EventEndAt).HasColumnName("EventEndAt");
            this.Property(t => t.EventTitle).HasColumnName("EventTitle");
            this.Property(t => t.EventBody).HasColumnName("EventBody");
            this.Property(t => t.EventBriefInfo).HasColumnName("EventBriefInfo");
            this.Property(t => t.EventLocation).HasColumnName("EventLocation");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.ContactPerson).HasColumnName("ContactPerson");
            this.Property(t => t.ContactNumber).HasColumnName("ContactNumber");
            this.Property(t => t.ContactEmail).HasColumnName("ContactEmail");
            this.Property(t => t.WebAddress).HasColumnName("WebAddress");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.Events)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
