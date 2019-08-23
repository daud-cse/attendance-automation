app.factory( 'SmsService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/ShortMessage', {}, {
        query: { method: 'GET', isArray: true },
        'New': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/ShortMessage/new" },
        'update': { method: 'PUT', params: { id: 'id' } },
    });
}]);
