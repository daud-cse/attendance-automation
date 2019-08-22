 
app.factory('ReligionService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Religion', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
