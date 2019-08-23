using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class TestimonialMap : EntityTypeConfiguration<Testimonial>
    {
        public TestimonialMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TestimonialBody)
                .IsRequired();

            this.Property(t => t.TestimonialBy)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Testimonials");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.TestimonialBody).HasColumnName("TestimonialBody");
            this.Property(t => t.TestimonialBy).HasColumnName("TestimonialBy");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.Testimonials)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
