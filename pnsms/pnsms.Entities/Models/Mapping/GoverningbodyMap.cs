using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class GoverningbodyMap : EntityTypeConfiguration<Governingbody>
    {
        public GoverningbodyMap()
        {
            // Primary Key
            this.HasKey(t => t.GoverningbodyId);

            // Properties
            this.Property(t => t.GoverningbodyId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FatherName)
                .HasMaxLength(128);

            this.Property(t => t.MotherName)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Governingbodies");
            this.Property(t => t.GoverningbodyId).HasColumnName("GoverningbodyId");
            this.Property(t => t.FatherName).HasColumnName("FatherName");
            this.Property(t => t.MotherName).HasColumnName("MotherName");

            // Relationships
            this.HasRequired(t => t.UserInfo)
                .WithOptional(t => t.Governingbody);

        }
    }
}
