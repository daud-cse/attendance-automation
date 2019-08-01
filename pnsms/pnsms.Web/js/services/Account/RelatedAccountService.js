app.factory('RelatedAccountService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Account', {}, {
        'query': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/GetRelatedAccount/" },

        'edit': { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/GetRelatedAccountById" },
        //   update: { method: 'PUT', params: { id: 'id' } },
        update: { method: 'PUT', isArray: true, url: apiUrlPrefix + "api/UpdateRelatedAccount/" },
        New: { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/RelatedAccountInfo/newRelatedAccountInfo" },
        save: { method: 'Post', isArray: true, url: apiUrlPrefix + "api/SaveRelatedAccount/" }



    });
}]);