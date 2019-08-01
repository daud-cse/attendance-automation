using pnsms.Entities.Models;
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
    public interface IFeesGenerateAcademicService : IService<FeesGenerateAcademic>
    {
       
    }
    public class FeesGenerateAcademicService : Service<FeesGenerateAcademic>, IFeesGenerateAcademicService
    {
        private readonly IRepositoryAsync<FeesGenerateAcademic> _repository;

        public FeesGenerateAcademicService(IRepositoryAsync<FeesGenerateAcademic> repository)
            : base(repository)
        {
            _repository = repository;

        }

    }
}
