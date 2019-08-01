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
    public interface IPaymentTypeService
   {
       List<KeyValuePair<int, string>> GetKVP();
   }

    public class PaymentTypeService : Service<PaymentType>, IPaymentTypeService
   {
       private readonly IRepositoryAsync<PaymentType> _repository;

       public PaymentTypeService(IRepositoryAsync<PaymentType> repository)
           : base(repository)
       {
           _repository = repository;

       }
       public List<KeyValuePair<int, string>> GetKVP()
       {
           var data = _repository.Query(r=>r.IsActive).Select().ToList();
           var paymentList = new List<KeyValuePair<int, string>>();
           data.ForEach(c => paymentList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

           return paymentList;
       }
   }
}
