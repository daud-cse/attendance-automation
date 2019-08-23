app.factory('GenderService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Gender', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
