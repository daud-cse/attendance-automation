using pnsms.Entities.Models;
using pnsms.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using pnsms.Entities.StoredProcedures.Models;

namespace pnsms.portal.Controllers
{
    public class InstituteController : Controller
    {
        private readonly IInstituteService _instituteService;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        public InstituteController(IInstituteService instituteService
            , IUnitOfWorkAsync unitOfWorkAsync)
        {
            _instituteService = instituteService;
            _unitOfWorkAsync = unitOfWorkAsync;

        }
        // GET api/academicbranch
        public ActionResult GetInstituteList(string searchItem, int? page, int? id)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            const int defaultPageSize = 15;
            IList<VmInstitute> InstituteList = new List<VmInstitute>();
            var lstInstitute= _instituteService.GetInstituteListForGlobalUser(Sessions.UserId, Sessions.UserInfoTypeId);
            InstituteList = (IList<VmInstitute>)lstInstitute.ToList();
            if (string.IsNullOrWhiteSpace(searchItem))
            {
                InstituteList = lstInstitute.ToPagedList(currentPageIndex, defaultPageSize);
            }
            else
            {
                InstituteList = lstInstitute.Where(p => p.InstituteName.ToLower() == searchItem.ToLower()                    
                     || Convert.ToString(p.InstituteName) == searchItem.ToLower()
                     || Convert.ToString(p.GlobalDivisionName==null?"": p.GlobalDivisionName).ToLower() == searchItem.ToLower()
                     || Convert.ToString(p.GlobalDistrictName==null?"": p.GlobalDistrictName).ToLower() == searchItem.ToLower()
                     || Convert.ToString(p.GlobalSubDistrictName==null?"": p.GlobalSubDistrictName).ToLower() == searchItem.ToLower()
                    ).ToPagedList(currentPageIndex, defaultPageSize);
            }
            return View(InstituteList);
        }


        
    }
}