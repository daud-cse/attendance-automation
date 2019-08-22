using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class RoutineMap : EntityTypeConfiguration<Routine>
    {
        public RoutineMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Routines");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.AcademicClassId).HasColumnName("AcademicClassId");
            this.Property(t => t.AcademicShiftId).HasColumnName("AcademicShiftId");
            this.Property(t => t.AcademicSectionId).HasColumnName("AcademicSectionId");
            this.Property(t => t.AcamedicGroupId).HasColumnName("AcamedicGroupId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.RoutineNoteId).HasColumnName("RoutineNoteId");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.RoutineTypeId).HasColumnName("RoutineTypeId");
            this.Property(t => t.BuildingRoomId).HasColumnName("BuildingRoomId");
            this.Property(t => t.RoutineOtherDutyTypeId).HasColumnName("RoutineOtherDutyTypeId");

            // Relationships
            this.HasRequired(t => t.AcademicBranch)
                .WithMany(t => t.Routines)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasOptional(t => t.AcademicClass)
                .WithMany(t => t.Routines)
                .HasForeignKey(d => d.AcademicClassId);
            this.HasOptional(t => t.AcademicGroup)
                .WithMany(t => t.Routines)
                .HasForeignKey(d => d.AcamedicGroupId);
            this.HasOptional(t => t.AcademicSection)
                .WithMany(t => t.Routines)
                .HasForeignKey(d => d.AcademicSectionId);
            this.HasOptional(t => t.AcademicSession)
                .WithMany(t => t.Routines)
                .HasForeignKey(d => d.AcademicSessionId);
            this.HasOptional(t => t.AcademicShift)
                .WithMany(t => t.Routines)
                .HasForeignKey(d => d.AcademicShiftId);
            this.HasOptional(t => t.BuildingRoom)
                .WithMany(t => t.Routines)
                .HasForeignKey(d => d.BuildingRoomId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.Routines)
                .HasForeignKey(d => d.InstituteId);
            this.HasOptional(t => t.RoutineNote)
                .WithMany(t => t.Routines)
                .HasForeignKey(d => d.RoutineNoteId);

        }
    }
}
