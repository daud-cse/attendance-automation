using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class AdmissionFormMap : EntityTypeConfiguration<AdmissionForm>
    {
        public AdmissionFormMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(128);

            this.Property(t => t.FirstName)
                .HasMaxLength(128);

            this.Property(t => t.MiddleName)
                .HasMaxLength(128);

            this.Property(t => t.LastName)
                .HasMaxLength(128);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.ContactNumber)
                .HasMaxLength(32);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(128);

            this.Property(t => t.SSN)
                .HasMaxLength(128);

            this.Property(t => t.PassportNo)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("AdmissionForms");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.ImageBinaryData).HasColumnName("ImageBinaryData");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.MiddleName).HasColumnName("MiddleName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ContactNumber).HasColumnName("ContactNumber");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.SSN).HasColumnName("SSN");
            this.Property(t => t.PassportNo).HasColumnName("PassportNo");
            this.Property(t => t.DOB).HasColumnName("DOB");
            this.Property(t => t.GenderId).HasColumnName("GenderId");
            this.Property(t => t.NationalityId).HasColumnName("NationalityId");
            this.Property(t => t.ReligionId).HasColumnName("ReligionId");
            this.Property(t => t.BloodGroupId).HasColumnName("BloodGroupId");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.AcademicClassId).HasColumnName("AcademicClassId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsSelected).HasColumnName("IsSelected");

            // Relationships
            this.HasOptional(t => t.AcademicBranch)
                .WithMany(t => t.AdmissionForms)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasOptional(t => t.AcademicClass)
                .WithMany(t => t.AdmissionForms)
                .HasForeignKey(d => d.AcademicClassId);
            this.HasOptional(t => t.AcademicSession)
                .WithMany(t => t.AdmissionForms)
                .HasForeignKey(d => d.AcademicSessionId);
            this.HasOptional(t => t.BloodGroup)
                .WithMany(t => t.AdmissionForms)
                .HasForeignKey(d => d.BloodGroupId);
            this.HasOptional(t => t.Gender)
                .WithMany(t => t.AdmissionForms)
                .HasForeignKey(d => d.GenderId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.AdmissionForms)
                .HasForeignKey(d => d.InstituteId);
            this.HasOptional(t => t.Nationality)
                .WithMany(t => t.AdmissionForms)
                .HasForeignKey(d => d.NationalityId);
            this.HasOptional(t => t.Religion)
                .WithMany(t => t.AdmissionForms)
                .HasForeignKey(d => d.ReligionId);

        }
    }
}
