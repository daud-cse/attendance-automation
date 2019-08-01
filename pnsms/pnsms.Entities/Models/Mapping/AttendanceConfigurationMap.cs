using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class AttendanceConfigurationMap : EntityTypeConfiguration<AttendanceConfiguration>
    {
        public AttendanceConfigurationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MachineNo)
                .HasMaxLength(10);

            this.Property(t => t.MachineSerialNo)
                .HasMaxLength(500);

            this.Property(t => t.MachineNo1)
                .HasMaxLength(10);

            this.Property(t => t.MachineSerialNo1)
                .HasMaxLength(500);

            this.Property(t => t.MachineNo2)
                .HasMaxLength(10);

            this.Property(t => t.MachineSerialNo2)
                .HasMaxLength(500);

            this.Property(t => t.MachineNo3)
                .HasMaxLength(10);

            this.Property(t => t.MachineSerialNo3)
                .HasMaxLength(500);

            this.Property(t => t.MachineNo4)
                .HasMaxLength(10);

            this.Property(t => t.MachineSerialNo4)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("AttendanceConfiguration");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MachineNo).HasColumnName("MachineNo");
            this.Property(t => t.MachineSerialNo).HasColumnName("MachineSerialNo");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AttendanceStartSynDate).HasColumnName("AttendanceStartSynDate");
            this.Property(t => t.AttendanceLastSynDate).HasColumnName("AttendanceLastSynDate");
            this.Property(t => t.MachineNo1).HasColumnName("MachineNo1");
            this.Property(t => t.MachineSerialNo1).HasColumnName("MachineSerialNo1");
            this.Property(t => t.MachineNo2).HasColumnName("MachineNo2");
            this.Property(t => t.MachineSerialNo2).HasColumnName("MachineSerialNo2");
            this.Property(t => t.MachineNo3).HasColumnName("MachineNo3");
            this.Property(t => t.MachineSerialNo3).HasColumnName("MachineSerialNo3");
            this.Property(t => t.MachineNo4).HasColumnName("MachineNo4");
            this.Property(t => t.MachineSerialNo4).HasColumnName("MachineSerialNo4");
        }
    }
}
