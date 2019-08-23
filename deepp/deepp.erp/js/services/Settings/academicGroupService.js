app.factory('AcademicGroupService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/academicgroup', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
