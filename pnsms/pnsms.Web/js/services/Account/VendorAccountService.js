app.factory('VendorAccountService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Account', {}, {
        'query': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/GetVendorAccount/" },

        'edit': { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/GetVendorAccountById" },
        //   update: { method: 'PUT', params: { id: 'id' } },
        update: { method: 'PUT', isArray: true, url: apiUrlPrefix + "api/UpdateVendorAccount/" },
        New: { method: 'Get', url: apiUrlPrefix + "api/VendorInfo/newVendorInfo" },
        save: { method: 'Post', isArray: true, url: apiUrlPrefix + "api/SaveVendorAccount/" }



    });
}]);
