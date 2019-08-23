using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class CoCurricularActivityMap : EntityTypeConfiguration<CoCurricularActivity>
    {
        public CoCurricularActivityMap()
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
            this.ToTable("CoCurricularActivities");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.GlobalCoCurricularActivitiyId).HasColumnName("GlobalCoCurricularActivitiyId");

            // Relationships
            this.HasOptional(t => t.GlobalCoCurricularActivity)
                .WithMany(t => t.CoCurricularActivities)
                .HasForeignKey(d => d.GlobalCoCurricularActivitiyId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.CoCurricularActivities)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
