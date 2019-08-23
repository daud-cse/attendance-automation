using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deepp.Entities.ResponseModel
{
  public  class SynStudentAttendanceReponseModel
    {
        public string Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public object Obj { get; set; }
        public List<object> lstObj { get; set; }
      //SynStudentAttendanceReponseModel

    }

  public class SynStudentAttendanceReponseModelDeatails
  {
      public int LocalId { get; set; }
      public int StudentAttendanceId { get; set; }
      public int StudentId { get; set; }
      public int Id { get; set; }
  }
}
