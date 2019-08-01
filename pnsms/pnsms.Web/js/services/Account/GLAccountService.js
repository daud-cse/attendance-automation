app.factory('GLAccountService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Account', {}, {
        'query': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/GetGLAccount/" },

        'edit': { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/GetGLAccountById" },
        //   update: { method: 'PUT', params: { id: 'id' } },
        update: { method: 'PUT', isArray: true, url: apiUrlPrefix + "api/UpdateGLAccount/" },
        New: { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/GLAccountInfo/newGLAccountInfo" },
        save: { method: 'Post', isArray: true, url: apiUrlPrefix + "api/SaveGLAccount/" }



    });
}]);
