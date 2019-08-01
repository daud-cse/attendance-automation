app.factory('ExamTabulationSheetService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/student', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'New': { method: 'GET', url: apiUrlPrefix + "api/newExamTabulationSheet" },
        'NewStudent': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/student/newStudent" },
        'save': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/SaveExamSubjectMark" },
        'GetTabulationSheetMasterCriteria': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/GetTabulationSheetMasterCriteria" },
        //'GetExamTabulationSheetCriteria': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/GetExamTabulationSheetCriteria" },
        'GetExamTabulationSheetByCriteria': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/GetExamTabulationSheetByCriteria" },
        'getScetionClasswise': { method: 'GET', isArray: true, url: apiUrlPrefix + "api/AcademicClassSectionMapping/getsectionbyacademicclass" },

    });
}]);
