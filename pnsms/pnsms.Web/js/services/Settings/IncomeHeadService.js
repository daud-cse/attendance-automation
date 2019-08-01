app.factory('IncomeHeadService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/IncomeHead', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
