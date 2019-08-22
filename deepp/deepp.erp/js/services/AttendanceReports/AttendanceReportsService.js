app.factory('AttendanceReportsService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/AttendanceReports', {}, {
        query: { method: 'GET', isArray: true },

        'edit': { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/GetExamById" },
        update: { method: 'PUT', params: { id: 'id' } },
        New: { method: 'Get', url: apiUrlPrefix + "api/newAttendanceReports" },
        GetAttendanceReports: { method: 'Get', AttendanceReports: 'VmCommonSearch', url: apiUrlPrefix + "api/GetAttendanceReports" },
        'getScetionClasswise': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/AcademicClassSectionMapping/getsectionbyacademicclass" },
        'SearchUserInfo': { method: 'GET', params: { searchItem: 'searchItem' }, isArray: true, url: apiUrlPrefix + "api/UserInfoSearch" },
    });
}]);
