app.factory('EducationalQualificationService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/EducationalQualification', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
