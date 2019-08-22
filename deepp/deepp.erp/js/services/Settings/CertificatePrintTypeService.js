app.factory('CertificatePrintTypeService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/CertificatePrintType', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
