app.factory('ExamProcessService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/ExamType', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        New: { method: 'GET', url: apiUrlPrefix + "api/newExamProcess" },
        'ExamProcessExecute': {
            method: 'POST',isArray: true, url: apiUrlPrefix + "api/ExamProcess"
        }
    });
}]);
