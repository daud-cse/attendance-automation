using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class RightsOfRoleMap : EntityTypeConfiguration<RightsOfRole>
    {
        public RightsOfRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("RightsOfRoles");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.RightId).HasColumnName("RightId");

            // Relationships
            this.HasRequired(t => t.Right)
                .WithMany(t => t.RightsOfRoles)
                .HasForeignKey(d => d.RightId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.RightsOfRoles)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
