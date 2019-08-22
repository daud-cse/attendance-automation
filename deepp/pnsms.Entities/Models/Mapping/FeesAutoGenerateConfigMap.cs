using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class FeesAutoGenerateConfigMap : EntityTypeConfiguration<FeesAutoGenerateConfig>
    {
        public FeesAutoGenerateConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(512);

            this.Property(t => t.Description)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("FeesAutoGenerateConfig");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsAllAcademicBranch).HasColumnName("IsAllAcademicBranch");
            this.Property(t => t.IsAllAcademicVerssion).HasColumnName("IsAllAcademicVerssion");
            this.Property(t => t.IsAllAcademicClass).HasColumnName("IsAllAcademicClass");
            this.Property(t => t.IsAllAcademicShift).HasColumnName("IsAllAcademicShift");
            this.Property(t => t.IsAllAcademicGroup).HasColumnName("IsAllAcademicGroup");
            this.Property(t => t.IsAllFacility).HasColumnName("IsAllFacility");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.FeesAutoGenerateConfigTypeId).HasColumnName("FeesAutoGenerateConfigTypeId");
            this.Property(t => t.FacilityId).HasColumnName("FacilityId");

            // Relationships
            this.HasOptional(t => t.FeesAutoGenerateConfigType)
                .WithMany(t => t.FeesAutoGenerateConfigs)
                .HasForeignKey(d => d.FeesAutoGenerateConfigTypeId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.FeesAutoGenerateConfigs)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
