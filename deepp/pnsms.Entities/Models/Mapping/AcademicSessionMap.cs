using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class AcademicSessionMap : EntityTypeConfiguration<AcademicSession>
    {
        public AcademicSessionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("AcademicSessions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.StartAt).HasColumnName("StartAt");
            this.Property(t => t.EndAt).HasColumnName("EndAt");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.IsCompleted).HasColumnName("IsCompleted");
            this.Property(t => t.IsRunning).HasColumnName("IsRunning");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.AcademicSessions)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
