app.factory('ExamService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Exam', {}, {
        'query': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/GetExam" },
      
        'edit': { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/GetExamById" },        
     //   update: { method: 'PUT', params: { id: 'id' } },
        update: { method: 'PUT', isArray: true, url: apiUrlPrefix + "api/UpdateExam" },
        New: { method: 'Get', params: { id: 'id' }, url: apiUrlPrefix + "api/Exam/newExam" },
        save: { method: 'Post', isArray: true, url: apiUrlPrefix + "api/SaveExam" },
        'getScetionClasswise': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/AcademicClassSectionMapping/getsectionbyacademicclass" }


    });
}]);
