using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class SubjectAcademicClassMappingMap : EntityTypeConfiguration<SubjectAcademicClassMapping>
    {
        public SubjectAcademicClassMappingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SubjectGroupNameList)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("SubjectAcademicClassMappings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.AcademicClassId).HasColumnName("AcademicClassId");
            this.Property(t => t.AcademicClassSectionMapId).HasColumnName("AcademicClassSectionMapId");
            this.Property(t => t.SubjectMarks).HasColumnName("SubjectMarks");
            this.Property(t => t.SubjectId).HasColumnName("SubjectId");
            this.Property(t => t.ParentSubjectId).HasColumnName("ParentSubjectId");
            this.Property(t => t.TeacherId).HasColumnName("TeacherId");
            this.Property(t => t.OrderBy).HasColumnName("OrderBy");
            this.Property(t => t.MarksEntryTypeKey).HasColumnName("MarksEntryTypeKey");
            this.Property(t => t.IsSubjectGroupWise).HasColumnName("IsSubjectGroupWise");
            this.Property(t => t.SubjectGroupId).HasColumnName("SubjectGroupId");
            this.Property(t => t.SubjectGroupNameList).HasColumnName("SubjectGroupNameList");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.AcademicGroupId).HasColumnName("AcademicGroupId");
            this.Property(t => t.SubjectTypeId).HasColumnName("SubjectTypeId");
            this.Property(t => t.ExamDate).HasColumnName("ExamDate");
            this.Property(t => t.IsFailApplicable).HasColumnName("IsFailApplicable");

            // Relationships
            this.HasRequired(t => t.AcademicBranch)
                .WithMany(t => t.SubjectAcademicClassMappings)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasRequired(t => t.AcademicClass)
                .WithMany(t => t.SubjectAcademicClassMappings)
                .HasForeignKey(d => d.AcademicClassId);
            this.HasRequired(t => t.AcademicClassSectionMapping)
                .WithMany(t => t.SubjectAcademicClassMappings)
                .HasForeignKey(d => d.AcademicClassSectionMapId);
            this.HasOptional(t => t.AcademicGroup)
                .WithMany(t => t.SubjectAcademicClassMappings)
                .HasForeignKey(d => d.AcademicGroupId);
            this.HasRequired(t => t.AcademicSession)
                .WithMany(t => t.SubjectAcademicClassMappings)
                .HasForeignKey(d => d.AcademicSessionId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.SubjectAcademicClassMappings)
                .HasForeignKey(d => d.InstituteId);
            this.HasRequired(t => t.InstituteSubject)
                .WithMany(t => t.SubjectAcademicClassMappings)
                .HasForeignKey(d => d.SubjectId);
            this.HasOptional(t => t.SubjectGroup)
                .WithMany(t => t.SubjectAcademicClassMappings)
                .HasForeignKey(d => d.SubjectGroupId);
            this.HasRequired(t => t.SubjectType)
                .WithMany(t => t.SubjectAcademicClassMappings)
                .HasForeignKey(d => d.SubjectTypeId);
            this.HasRequired(t => t.Teacher)
                .WithMany(t => t.SubjectAcademicClassMappings)
                .HasForeignKey(d => d.TeacherId);

        }
    }
}
