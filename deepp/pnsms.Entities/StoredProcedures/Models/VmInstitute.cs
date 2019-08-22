using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.StoredProcedures.Models
{
  public  class VmInstitute
    {
        public int InstituteId { get; set; }
	     public string InstituteName { get; set; }
        public int GlobalCountryId { get; set; }

        public string  CountryName { get; set; }
        public int GlobalDivisionId { get; set; }

        public string GlobalDivisionName { get; set; }
        public int GlobalDistrictId { get; set; }

        public string GlobalDistrictName { get; set; }
        public int GlobalSubDistrictId { get; set; }
        public string GlobalSubDistrictName { get; set; }

        public string DomainName { get; set; }

    }
}
