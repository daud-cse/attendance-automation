app.factory('BloodGroupService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/BloodGroup', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
