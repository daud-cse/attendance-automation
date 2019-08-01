app.controller('FeesEnrollAutoGenerateListCtrl', ['$scope', '$state', '$http', 'FeesAutoGeneratService', '$filter', 'toaster',
    function ($scope, $state, $http, FeesAutoGeneratService, $filter, toaster) {

        $scope.listModel = null;
        $scope.isLoading = true;
        FeesAutoGeneratService.listEnroll(function (result) {
            $scope.listModel = result;
            $scope.isLoading = false;
        });


    }]);
//Todo: remove all validation error to a common js.
function showErrors(toaster, err) {

    if (err.data.ExceptionMessage) {

        toaster.pop("error", "Error", err.data.ExceptionMessage);
    }
    if (err.data.ModelState) {
        var msg = "";
        for (var key in err.data.ModelState) {
            msg += err.data.ModelState[key] + "\n";
        }

        toaster.pop("error", "Error", msg);
    }
}