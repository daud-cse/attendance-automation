
app.factory('NationalityService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Nationality', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
