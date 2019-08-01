using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class UserInfoSecurityMap : EntityTypeConfiguration<UserInfoSecurity>
    {
        public UserInfoSecurityMap()
        {
            // Primary Key
            this.HasKey(t => t.UserInfoId);

            // Properties
            this.Property(t => t.UserInfoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.PasswordHash)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("UserInfoSecurities");
            this.Property(t => t.UserInfoId).HasColumnName("UserInfoId");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.SecurityStamp).HasColumnName("SecurityStamp");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsLockout).HasColumnName("IsLockout");
            this.Property(t => t.LastLoggedinAt).HasColumnName("LastLoggedinAt");
            this.Property(t => t.LastPasswordChangedAt).HasColumnName("LastPasswordChangedAt");
            this.Property(t => t.LastLockoutAt).HasColumnName("LastLockoutAt");
            this.Property(t => t.FailedPasswordAttemptCount).HasColumnName("FailedPasswordAttemptCount");
            this.Property(t => t.FailedPasswordAttemptWindowStart).HasColumnName("FailedPasswordAttemptWindowStart");
            this.Property(t => t.Comment).HasColumnName("Comment");

            // Relationships
            this.HasOptional(t => t.Institute)
                .WithMany(t => t.UserInfoSecurities)
                .HasForeignKey(d => d.InstituteId);
            this.HasRequired(t => t.UserInfo)
                .WithOptional(t => t.UserInfoSecurity);

        }
    }
}
