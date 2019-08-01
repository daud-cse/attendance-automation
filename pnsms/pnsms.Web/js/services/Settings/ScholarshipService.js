app.factory('ScholarshipService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Scholarship', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
