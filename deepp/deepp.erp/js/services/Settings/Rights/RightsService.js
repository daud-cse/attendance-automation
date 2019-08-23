app.factory('RightsService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/rights', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
