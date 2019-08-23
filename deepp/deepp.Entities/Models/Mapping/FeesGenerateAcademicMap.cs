using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FeesGenerateAcademicMap : EntityTypeConfiguration<FeesGenerateAcademic>
    {
        public FeesGenerateAcademicMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FeesGenerateAcademics");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FeesGenerateId).HasColumnName("FeesGenerateId");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.AcademicClassId).HasColumnName("AcademicClassId");
            this.Property(t => t.AcademicShiftId).HasColumnName("AcademicShiftId");
            this.Property(t => t.AcademicSectionId).HasColumnName("AcademicSectionId");
            this.Property(t => t.AcademicVerssionId).HasColumnName("AcademicVerssionId");
            this.Property(t => t.AcademicGroupId).HasColumnName("AcademicGroupId");

            // Relationships
            this.HasOptional(t => t.AcademicBranch)
                .WithMany(t => t.FeesGenerateAcademics)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasOptional(t => t.AcademicClass)
                .WithMany(t => t.FeesGenerateAcademics)
                .HasForeignKey(d => d.AcademicClassId);
            this.HasOptional(t => t.AcademicGroup)
                .WithMany(t => t.FeesGenerateAcademics)
                .HasForeignKey(d => d.AcademicGroupId);
            this.HasOptional(t => t.AcademicSection)
                .WithMany(t => t.FeesGenerateAcademics)
                .HasForeignKey(d => d.AcademicSectionId);
            this.HasOptional(t => t.AcademicSession)
                .WithMany(t => t.FeesGenerateAcademics)
                .HasForeignKey(d => d.AcademicSessionId);
            this.HasOptional(t => t.AcademicShift)
                .WithMany(t => t.FeesGenerateAcademics)
                .HasForeignKey(d => d.AcademicShiftId);
            this.HasOptional(t => t.AcademicVersion)
                .WithMany(t => t.FeesGenerateAcademics)
                .HasForeignKey(d => d.AcademicVerssionId);
            this.HasRequired(t => t.FeesGenerate)
                .WithMany(t => t.FeesGenerateAcademics)
                .HasForeignKey(d => d.FeesGenerateId);

        }
    }
}
