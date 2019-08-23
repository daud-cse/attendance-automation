app.factory('StudentAttendanceService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/studentattendance', {}, {
         query: { method: 'GET', isArray: true },
         'update': { method: 'PUT', params: { id: 'id' } },
         'newteacher': { method: 'GET', isArray:true,url: apiUrlPrefix + "api/studentattendance/newteacher" },
         'newmanagement': { method: 'GET', url: apiUrlPrefix + "api/studentattendance/newmanagement" },
         'loadstudents': { method: 'POST', url: apiUrlPrefix + "api/studentattendance/loadstudents" },
         'loadstudentsforteacher': { method: 'POST', url: apiUrlPrefix + "api/studentattendance/loadstudentsforteacher" },
         'saveattendance': { method: 'POST', url: apiUrlPrefix + "api/studentattendance/saveattendance" },
         'updateattendance': { method: 'POST', url: apiUrlPrefix + "api/studentattendance/updateattendance" },
         'listbymanagement': { method: 'POST', url: apiUrlPrefix + "api/studentattendance/listbymanagement" },
         'listbyteacher': { method: 'POST', url: apiUrlPrefix + "api/studentattendance/listbyteacher" },
         'getsingle': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/studentattendance/getsingle" },
         'attendancesheetprint': { method: 'POST', url: apiUrlPrefix + "api/studentattendance/attendancesheetprint" },
         'getteachers': { method: 'GET', url: apiUrlPrefix + "api/studentattendance/getteachers" },

         'getScetionClassWiseSubjectClassSectionwise': { method: 'GET', url: apiUrlPrefix + "api/SubjectAcademicClassMapping/getsectionbyacademicclass" },
         'updateabsconding': { method: 'POST', url: apiUrlPrefix + "api/studentattendance/updateabsconding" },
         'getScetionClasswise': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/AcademicClassSectionMapping/getsectionbyacademicclass" },
    });
}]);
