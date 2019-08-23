using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.StoredProcedures.Models
{
   public class GlobalHeadmasterList
    {

        public int  InstituteId { get; set; }
          public string InstituteName { get; set; }
         public string  Name { get; set; }
		  public string ContactNumber1 { get; set; }
		  public string  ContactNumber2{ get;set;}
		 public string EmailAddress { get; set; }
		  public string DOB { get; set; }		  
		  public int DesignationId { get; set; }
         public string DesignationName { get; set; }
    }
}
