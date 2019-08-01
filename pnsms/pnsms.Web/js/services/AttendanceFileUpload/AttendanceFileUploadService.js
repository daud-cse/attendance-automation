
app.factory('AttendanceFileUploadService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/AttendanceExcelUpload', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'New': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/institute/new" },
        'Current': { method: 'GET', url: apiUrlPrefix + "api/institute/current" },
        'uploadexcel': { method: 'PUT', params: { id: 'id' }, url: apiUrlPrefix + 'api/AttendanceExcelUpload' },
        'updateInfotext': { method: 'PUT', params: { id: 'id' }, url: apiUrlPrefix + 'api/Institute/infotext' },
        'updateCurrent': { method: 'PUT', params: { id: 'id' }, url: apiUrlPrefix + 'api/Institute/Current' },

    });
}]);