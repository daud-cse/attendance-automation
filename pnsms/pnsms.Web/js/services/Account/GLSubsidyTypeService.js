app.factory('GLSubsidyTypeService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Account', {}, {
        'query': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/GetGLSubsidyTypeInfo/" },

        'edit': { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/GetGLSubsidyTypeInfoById" },
        //   update: { method: 'PUT', params: { id: 'id' } },
        update: { method: 'PUT', isArray: true, url: apiUrlPrefix + "api/UpdateGLSubsidyTypeInfo/" },
        New: { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/GLSubsidyTypeInfo/newGLSubsidyTypeInfo" },
        save: { method: 'Post', isArray: true, url: apiUrlPrefix + "api/SaveGLSubsidyTypeInfo/" }



    });
}]);
