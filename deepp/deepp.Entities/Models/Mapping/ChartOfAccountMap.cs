using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class ChartOfAccountMap : EntityTypeConfiguration<ChartOfAccount>
    {
        public ChartOfAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Code)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ChartOfAccounts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.IsAsset).HasColumnName("IsAsset");
            this.Property(t => t.IsLiabilities).HasColumnName("IsLiabilities");
            this.Property(t => t.IsIncome).HasColumnName("IsIncome");
            this.Property(t => t.IsExpense).HasColumnName("IsExpense");
            this.Property(t => t.IsCapital).HasColumnName("IsCapital");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasOptional(t => t.ChartOfAccount1)
                .WithMany(t => t.ChartOfAccounts1)
                .HasForeignKey(d => d.ParentId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.ChartOfAccounts)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
