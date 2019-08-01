using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class InstituteMap : EntityTypeConfiguration<Institute>
    {
        public InstituteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Code)
                .HasMaxLength(128);

            this.Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.FacebookUrl)
                .HasMaxLength(256);

            this.Property(t => t.TwitterUrl)
                .HasMaxLength(256);

            this.Property(t => t.GoogleUrl)
                .HasMaxLength(256);

            this.Property(t => t.LinkedinUrl)
                .HasMaxLength(256);

            this.Property(t => t.BehanceUrl)
                .HasMaxLength(256);

            this.Property(t => t.Email)
                .HasMaxLength(128);

            this.Property(t => t.Contact)
                .HasMaxLength(128);

            this.Property(t => t.GoogleMapAddress)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Institutes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.PackageId).HasColumnName("PackageId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.latitude).HasColumnName("latitude");
            this.Property(t => t.longitude).HasColumnName("longitude");
            this.Property(t => t.SeoText).HasColumnName("SeoText");
            this.Property(t => t.WelComeText).HasColumnName("WelComeText");
            this.Property(t => t.ContactText).HasColumnName("ContactText");
            this.Property(t => t.FacebookUrl).HasColumnName("FacebookUrl");
            this.Property(t => t.TwitterUrl).HasColumnName("TwitterUrl");
            this.Property(t => t.GoogleUrl).HasColumnName("GoogleUrl");
            this.Property(t => t.LinkedinUrl).HasColumnName("LinkedinUrl");
            this.Property(t => t.BehanceUrl).HasColumnName("BehanceUrl");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.HistoryText).HasColumnName("HistoryText");
            this.Property(t => t.InfractructureText).HasColumnName("InfractructureText");
            this.Property(t => t.MasterPlanText).HasColumnName("MasterPlanText");
            this.Property(t => t.UsefulLinkText).HasColumnName("UsefulLinkText");
            this.Property(t => t.GlobalCountryId).HasColumnName("GlobalCountryId");
            this.Property(t => t.GlobalDivisionId).HasColumnName("GlobalDivisionId");
            this.Property(t => t.GlobalDistrictId).HasColumnName("GlobalDistrictId");
            this.Property(t => t.GlobalSubDistrictId).HasColumnName("GlobalSubDistrictId");
            this.Property(t => t.VisitorToday).HasColumnName("VisitorToday");
            this.Property(t => t.VisitorThisMonth).HasColumnName("VisitorThisMonth");
            this.Property(t => t.VisitorTotal).HasColumnName("VisitorTotal");
            this.Property(t => t.GlobalInstituteTypeId).HasColumnName("GlobalInstituteTypeId");
            this.Property(t => t.Asset).HasColumnName("Asset");
            this.Property(t => t.IncExp).HasColumnName("IncExp");
            this.Property(t => t.Sanitation).HasColumnName("Sanitation");
            this.Property(t => t.Multimedia).HasColumnName("Multimedia");
            this.Property(t => t.LibraryInfo).HasColumnName("LibraryInfo");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Contact).HasColumnName("Contact");
            this.Property(t => t.IsSMSCostPayByReceipient).HasColumnName("IsSMSCostPayByReceipient");
            this.Property(t => t.IsAuthenticationSMSCostPayByReceipient).HasColumnName("IsAuthenticationSMSCostPayByReceipient");
            this.Property(t => t.RestSMSCount).HasColumnName("RestSMSCount");
            this.Property(t => t.MenuHtml).HasColumnName("MenuHtml");
            this.Property(t => t.CssOverwrite).HasColumnName("CssOverwrite");
            this.Property(t => t.GoogleMapAddress).HasColumnName("GoogleMapAddress");

            // Relationships
            this.HasOptional(t => t.GlobalCountry)
                .WithMany(t => t.Institutes)
                .HasForeignKey(d => d.GlobalCountryId);
            this.HasOptional(t => t.GlobalDistrict)
                .WithMany(t => t.Institutes)
                .HasForeignKey(d => d.GlobalDistrictId);
            this.HasOptional(t => t.GlobalDivision)
                .WithMany(t => t.Institutes)
                .HasForeignKey(d => d.GlobalDivisionId);
            this.HasOptional(t => t.GlobalInstituteType)
                .WithMany(t => t.Institutes)
                .HasForeignKey(d => d.GlobalInstituteTypeId);
            this.HasOptional(t => t.GlobalSubDistrict)
                .WithMany(t => t.Institutes)
                .HasForeignKey(d => d.GlobalSubDistrictId);
            this.HasRequired(t => t.Package)
                .WithMany(t => t.Institutes)
                .HasForeignKey(d => d.PackageId);

        }
    }
}
