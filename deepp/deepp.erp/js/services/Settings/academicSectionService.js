app.factory('AcademicSectionService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/academicsection', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
