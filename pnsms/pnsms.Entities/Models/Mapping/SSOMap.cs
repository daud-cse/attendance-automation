using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class SSOMap : EntityTypeConfiguration<SSO>
    {
        public SSOMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Tokenkey)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.UserName)
                .HasMaxLength(50);

            this.Property(t => t.DeviceInfo)
                .HasMaxLength(500);

            this.Property(t => t.IPAddress)
                .HasMaxLength(500);

            this.Property(t => t.Country)
                .HasMaxLength(500);

            this.Property(t => t.Subject)
                .HasMaxLength(500);

            this.Property(t => t.ClientId)
                .HasMaxLength(500);

            this.Property(t => t.SessionId)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("SSO");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Tokenkey).HasColumnName("Tokenkey");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.UserTypeId).HasColumnName("UserTypeId");
            this.Property(t => t.DeviceInfo).HasColumnName("DeviceInfo");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.ClientId).HasColumnName("ClientId");
            this.Property(t => t.IssuedUtc).HasColumnName("IssuedUtc");
            this.Property(t => t.ExpiresUtc).HasColumnName("ExpiresUtc");
            this.Property(t => t.SessionId).HasColumnName("SessionId");
            this.Property(t => t.LogDate).HasColumnName("LogDate");
        }
    }
}
