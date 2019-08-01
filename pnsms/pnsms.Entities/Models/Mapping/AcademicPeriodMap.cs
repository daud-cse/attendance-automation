using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class AcademicPeriodMap : EntityTypeConfiguration<AcademicPeriod>
    {
        public AcademicPeriodMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Description)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("AcademicPeriod");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.AcademicPeriods)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
