using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class StudentMap : EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            // Primary Key
            this.HasKey(t => t.StudentId);

            // Properties
            this.Property(t => t.StudentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CurrentRollNo)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Students");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.CurrentAcademicSessionId).HasColumnName("CurrentAcademicSessionId");
            this.Property(t => t.CurrentAcademicBranchId).HasColumnName("CurrentAcademicBranchId");
            this.Property(t => t.CurrentAcademicClassId).HasColumnName("CurrentAcademicClassId");
            this.Property(t => t.CurrentAcademicShiftId).HasColumnName("CurrentAcademicShiftId");
            this.Property(t => t.CurrentAcademicSectionId).HasColumnName("CurrentAcademicSectionId");
            this.Property(t => t.CurrentAcademicVerssionId).HasColumnName("CurrentAcademicVerssionId");
            this.Property(t => t.CurrentAcademicGroupId).HasColumnName("CurrentAcademicGroupId");
            this.Property(t => t.CurrentRollNo).HasColumnName("CurrentRollNo");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.IsCurrent).HasColumnName("IsCurrent");

            // Relationships
            this.HasOptional(t => t.AcademicBranch)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.CurrentAcademicBranchId);
            this.HasOptional(t => t.AcademicClass)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.CurrentAcademicClassId);
            this.HasOptional(t => t.AcademicClassSectionMapping)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.CurrentAcademicSectionId);
            this.HasOptional(t => t.AcademicGroup)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.CurrentAcademicGroupId);
            this.HasOptional(t => t.AcademicSession)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.CurrentAcademicSessionId);
            this.HasOptional(t => t.AcademicShift)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.CurrentAcademicShiftId);
            this.HasOptional(t => t.AcademicVersion)
                .WithMany(t => t.Students)
                .HasForeignKey(d => d.CurrentAcademicVerssionId);
            this.HasRequired(t => t.UserInfo)
                .WithOptional(t => t.Student);

        }
    }
}
