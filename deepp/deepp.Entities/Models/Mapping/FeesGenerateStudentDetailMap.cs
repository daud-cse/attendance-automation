using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FeesGenerateStudentDetailMap : EntityTypeConfiguration<FeesGenerateStudentDetail>
    {
        public FeesGenerateStudentDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FeesGenerateStudentDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FeesGenerateId).HasColumnName("FeesGenerateId");
            this.Property(t => t.FeesGenerateStudentId).HasColumnName("FeesGenerateStudentId");
            this.Property(t => t.FeesHeadId).HasColumnName("FeesHeadId");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.IsAmmenment).HasColumnName("IsAmmenment");

            // Relationships
            this.HasRequired(t => t.FeesGenerate)
                .WithMany(t => t.FeesGenerateStudentDetails)
                .HasForeignKey(d => d.FeesGenerateId);
            this.HasRequired(t => t.FeesGenerateStudent)
                .WithMany(t => t.FeesGenerateStudentDetails)
                .HasForeignKey(d => d.FeesGenerateStudentId);
            this.HasRequired(t => t.FeesHead)
                .WithMany(t => t.FeesGenerateStudentDetails)
                .HasForeignKey(d => d.FeesHeadId);

        }
    }
}
