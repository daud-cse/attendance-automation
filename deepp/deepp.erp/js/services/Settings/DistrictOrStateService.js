app.factory('DistrictOrStateService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/DistrictOrState', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
