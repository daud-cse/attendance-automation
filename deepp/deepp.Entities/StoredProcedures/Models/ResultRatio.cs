using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.StoredProcedures.Models
{
    public class ResultRatio
    {
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public int Total { get; set; }
        public decimal FemalePercentage { get; set; }
        public decimal MalePercentage { get; set; }
    }
}
