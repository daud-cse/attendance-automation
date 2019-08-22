app.factory('GalleryService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/gallery', {}, {
        query: { method: 'GET', isArray: true },
        'update': { method: 'PUT', params: { id: 'id' } },
        'New': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/gallery/new" },
        'NewGallery': { method: 'GET', params: { id: 'id' }, url: apiUrlPrefix + "api/gallery/newgallery" },
        'SearchGallery': { method: 'POST', isArray: true, url: apiUrlPrefix + "api/gallery/search" },
    });
}]);
