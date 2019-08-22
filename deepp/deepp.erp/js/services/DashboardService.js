app.factory('DashBoardService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/dashboard', {}, {
        query: { method: 'GET', isArray: true },
        'summary': { method: 'GET', url: apiUrlPrefix + "api/dashboard/summary" },
    });
}]);