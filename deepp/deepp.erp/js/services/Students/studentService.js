app.factory('StudentService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/student', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'New': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/student/new" },
        'NewStudent': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/student/newStudent" },
        'SearchStudent': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/student/search" },
        'GetAllSmsDetails': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/student/getAllSmsDetails" },
        'GetAllGuardianSmsDetails': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/guardian/getAllSmsDetails" },
        'GetAllGoverningbodySmsDetails': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/governingbody/getAllSmsDetails" },
        'getScetionClasswise': { method: 'GET',isArray: true, url: apiUrlPrefix + "api/AcademicClassSectionMapping/getsectionbyacademicclass" },
        
    });
}]);
