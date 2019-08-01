//app.factory('feesHeadsService', function ($http) {
//    var serviceBase = apiUrlPrefix;
//    function save(feeshead) {
//        alert()
//        return $http.post(serviceBase + "api/feeshead/save", feeshead);
//    }
//    function query() {
//        return $http.get(serviceBase + "api/feeshead/get").then(function (result) {
//            return result.data;
//        });
//    }
//    function update(feeshead) {
//        return $http.post(serviceBase + "api/feeshead/edit", feeshead).then(function (result) {
//            return result.data;
//        });
//    }
//    return {
//        save: save,
//        query: query,
//        edit: edit
//    }
//});

app.factory('feesHeadsService', ['$resource', function ($resource) {
    return $resource(apiUrlPrefix + 'api/feeshead', {}, {
        query: { method: 'GET', isArray: true },
        update:
            { method: 'PUT', params: { id: 'id' }, url: apiUrlPrefix + "api/editFeeshead" },

        save: {
            method: 'POST', url: apiUrlPrefix + "api/saveFeeshead"
        }
    });
}]);
