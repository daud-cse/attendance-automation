app.factory('ExamService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Exam', {}, {
        query: { method: 'GET', isArray: true },

        'edit': { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/GetExamById" },
        update: { method: 'PUT', params: { id: 'id' } },
        New: { method: 'Get', url: apiUrlPrefix + "api/Exam/newExam" },
        save: { method: 'Post', isArray: true, url: apiUrlPrefix + "api/SaveExam" }

    });
}]);
