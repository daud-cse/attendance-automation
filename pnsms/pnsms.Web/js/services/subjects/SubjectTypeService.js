app.factory('SubjectTypeService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/SubjectType', {}, {
        'getAll': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/SubjectType/getAll" },
        'getById': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/SubjectType/getDetailsById" },
        'save': { method: 'POST', url: apiUrlPrefix + "api/SubjectType/save" },
        'update': { method: 'PUT', url: apiUrlPrefix + "api/SubjectType/update" }
    });
}]);
