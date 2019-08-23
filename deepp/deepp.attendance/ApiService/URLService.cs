using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace deepp.attendance.ApiService
{
  public  class URLService
    {

        public static String URL_HOST = "http://deeppapi.shikkhaforall.com/";
       // public static String URL_HOST = "http://localhost:1692/";
        
        public static String URL_LOGIN = URL_HOST + "api/login";
        public static String URL_TEACHERATTENDANCE = URL_HOST + "api/teacherattendance/saveteacherattendance";
        public static String URL_LISTTEACHERATTENDANCE = URL_HOST + "api/teacherattendance/saveteacherattendance";
        public static String URL_POSTLISTMACHINEINFO = URL_HOST + "api/teacherattendance/machinedatadeepptteacherattendance";

        public static String URL_GET_ATTENDANCETYPE = URL_HOST + "api/attendancetype";
        public static String URL_GET_ATTENDANCEDATASYNINFO = URL_HOST + "api/teacherattendance/getattendancedatasyninfo";
        public static String URL_POST_UPDATE = URL_HOST + "?r=api/update";
        public static String URL_POST_FAILED_UPDATE = URL_HOST + "?r=api/failedupdate";

        public static String URL_GET_BP_ALL_EMAIL = URL_HOST + "?r=api/bpallemail";
        public static String URL_GET_BP_WORKABLE_EMAIL = URL_HOST + "?r=api/bpworkableemail";
        public static String URL_GET_BP_NOT_WORKABLE_EMAIL = URL_HOST + "?r=api/bpnotworkableemail";
        public static String URL_UPDATE_PVA_STATUS = URL_HOST + "?r=api/updatepvastatus";


        
    }
}
