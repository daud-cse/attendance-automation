app.factory('VoucherEntryService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/voucherEntry', {}, {
        query: { method: 'GET', isArray: true },
        'list': { method: 'POST', url: apiUrlPrefix + "api/voucherEntry/list" },
        'save': { method: 'POST', url: apiUrlPrefix + "api/voucherEntry/save" },
        'update': { method: 'POST', url: apiUrlPrefix + "api/voucherEntry/update" },
        'new': { method: 'POST', url: apiUrlPrefix + "api/voucherEntry/new" },
        'getsingle': { method: 'GET', params: { id: 'voucherEntryId' }, url: apiUrlPrefix + "api/voucherEntry/getsingle" },
    });
}]);