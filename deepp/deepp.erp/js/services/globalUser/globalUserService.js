app.factory('globalUserService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/GlobalUsers', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'New': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/GlobalUsers/new" },
        'NewGlobalUsers': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/GlobalUsers/newGlobalUsers" },
        'SearchGlobalUsers': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/GlobalUsers/search" },
        'GetAllSmsDetails': { method: 'POST', isArray: true, params: { GlobalUsers: null, branchId: null }, url: apiUrlPrefix + "api/GlobalUsers/getAllSmsDetails" },
    });
}]);