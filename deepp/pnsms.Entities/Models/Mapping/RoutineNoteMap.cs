using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class RoutineNoteMap : EntityTypeConfiguration<RoutineNote>
    {
        public RoutineNoteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Note)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("RoutineNotes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.Name).HasColumnName("Name");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.RoutineNotes)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
