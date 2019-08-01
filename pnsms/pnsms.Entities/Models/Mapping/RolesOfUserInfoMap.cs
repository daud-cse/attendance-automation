using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class RolesOfUserInfoMap : EntityTypeConfiguration<RolesOfUserInfo>
    {
        public RolesOfUserInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("RolesOfUserInfoes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserInfoId).HasColumnName("UserInfoId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");

            // Relationships
            this.HasRequired(t => t.Role)
                .WithMany(t => t.RolesOfUserInfoes)
                .HasForeignKey(d => d.RoleId);
            this.HasRequired(t => t.UserInfo)
                .WithMany(t => t.RolesOfUserInfoes)
                .HasForeignKey(d => d.UserInfoId);

        }
    }
}
