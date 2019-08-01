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
   public interface INoticeTypeService
   {
       List<KeyValuePair<int, string>> GetKVP();
   }

   public class NoticeTypeService : Service<NoticeType>, INoticeTypeService
   {
       private readonly IRepositoryAsync<NoticeType> _repository;

       public NoticeTypeService(IRepositoryAsync<NoticeType> repository)
           : base(repository)
       {
           _repository = repository;

       }
       public List<KeyValuePair<int, string>> GetKVP()
       {
           var data = _repository.Query().Select().ToList();
           var classList = new List<KeyValuePair<int, string>>();
           data.ForEach(c => classList.Add(new KeyValuePair<int, string>(c.Id, c.Name)));

           return classList;
       }
   }
}
