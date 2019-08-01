app.factory('ExamSubjectMarksService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/ExamSubjectMark', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'New': { method: 'GET', url: apiUrlPrefix + "api/examsubjectmark/newexamsubjectmark" },        
        'save': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/SaveExamSubjectMark" },
        'GetExamByCriteria': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/GetExamByCriteria" },
        'GetExamById': { method: 'GET', params:{id:'id'}, url: apiUrlPrefix + "api/GetExamById" },
        'GetSubjectCriteria': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/GetSubjectCriteria" },
        'GetExamSubjectMarksByCriteria': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/GetExamSubjectMarksByCriteria" },
        'getScetionClasswise': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/AcademicClassSectionMapping/getsectionbyacademicclass" },
        'GetSubjectAcademicClassMapping': { method: 'GET', url: apiUrlPrefix + "api/SubjectAcademicClassMapping/GetSubjectAcademicClassMapping" }
    });
}]);
