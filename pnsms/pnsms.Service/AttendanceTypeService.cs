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
   
    public interface IAttendanceTypeService: IService<AttendanceType>
    {
        IEnumerable<AttendanceType> GetAttendanceTypes(int instituteId, bool isActive = false);
        IEnumerable<AttendanceType> GetAttendanceTypesForStudent(int InstituteId);
        IEnumerable<AttendanceType> GetAttendanceTypesForTeacher(int InstituteId);
        IEnumerable<AttendanceType> GetAttendanceTypesForEmployee(int InstituteId);
        AttendanceType GetAttendanceTypeById(int id);
        AttendanceType NewAttendanceType();

    }
    public class AttendanceTypeService : Service<AttendanceType>, IAttendanceTypeService
    {
        private readonly IRepositoryAsync<AttendanceType> _repository;
        private readonly IColourService _colourService;
        public AttendanceTypeService(IRepositoryAsync<AttendanceType> repository, IColourService colourService)
            : base(repository)
        {
            _repository = repository;
            _colourService=colourService;
        }


        public IEnumerable<AttendanceType> GetAttendanceTypes(int instituteId,bool isActive = false)
        {
            if (isActive)
            {
                return _repository.Query(x => x.IsActive.Equals(true) && x.InstituteId == instituteId).Include(x => x.Colour).Select();
            }

            return _repository.Query(x=>x.InstituteId==instituteId).Include(x=>x.Colour).Select();
        }

        public AttendanceType GetAttendanceTypeById(int id)
        {
            return _repository.Query().Select().FirstOrDefault(x => x.Id == id);
        }

        public AttendanceType NewAttendanceType()
        {
            var colourList = _colourService.GetColours().ToList();
            var attendanceType = new AttendanceType();            
            var lstColourKVP = new List<KeyValuePair<int, string>>();
            colourList.ForEach(item => lstColourKVP.Add(new KeyValuePair<int, string>(item.Id, item.Name)));
            attendanceType.ColourList = lstColourKVP;
            return attendanceType;
        }
        public override void Update(AttendanceType attendanceType)
        {
            attendanceType.LastUpdateTime = DateTime.Now;
             _repository.Update(attendanceType);

        }
        /// <summary>
        /// anirban
        /// </summary>
        /// <param name="InstituteId"></param>
        /// <returns></returns>
        public IEnumerable<AttendanceType> GetAttendanceTypesForStudent(int InstituteId)
        {
            return _repository.Query(d => d.IsActive.Equals(true) && d.InstituteId == InstituteId)
                .Include(x => x.Colour)
                .Select();
        }

        public IEnumerable<AttendanceType> GetAttendanceTypesForTeacher(int InstituteId)
        {
            return _repository.Query(d => d.IsActive.Equals(true) && d.IsUsedForTeacher==true && d.InstituteId == InstituteId)
                .Include(x => x.Colour)
                .Select();
        }

        public IEnumerable<AttendanceType> GetAttendanceTypesForEmployee(int InstituteId)
        {
            return _repository.Query(d => d.IsActive.Equals(true)  && d.InstituteId == InstituteId)
                .Include(x => x.Colour)
                .Select();
        }

    }
   
}
