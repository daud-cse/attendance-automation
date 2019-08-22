app.factory('NoticeService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/notice', {}, {
        query: { method: 'GET', isArray: true },
        'new': { method: 'POST', url: apiUrlPrefix + "api/notice/new" },
        'save': { method: 'POST', url: apiUrlPrefix + "api/notice/save" },
        'list': { method: 'POST', url: apiUrlPrefix + "api/notice/list" },
        'getsingle': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/notice/getsingle" },
    });
}]);
