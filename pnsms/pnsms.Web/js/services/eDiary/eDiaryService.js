app.factory('eDiaryService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/marksEntry', {}, {
        query: { method: 'GET', isArray: true },
        'GetDiaries': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/eDiary/teacher" },
        'New': { method: 'GET',   url: apiUrlPrefix + "api/eDiary/new" },
        'Update': { method: 'POST', url: apiUrlPrefix + "api/eDiary/update" }
        
    });
}]);
