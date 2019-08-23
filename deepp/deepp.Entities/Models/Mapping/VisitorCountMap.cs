using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class VisitorCountMap : EntityTypeConfiguration<VisitorCount>
    {
        public VisitorCountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("VisitorCounts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.VisitorCount1).HasColumnName("VisitorCount");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.VisitorCounts)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
