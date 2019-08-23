using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class RightMap : EntityTypeConfiguration<Right>
    {
        public RightMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Description)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("Rights");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.EnableEmployee).HasColumnName("EnableEmployee");
            this.Property(t => t.EnableTeacher).HasColumnName("EnableTeacher");
            this.Property(t => t.EnableStudent).HasColumnName("EnableStudent");
            this.Property(t => t.EnableGuardian).HasColumnName("EnableGuardian");
            this.Property(t => t.EnableGlobalUser).HasColumnName("EnableGlobalUser");
            this.Property(t => t.EnableGoverningbody).HasColumnName("EnableGoverningbody");
        }
    }
}
