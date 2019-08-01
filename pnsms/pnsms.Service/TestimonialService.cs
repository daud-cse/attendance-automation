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
    public interface ITestimonialService
    {
        IEnumerable<Testimonial> GetAllTestimonial();
        IEnumerable<Testimonial> GetTestimonialByInstituteId(int InstituteId);
        List<Testimonial> GetActiveTestimonialByInstituteId(int instituteId);
        Testimonial CreateTestimonial(IUnitOfWorkAsync unitOfWorkAsync, Testimonial testimonial);
        Testimonial UpdateTestimonial(IUnitOfWorkAsync unitOfWorkAsync, Testimonial testimonial);
        Testimonial GetTestimonialById(int id);
    }
    public class TestimonialService : Service<Testimonial>, ITestimonialService
    {
        private readonly IRepositoryAsync<Testimonial> _repository;
        public TestimonialService(IRepositoryAsync<Testimonial> repository)
            : base(repository)
        {
            _repository = repository;

        }
        public IEnumerable<Testimonial> GetAllTestimonial()
        {
            var testimonial = _repository.Query().Select();
            return testimonial;
        }
        public IEnumerable<Testimonial> GetTestimonialByInstituteId(int InstituteId)
        {
            var testimonial = _repository.Query().Select().Where(x => x.InstituteId == InstituteId);
            return testimonial;
        }
        public List<Testimonial> GetActiveTestimonialByInstituteId(int InstituteId)
        {
            var testimonial = _repository.Query().Select().Where(x => x.InstituteId == InstituteId && x.IsActive == true);
            return testimonial.Count() <= 0 ? new List<Testimonial>() : testimonial.ToList();
        }

        public Testimonial GetTestimonialById(int id)
        {
            return _repository.Query(x => x.Id == id).Select().SingleOrDefault();
        }
        public Testimonial CreateTestimonial(IUnitOfWorkAsync unitOfWorkAsync, Testimonial testimonial)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                _repository.Insert(testimonial);
                unitOfWorkAsync.SaveChanges();

                unitOfWorkAsync.Commit();
            }
            catch
            {
                unitOfWorkAsync.Rollback();
            }
            return testimonial;
        }
        public Testimonial UpdateTestimonial(IUnitOfWorkAsync unitOfWorkAsync, Testimonial testimonial)
        {
            try
            {
                unitOfWorkAsync.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Unspecified);
                _repository.Update(testimonial);
                unitOfWorkAsync.SaveChanges();
                 
                unitOfWorkAsync.Commit(); 
            }
            catch
            {
                unitOfWorkAsync.Rollback();
                throw new Exception("Error");
            }
            return testimonial;
        }
    }
}
