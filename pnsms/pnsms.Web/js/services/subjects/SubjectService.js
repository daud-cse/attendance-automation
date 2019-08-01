app.factory('SubjectService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/subject', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
