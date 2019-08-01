app.factory('PackageService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Package', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
