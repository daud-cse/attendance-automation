app.factory('LibraryBookAuthoreService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/LibraryBookAuthore', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
