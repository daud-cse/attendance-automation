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

    public interface IImageService:IService<Image>
    {
        IEnumerable<Image> GetAllImage();
        IEnumerable<Image> GetImageByRefcode(int RefTypeId);
        IEnumerable<Image> GetImageByRefTypeIdAndRefPrimaryKey(int RefTypeId, int RefPrimaryKey);
        Image GetImageById(int Id);
        void DeleteImage(int Id);
    }
    public class ImageService : Service<Image>, IImageService
    {
        private readonly IRepositoryAsync<Image> _redeeppitory;

        public ImageService(IRepositoryAsync<Image> redeeppitory)
            : base(redeeppitory)
        {
            _redeeppitory = redeeppitory;

        }

        public IEnumerable<Image> GetAllImage()
        {
            var image = _redeeppitory.Query().Select();
            return image;
        }
        public Image GetImageById(int Id)
        {
            var image = _redeeppitory.Query(x=>x.Id==Id).Select().SingleOrDefault();
            image = image == null ? new Image() : image;
            return image;//.Count()<=0?new List<Image>():image.ToList();
        }
        public IEnumerable<Image> GetImageByRefcode(int RefTypeId)
        {
            var image = _redeeppitory.Query(x => x.RefTypeId == RefTypeId).Select();//.Where(x => x.RefTypeId == RefTypeId);
            return image;
        }
        public IEnumerable<Image> GetImageByRefTypeIdAndRefPrimaryKey(int RefTypeId, int RefPrimaryKey)
        {

            var Image = _redeeppitory.Query(x => x.RefTypeId == RefTypeId && x.RefPrimaryKey == RefPrimaryKey).Select();//.Where(x => x.RefTypeId == RefTypeId && x.RefPrimaryKey == RefPrimaryKey);           
            return Image;
        }

        public void DeleteImage(int Id)
        {
            _redeeppitory.Delete(Id);
        }
       
       


    }
}
