app.factory('SubjectAcademicClassMappingService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/SubjectAcademicClassMapping', {}, {
        query: { method: 'GET', params: { id: 'id',AcademicBranchId:'AcademicBranchId' }, isArray: true },
        save: { method: 'POST', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'new': { method: 'GET', url: apiUrlPrefix + "api/SubjectAcademicClassMapping/new" },
        'GetExamConfig': { method: 'GET', url: apiUrlPrefix + "api/SubjectAcademicClassMapping/GetExamConfig" },
        'copy': { method: 'GET', params: { toAcademicClassId: 'toAcademicClassId', academicSessionId: 'academicSessionId', academicClassId: 'academicClassId', toAcademicSessionId: 'toAcademicSessionId' }, url: apiUrlPrefix + "api/SubjectAcademicClassMapping/copyMapping" }
    });
}]);
