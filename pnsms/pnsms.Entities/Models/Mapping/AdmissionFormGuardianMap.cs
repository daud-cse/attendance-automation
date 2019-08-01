using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class AdmissionFormGuardianMap : EntityTypeConfiguration<AdmissionFormGuardian>
    {
        public AdmissionFormGuardianMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(128);

            this.Property(t => t.MiddleName)
                .HasMaxLength(128);

            this.Property(t => t.LastName)
                .HasMaxLength(128);

            this.Property(t => t.Name)
                .HasMaxLength(128);

            this.Property(t => t.ContactNumber1)
                .HasMaxLength(32);

            this.Property(t => t.ContactNumber2)
                .HasMaxLength(32);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(128);

            this.Property(t => t.SSN)
                .HasMaxLength(128);

            this.Property(t => t.PassportNo)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("AdmissionFormGuardians");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AdmissionFormId).HasColumnName("AdmissionFormId");
            this.Property(t => t.GuardianTypeId).HasColumnName("GuardianTypeId");
            this.Property(t => t.EducationalQualificationId).HasColumnName("EducationalQualificationId");
            this.Property(t => t.ProfessionId).HasColumnName("ProfessionId");
            this.Property(t => t.MonthlyIncome).HasColumnName("MonthlyIncome");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ContactNumber1).HasColumnName("ContactNumber1");
            this.Property(t => t.ContactNumber2).HasColumnName("ContactNumber2");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.SSN).HasColumnName("SSN");
            this.Property(t => t.PassportNo).HasColumnName("PassportNo");
            this.Property(t => t.DOB).HasColumnName("DOB");
            this.Property(t => t.GenderId).HasColumnName("GenderId");
            this.Property(t => t.NationalityId).HasColumnName("NationalityId");
            this.Property(t => t.ReligionId).HasColumnName("ReligionId");
            this.Property(t => t.BloodGroupId).HasColumnName("BloodGroupId");

            // Relationships
            this.HasRequired(t => t.AdmissionForm)
                .WithMany(t => t.AdmissionFormGuardians)
                .HasForeignKey(d => d.AdmissionFormId);
            this.HasOptional(t => t.EducationalQualification)
                .WithMany(t => t.AdmissionFormGuardians)
                .HasForeignKey(d => d.EducationalQualificationId);
            this.HasRequired(t => t.GuardianType)
                .WithMany(t => t.AdmissionFormGuardians)
                .HasForeignKey(d => d.GuardianTypeId);
            this.HasOptional(t => t.Profession)
                .WithMany(t => t.AdmissionFormGuardians)
                .HasForeignKey(d => d.ProfessionId);

        }
    }
}
