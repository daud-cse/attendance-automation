app.factory('UserInfoService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {
    var apiUrlPrefix = ngAuthSettings.apiServiceBaseUri
    function SearchUserInfo(searchItem,userTypeId) {
        return $http({ url: apiUrlPrefix + "api/UserInfoSearch?searchItem=" + searchItem +"&userTypeId="+userTypeId, method: 'GET' }).then(function (result) {
                return result;
            });
    };

       
   
     return {
         SearchUserInfo: SearchUserInfo
     } 
}]);