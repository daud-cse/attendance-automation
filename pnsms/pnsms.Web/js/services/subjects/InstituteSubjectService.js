app.factory('InstituteSubjectService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/InstituteSubject', {}, {
        query: { method: 'GET', isArray: true },
        save: { method: 'POST', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'new': { method: 'GET', url: apiUrlPrefix + "api/InstituteSubject/new" }
       
       
    });
}]);
