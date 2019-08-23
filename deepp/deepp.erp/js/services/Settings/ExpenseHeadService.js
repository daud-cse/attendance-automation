app.factory('ExpenseHeadService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/ExpenseHead', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
