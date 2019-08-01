app.factory('FeesAutoGeneratService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/feesautogenerate', {}, {
        query: { method: 'GET', isArray: true },
        'new': { method: 'POST', url: apiUrlPrefix + "api/feesautogenerate/new" },
        'list': { method: 'POST', url: apiUrlPrefix + "api/feesautogenerate/list" },
        'save': { method: 'POST', url: apiUrlPrefix + "api/feesautogenerate/save" },
        'saveEnroll': { method: 'POST', url: apiUrlPrefix + "api/feesautogenerate/saveEnroll" },
        'listEnroll': { method: 'GET', url: apiUrlPrefix + "api/feesautogenerate/listEnroll" },
        'newEnroll': { method: 'POST', url: apiUrlPrefix + "api/feesautogenerate/newEnroll" },
        'getsingleEnroll': { method: 'GET', params: { id: 'feesAutoGenerateId' }, url: apiUrlPrefix + "api/feesautogenerate/getsingleEnroll" },
        'getsingle': { method: 'GET', params: { id: 'feesAutoGenerateId' }, url: apiUrlPrefix + "api/feesautogenerate/getsingle" }
    });
}]);
