using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class MachineInfoMap : EntityTypeConfiguration<MachineInfo>
    {
        public MachineInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.MachineInfoId);

            // Properties
            this.Property(t => t.deviceinfo)
                .HasMaxLength(500);

            this.Property(t => t.Name)
                .HasMaxLength(500);

            this.Property(t => t.DateTimeRecord)
                .HasMaxLength(500);

            this.Property(t => t.StudentOrTeacherId)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MachineInfo");
            this.Property(t => t.MachineInfoId).HasColumnName("MachineInfoId");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.MachineNumber).HasColumnName("MachineNumber");
            this.Property(t => t.deviceinfo).HasColumnName("deviceinfo");
            this.Property(t => t.IndRegID).HasColumnName("IndRegID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DateTimeRecord).HasColumnName("DateTimeRecord");
            this.Property(t => t.InTime).HasColumnName("InTime");
            this.Property(t => t.OutTime).HasColumnName("OutTime");
            this.Property(t => t.StudentOrTeacherId).HasColumnName("StudentOrTeacherId");
            this.Property(t => t.DateOnlyRecord).HasColumnName("DateOnlyRecord");
            this.Property(t => t.TimeOnlyRecord).HasColumnName("TimeOnlyRecord");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsProcess).HasColumnName("IsProcess");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.UserId).HasColumnName("UserId");
        }
    }
}
