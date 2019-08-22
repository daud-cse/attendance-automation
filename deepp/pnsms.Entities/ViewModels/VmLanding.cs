using pnsms.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Entities.ViewModels
{
   public class VmLanding
    {
       public Notice Notice { get; set; }
       public List<Notice> NoticeList { get; set; }
       public List<Testimonial> TestimonialList { get; set; }
       public Institute Institute { get; set; }
       public List<Image> Images { get; set; }
       public Image ImageLogo { get; set; }
       public Image ImageBanner { get; set; }

    }
}
