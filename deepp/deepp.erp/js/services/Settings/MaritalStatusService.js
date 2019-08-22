app.factory('MaritalService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/MaritalStatuses', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
