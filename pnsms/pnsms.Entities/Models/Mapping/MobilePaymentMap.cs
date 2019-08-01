using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class MobilePaymentMap : EntityTypeConfiguration<MobilePayment>
    {
        public MobilePaymentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.MobileNo)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.TransactionId)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MobilePayments");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.MobileNo).HasColumnName("MobileNo");
            this.Property(t => t.ReffStudentId).HasColumnName("ReffStudentId");
            this.Property(t => t.Payment).HasColumnName("Payment");
            this.Property(t => t.TransactionId).HasColumnName("TransactionId");
            this.Property(t => t.TransactionDate).HasColumnName("TransactionDate");
            this.Property(t => t.PaymentTypeId).HasColumnName("PaymentTypeId");
            this.Property(t => t.LastActionBy).HasColumnName("LastActionBy");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.MobilePayments)
                .HasForeignKey(d => d.InstituteId);
            this.HasRequired(t => t.PaymentType)
                .WithMany(t => t.MobilePayments)
                .HasForeignKey(d => d.PaymentTypeId);
            this.HasOptional(t => t.Student)
                .WithMany(t => t.MobilePayments)
                .HasForeignKey(d => d.ReffStudentId);

        }
    }
}
