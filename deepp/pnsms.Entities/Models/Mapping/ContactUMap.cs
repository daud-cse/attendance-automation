using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class ContactUMap : EntityTypeConfiguration<ContactU>
    {
        public ContactUMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(128);

            this.Property(t => t.Email)
                .HasMaxLength(128);

            this.Property(t => t.Subject)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("ContactUs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.ContactUs)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
