using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sfa.WindowsService.ApiService
{
    public class URLService
    {

        public static String URL_HOST = "http://posapi.shikkhaforall.com/";
        // public static String URL_HOST = "http://localhost:1692/";

        public static String URL_LOGIN = URL_HOST + "api/login";
        public static String URL_TEACHERATTENDANCE = URL_HOST + "api/teacherattendance/saveteacherattendance";
        public static String URL_LISTTEACHERATTENDANCE = URL_HOST + "api/teacherattendance/saveteacherattendance";
        public static String URL_POSTLISTMACHINEINFO = URL_HOST + "api/teacherattendance/machinedatapostteacherattendance";

        public static String URL_GET_ATTENDANCETYPE = URL_HOST + "api/attendancetype";
        public static String URL_GET_ATTENDANCEDATASYNINFO = URL_HOST + "api/teacherattendance/getattendancedatasyninfo";
        public static String URL_POST_UPDATE = URL_HOST + "?r=api/update";
        public static String URL_POST_FAILED_UPDATE = URL_HOST + "?r=api/failedupdate";

        public static String URL_GET_BP_ALL_EMAIL = URL_HOST + "?r=api/bpallemail";
        public static String URL_GET_BP_WORKABLE_EMAIL = URL_HOST + "?r=api/bpworkableemail";
        public static String URL_GET_BP_NOT_WORKABLE_EMAIL = URL_HOST + "?r=api/bpnotworkableemail";
        public static String URL_UPDATE_PVA_STATUS = URL_HOST + "?r=api/updatepvastatus";

        public static String URL_REQUEST_PVA = URL_HOST + "?r=api/requestpva";
        public static String URL_REQUEST_PVA_UPDATE = URL_HOST + "?r=api/requestpvaupdate";
        public static String URL_REQUEST_ONE_MESSAGE = URL_HOST + "?r=api/requestonemessage";
        public static String URL_TOTAL_LOCKED_PVA = URL_HOST + "?r=api/totallockedpva";
        public static String URL_LIST_OF_UNLOCK_PVA_BY_STATE = URL_HOST + "?r=api/listofunlockpvabystate";

        public static String URL_REQUEST_LEAD_RESET = URL_HOST + "?r=auto/unlockpva";
    }
}
