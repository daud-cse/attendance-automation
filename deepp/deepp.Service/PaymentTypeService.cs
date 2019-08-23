using deepp.Entities.Models;
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
    public interface IPaymentTypeService
   {
       List<KeyValuePair<int, string>> GetKVP();
   }

    public class PaymentTypeService : Service<PaymentType>, IPaymentTypeService
   {
       private readonly IRepositoryAsync<PaymentType> _redeeppitory;

       public PaymentTypeService(IRepositoryAsync<PaymentType> redeeppitory)
           : base(redeeppitory)
       {
           _redeeppitory = redeeppitory;

       }
       public List<KeyValuePair<int, string>> GetKVP()
       {
           var data = _redeeppitory.Query(r=>r.IsActive).Select().ToList();
           var paymentList = new List<KeyValuePair<int, string>>();
           data.ForEach(c => paymentList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

           return paymentList;
       }
   }
}
