app.factory('ExamTypeService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/ExamType', {}, {
        query: { method: 'GET', isArray: true },
        update:
            { method: 'PUT', params: { id: 'id' },url: apiUrlPrefix + "api/UpdateExamType" },

        save: {
            method: 'POST', url: apiUrlPrefix + "api/SaveExamType"
        }
    });
}]);
