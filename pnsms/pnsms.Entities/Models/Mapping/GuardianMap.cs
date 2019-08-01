using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class GuardianMap : EntityTypeConfiguration<Guardian>
    {
        public GuardianMap()
        {
            // Primary Key
            this.HasKey(t => t.GuardianId);

            // Properties
            this.Property(t => t.GuardianId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Guardians");
            this.Property(t => t.GuardianId).HasColumnName("GuardianId");
            this.Property(t => t.GuardianTypeId).HasColumnName("GuardianTypeId");
            this.Property(t => t.EducationalQualificationId).HasColumnName("EducationalQualificationId");
            this.Property(t => t.ProfessionId).HasColumnName("ProfessionId");
            this.Property(t => t.MonthlyIncome).HasColumnName("MonthlyIncome");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasOptional(t => t.EducationalQualification)
                .WithMany(t => t.Guardians)
                .HasForeignKey(d => d.EducationalQualificationId);
            this.HasRequired(t => t.GuardianType)
                .WithMany(t => t.Guardians)
                .HasForeignKey(d => d.GuardianTypeId);
            this.HasOptional(t => t.Profession)
                .WithMany(t => t.Guardians)
                .HasForeignKey(d => d.ProfessionId);
            this.HasRequired(t => t.UserInfo)
                .WithOptional(t => t.Guardian);

        }
    }
}
