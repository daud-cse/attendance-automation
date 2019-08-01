
// Factory Wise
app.factory('ExamGradeService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/ExamGrade', {}, {
        
        query: { method: 'GET', isArray: true },

        update: { method: 'PUT',params: { id: 'id' }, url: apiUrlPrefix + "api/UpdateExamGrade" },
        save: { method: 'POST', isArray: true, url: apiUrlPrefix + "api/SaveExamGrade" }
    });
}]);

