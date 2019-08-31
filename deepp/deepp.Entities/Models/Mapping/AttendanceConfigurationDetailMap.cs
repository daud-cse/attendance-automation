using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class AttendanceConfigurationDetailMap : EntityTypeConfiguration<AttendanceConfigurationDetail>
    {
        public AttendanceConfigurationDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.MachineNo)
                .HasMaxLength(500);

            this.Property(t => t.MachineSerialNo)
                .HasMaxLength(500);

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("AttendanceConfigurationDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AttendanceConfigurationId).HasColumnName("AttendanceConfigurationId");
            this.Property(t => t.MachineNo).HasColumnName("MachineNo");
            this.Property(t => t.MachineSerialNo).HasColumnName("MachineSerialNo");
            this.Property(t => t.MachineUseTypeId).HasColumnName("MachineUseTypeId");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AttendanceStartSynDate).HasColumnName("AttendanceStartSynDate");
            this.Property(t => t.AttendanceLastSynDate).HasColumnName("AttendanceLastSynDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");

            // Relationships
            this.HasRequired(t => t.AttendanceConfiguration)
                .WithMany(t => t.AttendanceConfigurationDetails)
                .HasForeignKey(d => d.AttendanceConfigurationId);

        }
    }
}
