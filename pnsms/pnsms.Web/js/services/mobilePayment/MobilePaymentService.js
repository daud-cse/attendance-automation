app.factory('MobilePaymentService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/mobilepayment', {}, {
        query: { method: 'GET', isArray: true },
        'new': { method: 'POST', url: apiUrlPrefix + "api/mobilepayment/new" },
        'save': { method: 'POST', url: apiUrlPrefix + "api/mobilepayment/save" },
        'list': { method: 'POST', url: apiUrlPrefix + "api/mobilepayment/list" },
        'getsingle': { method: 'GET', params: { id: 'mpaymentId' }, url: apiUrlPrefix + "api/mobilepayment/getsingle" },
    });
}]);
