app.factory('AcademicClassService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/academicclass', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
