'use strict';
app.controller('ResultPublishListCtrl', ['$scope', '$state', '$http', 'ResultPublishService', '$filter', 'toaster',
    function ($scope, $state, $http, ResultPublishService, $filter, toaster) {

        $scope.entity = { Name: "Result Publish" };
        $scope.resultPublishWithListModel = null;
        $scope.isLoading = true;
        ResultPublishService.list(function (result) {
            $scope.resultPublishWithListModel = result;
            $scope.isLoading = false;
        });

        $scope.GetFilterResult = function () {
            $scope.isLoading = true;
            ResultPublishService.list($scope.resultPublishWithListModel, function (allList) {
                $scope.resultPublishWithListModel = allList;
                $scope.isLoading = false;
            });

        };


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