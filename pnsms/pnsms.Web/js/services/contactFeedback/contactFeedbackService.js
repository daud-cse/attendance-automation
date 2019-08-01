app.factory('ContactFeedbackService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/contactusfeedback', {}, {
        query: { method: 'GET', isArray: true },
        'list': { method: 'POST', url: apiUrlPrefix + "api/contactusfeedback/list" },
        'getsingle': { method: 'GET', params: { id: 'contactFeedbackId' }, url: apiUrlPrefix + "api/contactusfeedback/getsingle" },
    });
}]);
