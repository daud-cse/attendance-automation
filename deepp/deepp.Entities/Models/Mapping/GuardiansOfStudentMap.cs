using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class GuardiansOfStudentMap : EntityTypeConfiguration<GuardiansOfStudent>
    {
        public GuardiansOfStudentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("GuardiansOfStudents");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.GuardianId).HasColumnName("GuardianId");
            this.Property(t => t.IsLocalGuardian).HasColumnName("IsLocalGuardian");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.Guardian)
                .WithMany(t => t.GuardiansOfStudents)
                .HasForeignKey(d => d.GuardianId);
            this.HasRequired(t => t.Student)
                .WithMany(t => t.GuardiansOfStudents)
                .HasForeignKey(d => d.StudentId);

        }
    }
}
