using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class AcademicClassSectionMappingMap : EntityTypeConfiguration<AcademicClassSectionMapping>
    {
        public AcademicClassSectionMappingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SectionName)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("AcademicClassSectionMappings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.AcademicShiftId).HasColumnName("AcademicShiftId");
            this.Property(t => t.AcademicClassId).HasColumnName("AcademicClassId");
            this.Property(t => t.AcademicSectionId).HasColumnName("AcademicSectionId");
            this.Property(t => t.SectionName).HasColumnName("SectionName");
            this.Property(t => t.ClassTeacherId).HasColumnName("ClassTeacherId");
            this.Property(t => t.AssClassTeacherId).HasColumnName("AssClassTeacherId");
            this.Property(t => t.AcademicGroupId).HasColumnName("AcademicGroupId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.AcademicBranch)
                .WithMany(t => t.AcademicClassSectionMappings)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasRequired(t => t.AcademicClass)
                .WithMany(t => t.AcademicClassSectionMappings)
                .HasForeignKey(d => d.AcademicClassId);
            this.HasOptional(t => t.AcademicGroup)
                .WithMany(t => t.AcademicClassSectionMappings)
                .HasForeignKey(d => d.AcademicGroupId);
            this.HasRequired(t => t.AcademicSection)
                .WithMany(t => t.AcademicClassSectionMappings)
                .HasForeignKey(d => d.AcademicSectionId);
            this.HasOptional(t => t.AcademicShift)
                .WithMany(t => t.AcademicClassSectionMappings)
                .HasForeignKey(d => d.AcademicShiftId);
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.AcademicClassSectionMappings)
                .HasForeignKey(d => d.ClassTeacherId);
            this.HasOptional(t => t.Employee1)
                .WithMany(t => t.AcademicClassSectionMappings1)
                .HasForeignKey(d => d.AssClassTeacherId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.AcademicClassSectionMappings)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
