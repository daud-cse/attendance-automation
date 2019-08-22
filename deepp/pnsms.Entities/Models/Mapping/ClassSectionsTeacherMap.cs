using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class ClassSectionsTeacherMap : EntityTypeConfiguration<ClassSectionsTeacher>
    {
        public ClassSectionsTeacherMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ClassSectionsTeacher");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.AcademicClassesId).HasColumnName("AcademicClassesId");
            this.Property(t => t.AcademicGroupId).HasColumnName("AcademicGroupId");
            this.Property(t => t.TeacherId).HasColumnName("TeacherId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateDate).HasColumnName("LastUpdateDate");
        }
    }
}
