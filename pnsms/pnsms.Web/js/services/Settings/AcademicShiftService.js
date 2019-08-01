app.factory('AcademicShiftService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/AcademicShift', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
