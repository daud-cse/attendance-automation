app.factory('AddressTypeService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/AddressType', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
