app.factory('AcademicPeriodService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/AcademicPeriod', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
