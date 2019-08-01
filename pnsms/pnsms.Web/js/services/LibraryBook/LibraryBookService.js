app.factory('LibraryBookService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/LibraryBook', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
