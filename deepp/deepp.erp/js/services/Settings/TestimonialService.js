app.factory('TestimonialService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/testimonial', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } } 
        
    });
}]);
