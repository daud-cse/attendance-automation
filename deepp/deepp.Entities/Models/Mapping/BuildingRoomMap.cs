using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class BuildingRoomMap : EntityTypeConfiguration<BuildingRoom>
    {
        public BuildingRoomMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Description)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("BuildingRooms");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.BuildingId).HasColumnName("BuildingId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.Building)
                .WithMany(t => t.BuildingRooms)
                .HasForeignKey(d => d.BuildingId);

        }
    }
}
