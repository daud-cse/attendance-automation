app.factory('CertificateService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Country', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
