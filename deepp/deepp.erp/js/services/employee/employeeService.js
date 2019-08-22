app.factory('EmployeeService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/employee', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'New': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/employee/new" },
        'NewEmployee': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/employee/newEmployee" },
        'SearchEmployee': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/employee/search" },
        'GetAllSmsDetails': { method: 'POST', isArray: true, params: { employee: null, branchId: null }, url: apiUrlPrefix + "api/employee/getAllSmsDetails" },
    });
}]);
