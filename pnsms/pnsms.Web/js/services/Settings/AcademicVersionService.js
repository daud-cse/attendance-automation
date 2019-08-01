app.factory('AcademicVersionService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/AcademicVersion', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
