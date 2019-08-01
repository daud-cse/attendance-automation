app.factory('CertificatePrintService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/certificateprint', {}, {
        query: { method: 'GET', isArray: true },
        'list': { method: 'POST', url: apiUrlPrefix + "api/certificateprint/list" },
        'save': { method: 'POST', url: apiUrlPrefix + "api/certificateprint/save" },
        'new': { method: 'POST', url: apiUrlPrefix + "api/certificateprint/new" },
        'getsingle': { method: 'GET', params: { id: 'certificateprintId' }, url: apiUrlPrefix + "api/certificateprint/getsingle" },
    });
}]);