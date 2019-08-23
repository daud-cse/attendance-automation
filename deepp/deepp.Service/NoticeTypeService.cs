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
   public interface INoticeTypeService
   {
       List<KeyValuePair<int, string>> GetKVP();
   }

   public class NoticeTypeService : Service<NoticeType>, INoticeTypeService
   {
       private readonly IRepositoryAsync<NoticeType> _redeeppitory;

       public NoticeTypeService(IRepositoryAsync<NoticeType> redeeppitory)
           : base(redeeppitory)
       {
           _redeeppitory = redeeppitory;

       }
       public List<KeyValuePair<int, string>> GetKVP()
       {
           var data = _redeeppitory.Query().Select().ToList();
           var classList = new List<KeyValuePair<int, string>>();
           data.ForEach(c => classList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

           return classList;
       }
   }
}
