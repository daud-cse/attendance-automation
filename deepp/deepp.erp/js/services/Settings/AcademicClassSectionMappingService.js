app.factory('AcademicClassSectionMappingService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/AcademicClassSectionMapping', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'new': { method: 'GET', url: apiUrlPrefix + "api/AcademicClassSectionMapping/new" }
    });
}]);
