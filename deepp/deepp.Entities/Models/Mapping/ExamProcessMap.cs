using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class ExamProcessMap : EntityTypeConfiguration<ExamProcess>
    {
        public ExamProcessMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("ExamProcesses");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.ExamTypeId).HasColumnName("ExamTypeId");
            this.Property(t => t.AcademicClassesId).HasColumnName("AcademicClassesId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.AcademicShiftId).HasColumnName("AcademicShiftId");
            this.Property(t => t.AcademicSectionId).HasColumnName("AcademicSectionId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.RunExamProcessAt).HasColumnName("RunExamProcessAt");
            this.Property(t => t.RunReportCardProcessAt).HasColumnName("RunReportCardProcessAt");
            this.Property(t => t.RunConsolidateReportProcessAt).HasColumnName("RunConsolidateReportProcessAt");

            // Relationships
            this.HasRequired(t => t.AcademicBranch)
                .WithMany(t => t.ExamProcesses)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasRequired(t => t.AcademicClass)
                .WithMany(t => t.ExamProcesses)
                .HasForeignKey(d => d.AcademicClassesId);
            this.HasOptional(t => t.AcademicSection)
                .WithMany(t => t.ExamProcesses)
                .HasForeignKey(d => d.AcademicSectionId);
            this.HasRequired(t => t.AcademicSession)
                .WithMany(t => t.ExamProcesses)
                .HasForeignKey(d => d.AcademicSessionId);
            this.HasOptional(t => t.AcademicShift)
                .WithMany(t => t.ExamProcesses)
                .HasForeignKey(d => d.AcademicShiftId);
            this.HasRequired(t => t.ExamType)
                .WithMany(t => t.ExamProcesses)
                .HasForeignKey(d => d.ExamTypeId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.ExamProcesses)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
