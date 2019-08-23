using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class GlobalDivisionMap : EntityTypeConfiguration<GlobalDivision>
    {
        public GlobalDivisionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("GlobalDivisions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.GlobalCountryId).HasColumnName("GlobalCountryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.GlobalCountry)
                .WithMany(t => t.GlobalDivisions)
                .HasForeignKey(d => d.GlobalCountryId);

        }
    }
}
