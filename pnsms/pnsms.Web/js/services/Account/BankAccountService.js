app.factory('BankAccountService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Account', {}, {
        'query': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/GetBankAccount" },

        'edit': { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/GetBankAccountById" },
        //   update: { method: 'PUT', params: { id: 'id' } },
        update: { method: 'PUT', isArray: true, url: apiUrlPrefix + "api/UpdateBankAccount/" },
        New: { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/BankAccountInfo/newBankAccountInfo" },
        save: { method: 'Post', isArray: true, url: apiUrlPrefix + "api/SaveBankAccount" }
       


    });
}]);
