using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class GlobalCoCurricularActivityMap : EntityTypeConfiguration<GlobalCoCurricularActivity>
    {
        public GlobalCoCurricularActivityMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("GlobalCoCurricularActivities");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
