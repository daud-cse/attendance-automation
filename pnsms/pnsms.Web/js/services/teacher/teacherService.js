app.factory('TeacherService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/teacher', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'New': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/teacher/new" },
        'NewTeacher': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/teacher/newTeacher" },
        'SearchTeacher': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/teacher/search" },
        'GetAllSmsDetails': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/teacher/getAllSmsDetails" },
    });
}]);
