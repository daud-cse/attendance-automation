app.factory('AttendanceTypeService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/AttendanceType', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },

        'New': { method: 'Get', url: apiUrlPrefix + "api/AttendanceType/new" }
    });
}]);
