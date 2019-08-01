app.factory('InstituteService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/Institute', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'New': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/institute/new" },
        'Current': { method: 'GET', url: apiUrlPrefix + "api/institute/current" },
        'updateSlider': { method: 'PUT', params: { id: 'id' }, url: apiUrlPrefix + 'api/institute/UpdateSlider' },
        'updateInfotext': { method: 'PUT', params: { id: 'id' }, url: apiUrlPrefix + 'api/Institute/infotext' },
        'updateCurrent': { method: 'PUT', params: { id: 'id' }, url: apiUrlPrefix + 'api/Institute/Current' },
        
    });
}]);
