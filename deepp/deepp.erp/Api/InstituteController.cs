using deepp.erp;
using deepp.Entities.Models;
using deepp.erp.Attributes;
using deepp.Service;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace deepp.erp.Api
{
    public class InstituteController : ApiController
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
        public IEnumerable<Institute> Get(bool IsActive = false)
        {
            return _instituteService.GetInstitutes(IsActive);
        }


        // GET api/academicbranch/5
        public Institute Get(int id)
        {
            return _instituteService.GetInstituteById(id);
        }
        [Route("api/institute/current")]
        public Institute GetCurrentInstitute(string infoText = "")
        {
            var institute = _instituteService.GetInstituteById(Sessions.InstituteId);
           
            if (("WelComeText").ToUpper().Equals(infoText.ToUpper()))
            {
                institute.ContactText = "";
                institute.MasterPlanText = "";
                institute.HistoryText = "";
                institute.InfractructureText = "";
                institute.UsefulLinkText = "";
                institute.Asset = "";
                institute.IncExp = "";
                institute.LibraryInfo = "";
                institute.Multimedia = "";
                institute.Sanitation = "";
            }
            else if (("ContactText").ToUpper().Equals(infoText.ToUpper()))
            {

                institute.WelComeText = "";
                institute.MasterPlanText = "";
                institute.HistoryText = "";
                institute.InfractructureText = "";
                institute.UsefulLinkText = "";
                institute.Asset = "";
                institute.IncExp = "";
                institute.LibraryInfo = "";
                institute.Multimedia = "";
                institute.Sanitation = "";
            }
            else if (("MasterPlanText").ToUpper().Equals(infoText.ToUpper()))
            {

                institute.WelComeText = "";
                institute.ContactText = "";
                institute.HistoryText = "";
                institute.InfractructureText = "";
                institute.UsefulLinkText = "";
                institute.Asset = "";
                institute.IncExp = "";
                institute.LibraryInfo = "";
                institute.Multimedia = "";
                institute.Sanitation = "";
            }
            else if (("HistoryText").ToUpper().Equals(infoText.ToUpper()))
            {

                institute.WelComeText = "";
                institute.ContactText = "";
                institute.MasterPlanText = "";

                institute.InfractructureText = "";
                institute.UsefulLinkText = "";
                institute.Asset = "";
                institute.IncExp = "";
                institute.LibraryInfo = "";
                institute.Multimedia = "";
                institute.Sanitation = "";
            }
            else if (("InfractructureText").ToUpper().Equals(infoText.ToUpper()))
            {

                institute.WelComeText = "";
                institute.ContactText = "";
                institute.MasterPlanText = "";
                institute.HistoryText = "";

                institute.UsefulLinkText = "";
                institute.Asset = "";
                institute.IncExp = "";
                institute.LibraryInfo = "";
                institute.Multimedia = "";
                institute.Sanitation = "";
            }
            else if (("UsefulLinkText").ToUpper().Equals(infoText.ToUpper()))
            {

                institute.WelComeText = "";
                institute.ContactText = "";
                institute.MasterPlanText = "";
                institute.HistoryText = "";
                institute.InfractructureText = "";

                institute.Asset = "";
                institute.IncExp = "";
                institute.LibraryInfo = "";
                institute.Multimedia = "";
                institute.Sanitation = "";
            }
            else if (("Asset").ToUpper().Equals(infoText.ToUpper()))
            {

                institute.WelComeText = "";
                institute.ContactText = "";
                institute.MasterPlanText = "";
                institute.HistoryText = "";
                institute.InfractructureText = "";
                institute.UsefulLinkText = "";

                institute.IncExp = "";
                institute.LibraryInfo = "";
                institute.Multimedia = "";
                institute.Sanitation = "";
            }
            else if (("IncExp").ToUpper().Equals(infoText.ToUpper()))
            {

                institute.WelComeText = "";
                institute.ContactText = "";
                institute.MasterPlanText = "";
                institute.HistoryText = "";
                institute.InfractructureText = "";
                institute.UsefulLinkText = "";
                institute.Asset = "";

                institute.LibraryInfo = "";
                institute.Multimedia = "";
                institute.Sanitation = "";
            }
            else if (("LibraryInfo").ToUpper().Equals(infoText.ToUpper()))
            {

                institute.WelComeText = "";
                institute.ContactText = "";
                institute.MasterPlanText = "";
                institute.HistoryText = "";
                institute.InfractructureText = "";
                institute.UsefulLinkText = "";
                institute.Asset = "";
                institute.IncExp = "";

                institute.Multimedia = "";
                institute.Sanitation = "";
            }
            else if (("Multimedia").ToUpper().Equals(infoText.ToUpper()))
            {

                institute.WelComeText = "";
                institute.ContactText = "";
                institute.MasterPlanText = "";
                institute.HistoryText = "";
                institute.InfractructureText = "";
                institute.UsefulLinkText = "";
                institute.Asset = "";
                institute.IncExp = "";
                institute.LibraryInfo = "";

                institute.Sanitation = "";
            }
            else if (("Sanitation").ToUpper().Equals(infoText.ToUpper()))
            {

                institute.WelComeText = "";
                institute.ContactText = "";
                institute.MasterPlanText = "";
                institute.HistoryText = "";
                institute.InfractructureText = "";
                institute.UsefulLinkText = "";
                institute.Asset = "";
                institute.IncExp = "";
                institute.LibraryInfo = "";
                institute.Multimedia = "";

            }

            SetListValue(institute);

            if (String.IsNullOrWhiteSpace(infoText))
            {
                SetInfoTextValue(institute);
            }
            return institute;
        }

        /// <summary>
        /// Sets the list value.
        /// </summary>
        /// <param name="institute">The institute.</param>
        private static void SetListValue(Institute institute)
        {
            //institute.ImagesList = null;
            institute.PackageList = null;
            institute.GlobalCountryList = null;
            institute.GlobalDivisionList = null;
            institute.GlobalDistrictList = null;
            institute.GlobalSubDistrictList = null;
            institute.GlobalInstituteTypeList = null;
            //GetValue(institute);
            institute.GlobalCountry = null;
            institute.GlobalDivision = null;
            institute.GlobalDistrict = null;
            institute.GlobalDivision = null;
            institute.GlobalInstituteType = null;
            institute.Package = null;

        }

        /// <summary>
        /// Sets the information text value.
        /// </summary>
        /// <param name="institute">The institute.</param>
        private static void SetInfoTextValue(Institute institute)
        {
            institute.WelComeText = "";
            institute.ContactText = "";
            institute.MasterPlanText = "";
            institute.HistoryText = "";
            institute.InfractructureText = "";
            institute.UsefulLinkText = "";
            institute.Asset = "";
            institute.Sanitation = "";
            institute.IncExp = "";
            institute.LibraryInfo = "";
            institute.Multimedia = "";

        }
        // POST api/academicbranch
        [Validate]
        public HttpResponseMessage Post([FromBody]Institute institute)
        {
            var images = new List<byte[]>();
            if (Sessions.Temp != null)
            {
                images = (List<byte[]>)Sessions.Temp;
                Sessions.Temp = null;
            }
            institute.LastUpdateTime = DateTime.Now;
            _instituteService.CreateInstitute(_unitOfWorkAsync, institute, images);

            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new JsonContent(new
                {
                    Id = institute.Id,
                    Message = "Success"
                })
            };
            return new HttpResponseMessage(HttpStatusCode.Created);

        }

        // PUT api/Institute/5
        public void Put(int id, [FromBody]Institute institute)
        {
            var images = new List<byte[]>();
            if (Sessions.Temp != null)
            {
                images = (List<byte[]>)Sessions.Temp;
                Sessions.Temp = null;
            }
            institute.LastUpdateTime = DateTime.Now;
            _instituteService.UpdateInstitute(_unitOfWorkAsync, institute, images);

        }
        [Route("api/Institute/Current")]
        [HttpPut]
        public void PutCurrent(int id, [FromBody]Institute institute)
        {
            var images = new List<byte[]>();
            if (Sessions.Temp != null)
            {
                images = (List<byte[]>)Sessions.Temp;
                Sessions.Temp = null;
            }
            var objInstitute = _instituteService.GetInstituteById(Sessions.InstituteId);

            objInstitute.Name = institute.Name;
            objInstitute.Code = institute.Code;
            objInstitute.latitude = institute.latitude;
            objInstitute.longitude = institute.longitude;
            objInstitute.SeoText = institute.SeoText;
            objInstitute.FacebookUrl = institute.FacebookUrl;
            objInstitute.TwitterUrl = institute.TwitterUrl;
            objInstitute.GoogleUrl = institute.GoogleUrl;
            objInstitute.LinkedinUrl = institute.LinkedinUrl;
            objInstitute.BehanceUrl = institute.BehanceUrl;
            objInstitute.Email = institute.Email;
            objInstitute.Contact = institute.Contact;
            objInstitute.GoogleMapAddress = institute.GoogleMapAddress;

            institute.LastUpdateTime = DateTime.Now;
            _instituteService.UpdateInstitute(_unitOfWorkAsync, objInstitute, images);

        }

        [Route("api/Institute/infotext")]
        public void PutInfotext(int id, string infoText, [FromBody]Institute institute)
        {
            var objInstitute = _instituteService.GetInstituteById(Sessions.InstituteId);
            if (("WelComeText").ToUpper().Equals(infoText.ToUpper()))
            {
                objInstitute.WelComeText = institute.WelComeText;
            }
            else if (("ContactText").ToUpper().Equals(infoText.ToUpper()))
            {
                objInstitute.ContactText = institute.ContactText;
            }
            else if (("MasterPlanText").ToUpper().Equals(infoText.ToUpper()))
            {
                objInstitute.MasterPlanText = institute.MasterPlanText;

            }
            else if (("HistoryText").ToUpper().Equals(infoText.ToUpper()))
            {
                objInstitute.HistoryText = institute.HistoryText;

            }
            else if (("InfractructureText").ToUpper().Equals(infoText.ToUpper()))
            {
                objInstitute.InfractructureText = institute.InfractructureText;

            }
            else if (("UsefulLinkText").ToUpper().Equals(infoText.ToUpper()))
            {
                objInstitute.UsefulLinkText = institute.UsefulLinkText;

            }
            else if (("Asset").ToUpper().Equals(infoText.ToUpper()))
            {
                objInstitute.Asset = institute.Asset;
            }
            else if (("IncExp").ToUpper().Equals(infoText.ToUpper()))
            {
                objInstitute.IncExp = institute.IncExp;
            }
            else if (("LibraryInfo").ToUpper().Equals(infoText.ToUpper()))
            {
                objInstitute.LibraryInfo = institute.LibraryInfo;

            }
            else if (("Multimedia").ToUpper().Equals(infoText.ToUpper()))
            {
                objInstitute.Multimedia = institute.Multimedia;

            }
            else if (("Sanitation").ToUpper().Equals(infoText.ToUpper()))
            {
                objInstitute.Sanitation = institute.Sanitation;


            }
            institute.LastUpdateTime = DateTime.Now;
            _instituteService.UpdateInstitute(_unitOfWorkAsync, objInstitute, null);

        }

        [Route("api/institute/UpdateSlider")]
        public void PutSliderImage(int id, [FromBody]Institute institute)
        {
            var imageList = new List<Image>();
            if (Sessions.Temp != null)
            {
                var images = (List<byte[]>)Sessions.Temp;
                if (images != null)
                {
                    imageList.AddRange(images.Select(image => new Entities.Models.Image() { RefTypeId = (int)utility.RefCode.Institute_Landing_Slider, ImageBinaryData = image }));
                }

                Sessions.Temp = null;
            }
            var objInstitute = _instituteService.GetInstituteById(Sessions.InstituteId);

            objInstitute.Name = institute.Name;
            objInstitute.Code = institute.Code;
            objInstitute.latitude = institute.latitude;
            objInstitute.longitude = institute.longitude;
            objInstitute.SeoText = institute.SeoText;
            objInstitute.FacebookUrl = institute.FacebookUrl;
            objInstitute.TwitterUrl = institute.TwitterUrl;
            objInstitute.GoogleUrl = institute.GoogleUrl;
            objInstitute.LinkedinUrl = institute.LinkedinUrl;
            objInstitute.BehanceUrl = institute.BehanceUrl;
            objInstitute.Email = institute.Email;
            objInstitute.Contact = institute.Contact;
            objInstitute.ImagesList = institute.ImagesList;
            objInstitute.GoogleMapAddress = institute.GoogleMapAddress;

            institute.LastUpdateTime = DateTime.Now;
            _instituteService.UpdateInstitute(_unitOfWorkAsync, objInstitute, null, imageList);

        }

        public void Delete(int id)
        {
        }
        [Route("api/institute/new")]
        public Institute GetNewInstitute()
        {
            return _instituteService.GetNewInstitute();
        }
        protected override void Dispose(bool disdeepping)
        {
            if (disdeepping)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disdeepping);
        }

    }
}
