 
app.factory('ProfessionService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Profession', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
