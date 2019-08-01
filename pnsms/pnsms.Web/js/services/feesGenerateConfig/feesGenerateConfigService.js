app.factory('FeesGenerateConfigService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/feesGenerateConfig', {}, {
        query: { method: 'GET', isArray: true },
        'config': { method: 'POST', url: apiUrlPrefix + "api/feesgenconfig/panel" },
        'addtype': { method: 'POST', url: apiUrlPrefix + "api/feesgenconfig/addtype" },
        'edittype': { method: 'POST', url: apiUrlPrefix + "api/feesgenconfig/edittype" },
        'checktype': { method: 'POST', url: apiUrlPrefix + "api/feesgenconfig/checktype" },
        'inactivetype': { method: 'POST', url: apiUrlPrefix + "api/feesgenconfig/inactivetype" }
    });
}]);
