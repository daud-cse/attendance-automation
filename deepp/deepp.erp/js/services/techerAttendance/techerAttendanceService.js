app.factory('TeacherAttendanceService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/teacherattendance', {}, {
        query: { method: 'GET', isArray: true },
        'list': { method: 'POST', url: apiUrlPrefix + "api/teacherattendance/list" },
        'saveteacherattendance': { method: 'POST', url: apiUrlPrefix + "api/teacherattendance/saveteacherattendance" },
        'updateteacherattendance': { method: 'POST', url: apiUrlPrefix + "api/teacherattendance/updateteacherattendance" },
        'updateteacherattendancedetails': { method: 'POST', url: apiUrlPrefix + "api/teacherattendance/updateteacherattendancedetails" },
        'getsingle': { method: 'GET', params: { id: 'teacherattendanceId' }, url: apiUrlPrefix + "api/teacherattendance/getsingle" },
        'new': { method: 'POST', url: apiUrlPrefix + "api/teacherattendance/new" },
        
    });
}]);