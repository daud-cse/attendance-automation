using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class AcademicCalendarMap : EntityTypeConfiguration<AcademicCalendar>
    {
        public AcademicCalendarMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Description)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("AcademicCalendars");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsInstituteClose).HasColumnName("IsInstituteClose");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.IsClassSuspend).HasColumnName("IsClassSuspend");
            this.Property(t => t.IsClassSuspendAllBranch).HasColumnName("IsClassSuspendAllBranch");
            this.Property(t => t.IsClassSuspendAllClass).HasColumnName("IsClassSuspendAllClass");
            this.Property(t => t.IsClassSuspendAllShift).HasColumnName("IsClassSuspendAllShift");

            // Relationships
            this.HasOptional(t => t.AcademicBranch)
                .WithMany(t => t.AcademicCalendars)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasRequired(t => t.AcademicSession)
                .WithMany(t => t.AcademicCalendars)
                .HasForeignKey(d => d.AcademicSessionId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.AcademicCalendars)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
