using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class FeesCollectionMap : EntityTypeConfiguration<FeesCollection>
    {
        public FeesCollectionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FeesCollections");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.CollectionDate).HasColumnName("CollectionDate");
            this.Property(t => t.FeesHeadsId).HasColumnName("FeesHeadsId");
            this.Property(t => t.AcademicClassId).HasColumnName("AcademicClassId");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.TotalReceiveAmount).HasColumnName("TotalReceiveAmount");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.FeesHead)
                .WithMany(t => t.FeesCollections)
                .HasForeignKey(d => d.FeesHeadsId);
            this.HasRequired(t => t.Student)
                .WithMany(t => t.FeesCollections)
                .HasForeignKey(d => d.StudentId);

        }
    }
}
