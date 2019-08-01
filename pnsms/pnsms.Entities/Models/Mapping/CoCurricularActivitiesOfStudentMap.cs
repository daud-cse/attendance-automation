using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class CoCurricularActivitiesOfStudentMap : EntityTypeConfiguration<CoCurricularActivitiesOfStudent>
    {
        public CoCurricularActivitiesOfStudentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("CoCurricularActivitiesOfStudents");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.CoCurricularActivityId).HasColumnName("CoCurricularActivityId");

            // Relationships
            this.HasRequired(t => t.CoCurricularActivity)
                .WithMany(t => t.CoCurricularActivitiesOfStudents)
                .HasForeignKey(d => d.CoCurricularActivityId);
            this.HasRequired(t => t.Student)
                .WithMany(t => t.CoCurricularActivitiesOfStudents)
                .HasForeignKey(d => d.StudentId);

        }
    }
}
