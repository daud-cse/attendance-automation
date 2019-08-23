using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class TeacherMap : EntityTypeConfiguration<Teacher>
    {
        public TeacherMap()
        {
            // Primary Key
            this.HasKey(t => t.TeacherId);

            // Properties
            this.Property(t => t.TeacherId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FatherName)
                .HasMaxLength(128);

            this.Property(t => t.MotherName)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Teachers");
            this.Property(t => t.TeacherId).HasColumnName("TeacherId");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.FatherName).HasColumnName("FatherName");
            this.Property(t => t.MotherName).HasColumnName("MotherName");
            this.Property(t => t.MaritalStatusId).HasColumnName("MaritalStatusId");
            this.Property(t => t.DesignationId).HasColumnName("DesignationId");
            this.Property(t => t.CurrentAcademicBranchId).HasColumnName("CurrentAcademicBranchId");
            this.Property(t => t.DefaultAcademicClassId).HasColumnName("DefaultAcademicClassId");
            this.Property(t => t.DefaultAcademicSectionId).HasColumnName("DefaultAcademicSectionId");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.DepartmentId).HasColumnName("DepartmentId");
            this.Property(t => t.AboutTeacher).HasColumnName("AboutTeacher");

            // Relationships
            this.HasOptional(t => t.AcademicBranch)
                .WithMany(t => t.Teachers)
                .HasForeignKey(d => d.CurrentAcademicBranchId);
            this.HasOptional(t => t.AcademicClass)
                .WithMany(t => t.Teachers)
                .HasForeignKey(d => d.DefaultAcademicClassId);
            this.HasOptional(t => t.AcademicClassSectionMapping)
                .WithMany(t => t.Teachers)
                .HasForeignKey(d => d.DefaultAcademicSectionId);
            this.HasOptional(t => t.Department)
                .WithMany(t => t.Teachers)
                .HasForeignKey(d => d.DepartmentId);
            this.HasOptional(t => t.Designation)
                .WithMany(t => t.Teachers)
                .HasForeignKey(d => d.DesignationId);
            this.HasOptional(t => t.MaritalStatus)
                .WithMany(t => t.Teachers)
                .HasForeignKey(d => d.MaritalStatusId);
            this.HasRequired(t => t.UserInfo)
                .WithOptional(t => t.Teacher);

        }
    }
}
