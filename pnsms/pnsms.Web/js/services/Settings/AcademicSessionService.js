app.factory('AcademicSessionService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/academicsession', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
