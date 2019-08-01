using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pnsms.utility
{

    public enum RefCode
    {
        Institute_Banner = 11
             ,
        Institute_Logo = 12
             ,
        Institute_Landing_Slider = 13
            ,
        UserInfo_Photo = 14
            ,
        UserInfo_Photo_Small = 20
            ,
        Notice = 15
            ,
        Events = 16
            ,
        Gallery = 17
            ,
        Galley_Small = 18
        ,
        Galley_Default = 19
        ,
        Content = 21
        , 
        Result_Publish = 22
    };

    public enum UserInfoType
    {
        Student = 11
            ,
        Guardian = 12
            ,
        Teacher = 13
        ,
        Employee = 14
        ,
        Global_Users = 15
        ,
        Governingbody = 16
    };

    public enum ErrorCode
    {
        UserSecurity_UserNameExists = 101,
        UserSecurity_PasswordNotChanged = 102,
        NoError = 0
    };

    public enum DashBoardColor
    {
        sky = 1
        ,
        orange
            ,
        brown
            ,
        midnightblue
            ,
        purple
            ,
        success
            ,
        primary
            ,
        indigo
            ,
        green
            ,
        danger
            ,
        magenta
        , inverse
    };

    public enum NoticeType
    {
        Student = 11
        ,
        Teacher = 12
        , 
        Employee = 13       
    };

    public enum ShortMessageStstus
    {
        Pending = 101
            ,
        Delivered = 102
           ,
        Undelivered = 103
    };

}
