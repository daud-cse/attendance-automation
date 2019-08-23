using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class RoutineDetailMap : EntityTypeConfiguration<RoutineDetail>
    {
        public RoutineDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Remarks)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("RoutineDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RoutineId).HasColumnName("RoutineId");
            this.Property(t => t.WeekDayId).HasColumnName("WeekDayId");
            this.Property(t => t.RoutinePeriodId).HasColumnName("RoutinePeriodId");
            this.Property(t => t.SubjectId).HasColumnName("SubjectId");
            this.Property(t => t.TeacherId).HasColumnName("TeacherId");
            this.Property(t => t.BuildingRoomId).HasColumnName("BuildingRoomId");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.SubjectSplitId).HasColumnName("SubjectSplitId");
            this.Property(t => t.SubstituteTeacherId).HasColumnName("SubstituteTeacherId");
            this.Property(t => t.RoutineSubjectGroupId).HasColumnName("RoutineSubjectGroupId");
            this.Property(t => t.SubjectGroupId).HasColumnName("SubjectGroupId");
            this.Property(t => t.TeacherDepartmentId).HasColumnName("TeacherDepartmentId");

            // Relationships
            this.HasOptional(t => t.BuildingRoom)
                .WithMany(t => t.RoutineDetails)
                .HasForeignKey(d => d.BuildingRoomId);
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.RoutineDetails)
                .HasForeignKey(d => d.TeacherId);
            this.HasOptional(t => t.Employee1)
                .WithMany(t => t.RoutineDetails1)
                .HasForeignKey(d => d.SubstituteTeacherId);
            this.HasRequired(t => t.RoutinePeriod)
                .WithMany(t => t.RoutineDetails)
                .HasForeignKey(d => d.RoutinePeriodId);
            this.HasRequired(t => t.Routine)
                .WithMany(t => t.RoutineDetails)
                .HasForeignKey(d => d.RoutineId);
            this.HasOptional(t => t.Subject)
                .WithMany(t => t.RoutineDetails)
                .HasForeignKey(d => d.SubjectId);
            this.HasRequired(t => t.WeekDay)
                .WithMany(t => t.RoutineDetails)
                .HasForeignKey(d => d.WeekDayId);

        }
    }
}
