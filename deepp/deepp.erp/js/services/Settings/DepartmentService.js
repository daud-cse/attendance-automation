app.factory('DepartmentService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/department', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
