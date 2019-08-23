using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Description)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("Roles");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsForEmployee).HasColumnName("IsForEmployee");
            this.Property(t => t.IsForTeacher).HasColumnName("IsForTeacher");
            this.Property(t => t.IsForStudent).HasColumnName("IsForStudent");
            this.Property(t => t.IsForGuardian).HasColumnName("IsForGuardian");
            this.Property(t => t.GuardianTypeId).HasColumnName("GuardianTypeId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.EnableGlobalUser).HasColumnName("EnableGlobalUser");
            this.Property(t => t.GlobalUserTypeId).HasColumnName("GlobalUserTypeId");
            this.Property(t => t.EnableGoverningbody).HasColumnName("EnableGoverningbody");

            // Relationships
            this.HasOptional(t => t.GuardianType)
                .WithMany(t => t.Roles)
                .HasForeignKey(d => d.GuardianTypeId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.Roles)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
