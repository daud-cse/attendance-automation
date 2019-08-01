app.factory('EventService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/event', {}, {
        query: { method: 'GET', isArray: true },
        'new': { method: 'POST', url: apiUrlPrefix + "api/event/new" },
        'save': { method: 'POST', url: apiUrlPrefix + "api/event/save" },
        'list': { method: 'POST', url: apiUrlPrefix + "api/event/list" },
        'getsingle': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/event/getsingle" },
    });
}]);
