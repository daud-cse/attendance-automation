using deepp.Entities.Models;
using deepp.Entities.StoredProcedures.Models;
using deepp.Service.DashBoard;
using deepp.Service.Institutes;
using deepp.utility;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
{


    public interface IInstituteService : IService<Institute>
    {
        IEnumerable<Institute> GetInstitutes();
        IEnumerable<Institute> GetInstitutes(bool isActive);
        IEnumerable<Institute> GetActiveInstitute();
        Institute GetInstituteById(int id);
        Institute GetActiveInstituteById(int id);
        
        Institute Get(string url);
        Institute GetNewInstitute();
        Institute CreateInstitute(IUnitOfWorkAsync unitOfWorkAsync, Institute institute, List<byte[]> images);
        Institute UpdateInstitute(IUnitOfWorkAsync unitOfWorkAsync, Institute institute, List<byte[]> logoBannarimage, List<Image> images = null);
        List<KeyValuePair<int, string>> GetKVP();

        List<VmInstitute> GetInstituteListForGlobalUser(int UserId, int UserInfoTypeId);
    }
    public class InstituteService : Service<Institute>, IInstituteService
    {


        private readonly IRepositoryAsync<Institute> _redeeppitory;
        private readonly IPackageService _iPackageService;
        private readonly IImageService _imageService;
        private readonly IGlobalCountryService _globalCountryService;
        private readonly IGlobalDistrictService _globalDistrictService;
        private readonly IGlobalDivisionService _globalDivisionService;
        private readonly IGlobalInstituteTypeService _globalInstituteTypeService;
        private readonly IGlobalSubDistrictService _globalSubDistrictService;
        private readonly IDashboardService _dashboardService;

        public InstituteService(IRepositoryAsync<Institute> redeeppitory, IPackageService iPackageService, IImageService imageService,
            IGlobalCountryService globalCountryService,
            IGlobalDistrictService globalDistrictService,
            IGlobalDivisionService globalDivisionService,
            IGlobalInstituteTypeService globalInstituteTypeService,
            IGlobalSubDistrictService globalSubDistrictService,
            IDashboardService dashboardService
            )
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
            _iPackageService = iPackageService;
            _imageService = imageService;
            _globalCountryService = globalCountryService;
            _globalDistrictService = globalDistrictService;
            _globalDivisionService = globalDivisionService;
            _globalInstituteTypeService = globalInstituteTypeService;
            _globalSubDistrictService = globalSubDistrictService;
            _dashboardService = dashboardService;
        }


        public IEnumerable<Institute> GetInstitutes()
        {

            return _redeeppitory.Query().Select();
        }

        public IEnumerable<Institute> GetInstitutes(bool isActive)
        {
            if (isActive)
            {
                return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
            }

            return _redeeppitory.Query().Select();
        }
        public List<VmInstitute> GetInstituteListForGlobalUser(int UserId, int UserInfoTypeId)
        {
            var dashboard = _dashboardService.GetDashboard(UserId, UserInfoTypeId);
            //var instituteList = new List<KeyValuePair<int, string>>();
            //dashboard.lstInstitute.ForEach(c => instituteList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));
            return dashboard.lstInstitute;
        }
        public IEnumerable<Institute> GetActiveInstitute()
        {
            return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        }
        public Institute GetInstituteById(int id)
        {
            var institute = _redeeppitory.Query(x => x.Id == id).Select().FirstOrDefault();

            if (institute == null)
                return null;
            // get slider Image
            institute.ImagesList = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)RefCode.Institute_Landing_Slider, id).ToList();
            institute.ImagesList.ForEach(i => i.ImageBinaryData = null);
            institute.PackageList =
                _iPackageService.GetPackages().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToList();
            institute.GlobalCountryList =
                _globalCountryService.GetGlobalCountries().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToList();
            institute.GlobalDistrictList =
                _globalDistrictService.GetGlobalDistricts().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToList();
            institute.GlobalDivisionList =
                _globalDivisionService.GetGlobalDivisions().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToList();
            institute.GlobalInstituteTypeList =
              _globalInstituteTypeService.GetGlobalInstituteTypes().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToList();
            institute.GlobalSubDistrictList =
                 _globalSubDistrictService.GetGlobalSubDistricts().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToList();
            
            return institute;
        }
        public Institute GetNewInstitute()
        {
            var institute = new Institute();
            institute.PackageList =
                _iPackageService.GetPackages().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToList();
            institute.GlobalCountryList =
                _globalCountryService.GetGlobalCountries().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToList();
            institute.GlobalDistrictList =
                _globalDistrictService.GetGlobalDistricts().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToList();
            institute.GlobalDivisionList =
                _globalDivisionService.GetGlobalDivisions().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToList();
            institute.GlobalInstituteTypeList =
              _globalInstituteTypeService.GetGlobalInstituteTypes().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToList();
            institute.GlobalSubDistrictList =
                 _globalSubDistrictService.GetGlobalSubDistricts().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToList();
            return institute;
        }
        public Institute GetActiveInstituteById(int id)
        {
            return _redeeppitory.Query().Include(x => x.AcademicBranches).Select().FirstOrDefault(x => x.Id == id && x.IsActive == true);
        }
        public Institute GetActiveInstituteByIdForApi(int id)
        {
            return _redeeppitory.Query(x => x.Id == id && x.IsActive == true).Select().FirstOrDefault();
        }
        public Institute Get(string url)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Url == url);
        }
        public List<KeyValuePair<int, string>> GetKVP()
        {
            var data = _redeeppitory.Query(c => c.GlobalDistrictId==16).Select().ToList();

            var instituteList = new List<KeyValuePair<int, string>>();
            data.ForEach(c => instituteList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

            return instituteList;
        }
        public Institute CreateInstitute(IUnitOfWorkAsync unitOfWorkAsync, Institute institute, List<byte[]> images)
        {
            try
            {
                institute.LastUpdateTime = DateTime.Now;
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                _redeeppitory.Insert(institute);
                unitOfWorkAsync.SaveChanges();
                unitOfWorkAsync.Commit();
            }
            catch
            {
                unitOfWorkAsync.Rollback();
            }
            return institute;
        }
        public Institute UpdateInstitute(IUnitOfWorkAsync unitOfWorkAsync, Institute institute, List<byte[]> logoBannarimage, List<Image> images = null)
        {

            try
            {
                institute.LastUpdateTime = DateTime.Now;
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                _redeeppitory.Update(institute);
                unitOfWorkAsync.SaveChanges();
                // logo and bannar
                if (logoBannarimage != null && logoBannarimage.Any())
                {

                    if (logoBannarimage[0] != null)
                    {
                        var logoImage = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)utility.RefCode.Institute_Logo, institute.Id).FirstOrDefault();
                        if (logoImage != null)
                        {
                            logoImage.ImageBinaryData = logoBannarimage[0];
                            _imageService.Update(logoImage);
                        }
                        else
                        {
                            logoImage = new Image { RefPrimaryKey = institute.Id, RefTypeId = (int)utility.RefCode.Institute_Logo, ImageBinaryData = logoBannarimage[0] };
                            _imageService.Insert(logoImage);
                        }


                    }
                    if (logoBannarimage[1] != null)
                    {
                        var bannerImage = _imageService.GetImageByRefTypeIdAndRefPrimaryKey((int)utility.RefCode.Institute_Banner, institute.Id).FirstOrDefault();
                        if (bannerImage != null)
                        {
                            bannerImage.ImageBinaryData = logoBannarimage[1];
                            _imageService.Update(bannerImage);
                        }
                        else
                        {
                            bannerImage = new Image { RefPrimaryKey = institute.Id, RefTypeId = (int)utility.RefCode.Institute_Banner, ImageBinaryData = logoBannarimage[1] };
                            _imageService.Insert(bannerImage);
                        }
                    }

                    unitOfWorkAsync.SaveChanges();
                }

                // existing Institute_Landing_Slider images
                var imagesOld = _imageService.GetImageByRefTypeIdAndRefPrimaryKey(((int)RefCode.Institute_Landing_Slider), institute.Id);
                if (institute.ImagesList != null)
                {
                    foreach (var imgold in imagesOld)
                    {
                        var gimg = institute.ImagesList.FirstOrDefault(s => s.Id == imgold.Id);
                        if (gimg != null)
                        {
                            imgold.ImageCaption = gimg.ImageCaption;
                            _imageService.Update(imgold);
                        }
                        else
                        {
                            _imageService.Delete(imgold);
                        }

                    }

                    unitOfWorkAsync.SaveChanges();
                }
                // new gallery images
                if (images != null)
                {
                    foreach (var image in images)
                    {
                        image.RefPrimaryKey = institute.Id;
                        _imageService.Insert(image);
                        unitOfWorkAsync.SaveChanges();
                    }

                }

                unitOfWorkAsync.Commit();
                return institute;
            }
            catch (Exception exp)
            {
                unitOfWorkAsync.Rollback();
                throw new Exception(exp.Message);
            }

        }
    }
}
