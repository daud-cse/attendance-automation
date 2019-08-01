app.factory('ResultPublishService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/resultpublish', {}, {
        query: { method: 'GET', isArray: true },
        'new': { method: 'POST', url: apiUrlPrefix + "api/resultpublish/new" },
        'save': { method: 'POST', url: apiUrlPrefix + "api/resultpublish/save" },
        'list': { method: 'POST', url: apiUrlPrefix + "api/resultpublish/list" },
        'getsingle': { method: 'GET', params: { id: 'resultPublishId' }, url: apiUrlPrefix + "api/resultpublish/getsingle" },
    });
}]);
