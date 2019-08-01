using pnsms.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.Service
{
    
    public interface IColourService : IService<Colour>
    {
        IEnumerable<Colour> GetColours();
        //IEnumerable<Colour> GetActiveColour();
        Colour GetColourById(int id);
        //IEnumerable<Colour> GetColours(bool isActive);
    }
    public class ColourService : Service<Colour>, IColourService
    {


        private readonly IRepositoryAsync<Colour> _repository;


        public ColourService(IRepositoryAsync<Colour> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public IEnumerable<Colour> GetColours()
        {

            return _repository.Query().Select();
        }

        //public IEnumerable<Colour> GetColours(bool isActive)
        //{
        //    if (isActive)
        //    {
        //        return _repository.Query().Select().Where(d => d.IsActive.Equals(true));
        //    }

        //    return _repository.Query().Select();
        //}

        //public IEnumerable<Colour> GetActiveColour()
        //{
        //    return _repository.Query().Select().Where(d => d.IsActive == true);
        //}
        public Colour GetColourById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }
    }

}
