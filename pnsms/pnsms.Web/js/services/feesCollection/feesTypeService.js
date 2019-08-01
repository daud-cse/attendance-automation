app.factory('feesTypeService', ['$resource', function ($resource) {
        return $resource(apiUrlPrefix + 'api/feesType', {}, {
            query: { method: 'GET', isArray: true },
            update: { method: 'PUT', params: { id: 'id' }, url: apiUrlPrefix + "api/editFeesType" },
            save: {
                method: 'POST', url: apiUrlPrefix + "api/saveFeesType"
            }
        });
    }]);
