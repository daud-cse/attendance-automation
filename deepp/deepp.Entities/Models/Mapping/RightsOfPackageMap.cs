using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class RightsOfPackageMap : EntityTypeConfiguration<RightsOfPackage>
    {
        public RightsOfPackageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("RightsOfPackages");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PackageId).HasColumnName("PackageId");
            this.Property(t => t.RightId).HasColumnName("RightId");

            // Relationships
            this.HasRequired(t => t.Package)
                .WithMany(t => t.RightsOfPackages)
                .HasForeignKey(d => d.PackageId);
            this.HasRequired(t => t.Right)
                .WithMany(t => t.RightsOfPackages)
                .HasForeignKey(d => d.RightId);

        }
    }
}
