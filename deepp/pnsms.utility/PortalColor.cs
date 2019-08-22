using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.utility
{
    public class PortalColor
    {
        public int Id { get; set; }
        public string Name { get; set; }




    }

    public class PortalUtility
    {
        public static string PortalColorTiles(int id)
        {
            var colorId = id % 12;
            return getPortalColor().Find(x => x.Id == colorId).Name;
        }

        private static List<PortalColor> getPortalColor()
        {
            var colors = new List<PortalColor>
                         {
                             new PortalColor
                             {
                                 Id = 1,
                                 Name = "sky"
                             },
                             new PortalColor
                             {
                                 Id = 2,
                                 Name = "orange"
                             },
                             new PortalColor
                             {
                                 Id = 3,
                                 Name = "brown"
                             },
                             new PortalColor
                             {
                                 Id = 4,
                                 Name = "midnightblue"
                             },
                             new PortalColor
                             {
                                 Id = 5,
                                 Name = "purple"
                             },
                             new PortalColor
                             {
                                 Id = 6,
                                 Name = "success"
                             },
                             new PortalColor
                             {
                                 Id = 7,
                                 Name = "primary"
                             },
                             new PortalColor
                             {
                                 Id = 8,
                                 Name = "indigo"
                             },
                             new PortalColor
                             {
                                 Id = 9,
                                 Name = "green"
                             },

                             new PortalColor
                             {
                                 Id = 10,
                                 Name = "danger"
                             },
                             new PortalColor
                             {
                                 Id = 11,
                                 Name = "magenta"
                             },
                             new PortalColor
                             {
                                 Id = 0,
                                 Name = "inverse"
                             }
                         };
            return colors;
        }

        public static string StudentTemplate = "<li class='hasChild'><a href='javascript:;'><i class='fa fa-th'></i> <span>[studentname]</span> </a><ul class='acc-menu' style='display: block;'><li><a href='/Student/index?studentId=[studentid]'><i class='fa fa-briefcase'></i> <span>Profile</span></a></li><li class='active'><a href='/Payment/index?studentId=[studentid]'><i class='fa fa-briefcase'></i> <span>Payment</span></a></li></ul></li>";

    }


}
