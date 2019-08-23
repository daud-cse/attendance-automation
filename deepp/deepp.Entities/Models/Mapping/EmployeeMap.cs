using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            // Primary Key
            this.HasKey(t => t.EmployeeId);

            // Properties
            this.Property(t => t.EmployeeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FatherName)
                .HasMaxLength(128);

            this.Property(t => t.MotherName)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Employees");
            this.Property(t => t.EmployeeId).HasColumnName("EmployeeId");
            this.Property(t => t.FatherName).HasColumnName("FatherName");
            this.Property(t => t.MotherName).HasColumnName("MotherName");
            this.Property(t => t.MaritalStatusId).HasColumnName("MaritalStatusId");
            this.Property(t => t.DesignationId).HasColumnName("DesignationId");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasOptional(t => t.Department)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.DepartmentId);
            this.HasOptional(t => t.Designation)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.DesignationId);
            this.HasOptional(t => t.MaritalStatus)
                .WithMany(t => t.Employees)
                .HasForeignKey(d => d.MaritalStatusId);
            this.HasRequired(t => t.UserInfo)
                .WithOptional(t => t.Employee);

        }
    }
}
