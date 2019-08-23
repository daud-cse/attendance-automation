using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FMS_TransactionTypeMap : EntityTypeConfiguration<FMS_TransactionType>
    {
        public FMS_TransactionTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("FMS_TransactionType");
            this.Property(t => t.TransactionTypeId).HasColumnName("TransactionTypeId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
