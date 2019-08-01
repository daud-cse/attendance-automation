app.factory('DesignationService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Designation', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
