using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class FeesCollectionAdvanceMap : EntityTypeConfiguration<FeesCollectionAdvance>
    {
        public FeesCollectionAdvanceMap()
        {
            // Primary Key
            this.HasKey(t => t.StudentId);

            // Properties
            this.Property(t => t.StudentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FeesCollectionAdvances");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.Amount).HasColumnName("Amount");

            // Relationships
            this.HasRequired(t => t.Student)
                .WithOptional(t => t.FeesCollectionAdvance);

        }
    }
}
