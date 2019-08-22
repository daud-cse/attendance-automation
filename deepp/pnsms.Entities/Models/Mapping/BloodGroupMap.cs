using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class BloodGroupMap : EntityTypeConfiguration<BloodGroup>
    {
        public BloodGroupMap()
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
            this.ToTable("BloodGroups");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.GlobalBloodGroupId).HasColumnName("GlobalBloodGroupId");

            // Relationships
            this.HasOptional(t => t.GlobalBloodGroup)
                .WithMany(t => t.BloodGroups)
                .HasForeignKey(d => d.GlobalBloodGroupId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.BloodGroups)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
