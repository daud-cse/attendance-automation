app.factory('OnlineAdmissionService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/onlineadmission', {}, {
        query: { method: 'GET', isArray: true },
        'list': { method: 'POST', url: apiUrlPrefix + "api/onlineadmission/list" },
        'update': { method: 'POST', url: apiUrlPrefix + "api/onlineadmission/update" },
        'save': { method: 'POST', url: apiUrlPrefix + "api/onlineadmission/save" },
        'getsingle': { method: 'GET', params: { id: 'onlineAdmissionId' }, url: apiUrlPrefix + "api/onlineadmission/getsingle" },
    });
}]);