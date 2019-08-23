using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pnsms.Entities.ViewModels.DashBoard;

namespace pnsms.Service.DashBoard
{
    public class DashboardService
    {
        private readonly IInstituteService _instituteService;
        private readonly IStudentService _studentService;

        public DashboardService(IInstituteService instituteService,IStudentService studentService)
        {
            _instituteService = instituteService;
            _studentService = studentService;
        }

        public Dashboard GetDashboard()
        {
            var objDashboard = new Dashboard();
            
            return objDashboard;
        }
    }
}
