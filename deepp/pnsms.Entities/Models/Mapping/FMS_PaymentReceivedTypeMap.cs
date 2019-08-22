using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class FMS_PaymentReceivedTypeMap : EntityTypeConfiguration<FMS_PaymentReceivedType>
    {
        public FMS_PaymentReceivedTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.PRTypeId);

            // Properties
            this.Property(t => t.PRTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PRTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("FMS_PaymentReceivedType");
            this.Property(t => t.PRTypeId).HasColumnName("PRTypeId");
            this.Property(t => t.PRTypeName).HasColumnName("PRTypeName");
        }
    }
}
