using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class GlobalUserMap : EntityTypeConfiguration<GlobalUser>
    {
        public GlobalUserMap()
        {
            // Primary Key
            this.HasKey(t => t.GlobalUserId);

            // Properties
            this.Property(t => t.GlobalUserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FatherName)
                .HasMaxLength(128);

            this.Property(t => t.MotherName)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("GlobalUsers");
            this.Property(t => t.GlobalUserId).HasColumnName("GlobalUserId");
            this.Property(t => t.UserInfoTypeId).HasColumnName("UserInfoTypeId");
            this.Property(t => t.GlobalUserTypeId).HasColumnName("GlobalUserTypeId");
            this.Property(t => t.FatherName).HasColumnName("FatherName");
            this.Property(t => t.MotherName).HasColumnName("MotherName");
            this.Property(t => t.MaritalStatusId).HasColumnName("MaritalStatusId");
            this.Property(t => t.DesignationId).HasColumnName("DesignationId");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
            this.Property(t => t.GlobalCountryId).HasColumnName("GlobalCountryId");
            this.Property(t => t.GlobalDivisionId).HasColumnName("GlobalDivisionId");
            this.Property(t => t.GlobalDistrictId).HasColumnName("GlobalDistrictId");
            this.Property(t => t.GlobalSubDistrictId).HasColumnName("GlobalSubDistrictId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasOptional(t => t.Department)
                .WithMany(t => t.GlobalUsers)
                .HasForeignKey(d => d.DepartmentId);
            this.HasOptional(t => t.Designation)
                .WithMany(t => t.GlobalUsers)
                .HasForeignKey(d => d.DesignationId);
            this.HasOptional(t => t.GlobalCountry)
                .WithMany(t => t.GlobalUsers)
                .HasForeignKey(d => d.GlobalCountryId);
            this.HasOptional(t => t.GlobalDistrict)
                .WithMany(t => t.GlobalUsers)
                .HasForeignKey(d => d.GlobalDistrictId);
            this.HasOptional(t => t.GlobalDivision)
                .WithMany(t => t.GlobalUsers)
                .HasForeignKey(d => d.GlobalDivisionId);
            this.HasOptional(t => t.GlobalSubDistrict)
                .WithMany(t => t.GlobalUsers)
                .HasForeignKey(d => d.GlobalSubDistrictId);
            this.HasRequired(t => t.GlobalUserType)
                .WithMany(t => t.GlobalUsers)
                .HasForeignKey(d => d.GlobalUserTypeId);
            this.HasOptional(t => t.MaritalStatus)
                .WithMany(t => t.GlobalUsers)
                .HasForeignKey(d => d.MaritalStatusId);
            this.HasRequired(t => t.UserInfo)
                .WithOptional(t => t.GlobalUser);

        }
    }
}
