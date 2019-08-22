app.factory('AcademicBranchService', ['$resource', function ($resource) {
   // alert("dfsdf");
    return $resource(apiUrlPrefix + 'api/academicBranch', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } }
    });
}]);
