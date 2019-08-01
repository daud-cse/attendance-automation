app.factory('ChangePasswordService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/usersecurity', {}, {
        query: { method: 'GET', isArray: true },
        'passwordchange': { method: 'POST', url: apiUrlPrefix + "api/usersecurity/passwordchange" }
    });
}]);