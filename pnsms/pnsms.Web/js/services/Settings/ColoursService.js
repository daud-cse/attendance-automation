app.factory('ColoursService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Colours', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
