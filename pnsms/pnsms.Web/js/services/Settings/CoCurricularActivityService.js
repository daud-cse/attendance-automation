app.factory('CoCurricularActivityService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/CoCurricularActivity', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
