using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class FeesAcademicClassMap : EntityTypeConfiguration<FeesAcademicClass>
    {
        public FeesAcademicClassMap()
        {
            // Primary Key
            this.HasKey(t => t.FeesAcademicClassId);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("FeesAcademicClass");
            this.Property(t => t.FeesAcademicClassId).HasColumnName("FeesAcademicClassId");
            this.Property(t => t.AcademicClassId).HasColumnName("AcademicClassId");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.FeesHeadsId).HasColumnName("FeesHeadsId");
            this.Property(t => t.FeesTypeId).HasColumnName("FeesTypeId");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.AcademicClass)
                .WithMany(t => t.FeesAcademicClasses)
                .HasForeignKey(d => d.AcademicClassId);
            this.HasRequired(t => t.FeesHead)
                .WithMany(t => t.FeesAcademicClasses)
                .HasForeignKey(d => d.FeesHeadsId);
            this.HasRequired(t => t.FeesType)
                .WithMany(t => t.FeesAcademicClasses)
                .HasForeignKey(d => d.FeesTypeId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.FeesAcademicClasses)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
