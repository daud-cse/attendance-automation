app.factory( 'SmsTemplateService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/smstemplate', {}, {
        query: { method: 'GET', isArray: true },
        'New': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/smstemplate/new" },
        'update': { method: 'PUT', params: { id: 'id' } },
    });
}]);
