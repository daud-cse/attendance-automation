using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
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
            this.ToTable("Countries");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.GlobalCountryId).HasColumnName("GlobalCountryId");

            // Relationships
            this.HasOptional(t => t.GlobalCountry)
                .WithMany(t => t.Countries)
                .HasForeignKey(d => d.GlobalCountryId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.Countries)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
