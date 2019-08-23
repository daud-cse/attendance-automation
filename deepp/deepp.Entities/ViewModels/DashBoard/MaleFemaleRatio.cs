using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ViewModels.DashBoard
{
   public class cRatio
    {
       public List<KeyValuePair<string, decimal>> Propertices { get; set; }
       /// <summary>
       /// X- axis value
       /// </summary>
       public string Name { get; set; }
       /// <summary>
       /// Y-Axis value
       /// </summary>
       public int Count { get; set; }
      
    }
}
