using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class ResultPublicationMap : EntityTypeConfiguration<ResultPublication>
    {
        public ResultPublicationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("ResultPublications");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.AcademicSession)
                .WithMany(t => t.ResultPublications)
                .HasForeignKey(d => d.AcademicSessionId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.ResultPublications)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
