app.factory('ExamTabulationModalDetailsService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/student', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'New': { method: 'GET', url: apiUrlPrefix + "api/newExamTabulationSheet" },
       
        
        'GetExamTabulationSheetCriteria': { method: 'POST', isArray: false, url: apiUrlPrefix + "api/GetExamTabulationSheetCriteria" },
       

    });
}]);
