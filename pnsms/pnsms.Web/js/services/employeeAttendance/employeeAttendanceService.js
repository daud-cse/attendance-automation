app.factory('EmployeeAttendanceService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/employeeattendance', {}, {
        query: { method: 'GET', isArray: true },
        'list': { method: 'POST', url: apiUrlPrefix + "api/employeeattendance/list" },
        'saveemployeeattendance': { method: 'POST', url: apiUrlPrefix + "api/employeeattendance/saveemployeeattendance" },
        'updateemployeeattendance': { method: 'POST', url: apiUrlPrefix + "api/employeeattendance/updateemployeeattendance" },
        'getsingle': { method: 'GET', params: { id: 'employeeattendanceId' }, url: apiUrlPrefix + "api/employeeattendance/getsingle" },
        'new': { method: 'POST', url: apiUrlPrefix + "api/employeeattendance/new" },
        
    });
}]);