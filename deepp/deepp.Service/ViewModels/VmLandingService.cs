using deepp.Entities.Models;
using deepp.Entities.ViewModels;
using deepp.utility;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service.ViewModels
{
    public interface IVmLandingService
    {
        VmLanding GetAllVmLanding(int instituteId);
        VmLanding GetAllVmLanding(string url);

    }
    public class VmLandingService :IVmLandingService
    {
        private readonly INoticeService _noticeServic;
        private readonly ITestimonialService _testimonialService;
        private readonly IInstituteService _instituteService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IImageService _IImageService;
        public VmLandingService(IUnitOfWorkAsync unitOfWorkAsync,INoticeService noticeServic, ITestimonialService testimonialService
             , IInstituteService instituteService, IImageService IImageService
             )          
        {
             _unitOfWorkAsync = unitOfWorkAsync;
            _noticeServic = noticeServic;
            _testimonialService = testimonialService;
            _instituteService = instituteService;
            _IImageService = IImageService;
          
        }

        public VmLanding GetAllVmLanding(int instituteId) {

            VmLanding vmLanding = new VmLanding();
            vmLanding.Institute = _instituteService.GetActiveInstituteById(instituteId);
            vmLanding.ImageLogo = _IImageService.GetImageByRefTypeIdAndRefPrimaryKey((int)RefCode.Institute_Logo, instituteId).SingleOrDefault();
            vmLanding.ImageLogo = vmLanding.ImageLogo == null ? new Image() : vmLanding.ImageLogo;

            vmLanding.ImageBanner = _IImageService.GetImageByRefTypeIdAndRefPrimaryKey((int)RefCode.Institute_Banner, instituteId).SingleOrDefault();
            vmLanding.ImageBanner = vmLanding.ImageBanner == null ? new Image() : vmLanding.ImageBanner;

            vmLanding.TestimonialList = _testimonialService.GetActiveTestimonialByInstituteId(instituteId);
            vmLanding.Images = _IImageService.GetImageByRefTypeIdAndRefPrimaryKey((int)RefCode.Institute_Landing_Slider,instituteId).ToList();
            vmLanding.NoticeList = _noticeServic.GetActiveNotice(instituteId).ToList();
            
            return vmLanding;
        }

        public VmLanding GetAllVmLanding(string url)
        {
            VmLanding vmLanding = new VmLanding();
            var institute = _instituteService.Get(url);

            return institute == null ? null : GetAllVmLanding(institute.Id);
        }
    }
}
