﻿using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{
    public interface ICertificatePrintService
    {
        CertificatePrint GetCertificatePrintById(int id, int instituteId);
        CertificatePrint AddNewCertificatePrint(int instituteId);
        void SaveCertificatePrint(IUnitOfWorkAsync unitOfWorkAsync, CertificatePrint certificateModel);
        void UpdateCertificatePrint(IUnitOfWorkAsync unitOfWorkAsync, CertificatePrint certificateModel);
        IEnumerable<CertificatePrint> GetAllCertificatePrint(int instituteId);
    }


    public class CertificatePrintService : Service<CertificatePrint>, ICertificatePrintService
    {
        private readonly IRepositoryAsync<CertificatePrint> _repository;
        private readonly ICertificatePrintTypeService _certificatePrintTypeService;


        public CertificatePrintService(IRepositoryAsync<CertificatePrint> repository, ICertificatePrintTypeService certificatePrintTypeService)
            : base(repository)
        {
            _repository = repository;
            _certificatePrintTypeService = certificatePrintTypeService;

        }

        public CertificatePrint AddNewCertificatePrint(int instituteId)
        {
            CertificatePrint certificatePrinttModel = new CertificatePrint();
            certificatePrinttModel.InstituteId = instituteId;
            certificatePrinttModel.CertificatePrintTypeList = _certificatePrintTypeService.GetKVP(instituteId);
            certificatePrinttModel.IsActive = true;
            return certificatePrinttModel;
        }

        public CertificatePrint GetCertificatePrintById(int id, int instituteId)
        {
            CertificatePrint certificatePrinttModel = _repository.Query(r=>r.Id==id).Include(x => x.Institute).Include(x=>x.CertificatePrintType).Select().SingleOrDefault();
            certificatePrinttModel.CertificatePrintTypeList = _certificatePrintTypeService.GetKVP(instituteId);
            return certificatePrinttModel;
        }

        public void SaveCertificatePrint(IUnitOfWorkAsync unitOfWorkAsync, CertificatePrint certificateModel)
        {         
            certificateModel.LastUpdateTime = DateTime.Now;
            _repository.Insert(certificateModel);
            unitOfWorkAsync.SaveChanges();

        }

        public void UpdateCertificatePrint(IUnitOfWorkAsync unitOfWorkAsync, CertificatePrint certificateModel)
        {
            CertificatePrint newEntity = new CertificatePrint();
            newEntity.Id = certificateModel.Id;
            newEntity.InstituteId = certificateModel.InstituteId;
            newEntity.IsActive = certificateModel.IsActive;
            newEntity.LastUpdateTime = DateTime.Now;
            newEntity.Body = certificateModel.Body;
            newEntity.CertificatePrintTypeId = certificateModel.CertificatePrintTypeId;
            _repository.Update(newEntity);
            unitOfWorkAsync.SaveChanges();

        }

        public IEnumerable<CertificatePrint> GetAllCertificatePrint(int instituteId) 
        {
            var certificatePrintList = _repository.Query(x => x.InstituteId == instituteId).Include(x=>x.CertificatePrintType).Select();
            return certificatePrintList;
        }

    }



}
