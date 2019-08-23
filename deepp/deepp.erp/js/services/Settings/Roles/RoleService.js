app.factory('RoleService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/role', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
