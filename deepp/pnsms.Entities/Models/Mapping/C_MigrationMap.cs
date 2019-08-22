using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class C_MigrationMap : EntityTypeConfiguration<C_Migration>
    {
        public C_MigrationMap()
        {
            // Primary Key
            this.HasKey(t => t.LastUpdate);

            // Properties
            this.Property(t => t.LastUpdate)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("_Migration");
            this.Property(t => t.LastUpdate).HasColumnName("LastUpdate");
        }
    }
}
