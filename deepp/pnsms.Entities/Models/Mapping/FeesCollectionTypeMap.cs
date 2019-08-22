using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class FeesCollectionTypeMap : EntityTypeConfiguration<FeesCollectionType>
    {
        public FeesCollectionTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Description)
                .HasMaxLength(512);

            this.Property(t => t.RefNoTitle)
                .HasMaxLength(512);

            this.Property(t => t.MobileNoTitle)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("FeesCollectionTypes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.IsShowRefNo).HasColumnName("IsShowRefNo");
            this.Property(t => t.RefNoTitle).HasColumnName("RefNoTitle");
            this.Property(t => t.IsShowMobileNo).HasColumnName("IsShowMobileNo");
            this.Property(t => t.MobileNoTitle).HasColumnName("MobileNoTitle");
        }
    }
}
