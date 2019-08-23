app.factory('GuardianTypeService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/GuardianType', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
