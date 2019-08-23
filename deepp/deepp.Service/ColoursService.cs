using deepp.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Service
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


        private readonly IRepositoryAsync<Colour> _redeeppitory;


        public ColourService(IRepositoryAsync<Colour> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;
        }


        public IEnumerable<Colour> GetColours()
        {

            return _redeeppitory.Query().Select();
        }

        //public IEnumerable<Colour> GetColours(bool isActive)
        //{
        //    if (isActive)
        //    {
        //        return _redeeppitory.Query().Select().Where(d => d.IsActive.Equals(true));
        //    }

        //    return _redeeppitory.Query().Select();
        //}

        //public IEnumerable<Colour> GetActiveColour()
        //{
        //    return _redeeppitory.Query().Select().Where(d => d.IsActive == true);
        //}
        public Colour GetColourById(int id)
        {
            return _redeeppitory.Query().Select().FirstOrDefault(x => x.Id == id);
        }
    }

}
