using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class QryRightsOfUserInfoMap : EntityTypeConfiguration<QryRightsOfUserInfo>
    {
        public QryRightsOfUserInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.RightId);

            // Properties
            this.Property(t => t.RightId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(128);

            this.Property(t => t.Code)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("QryRightsOfUserInfoes");
            this.Property(t => t.UserInfoId).HasColumnName("UserInfoId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.RightId).HasColumnName("RightId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
        }
    }
}
