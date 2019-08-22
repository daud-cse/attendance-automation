using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class SubjectTypeMap : EntityTypeConfiguration<SubjectType>
    {
        public SubjectTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("SubjectTypes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.IsMandatory).HasColumnName("IsMandatory");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.IsFailApplicable).HasColumnName("IsFailApplicable");
            this.Property(t => t.HandicapGradePoint).HasColumnName("HandicapGradePoint");
            this.Property(t => t.HandicapMarks).HasColumnName("HandicapMarks");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.SubjectTypes)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
