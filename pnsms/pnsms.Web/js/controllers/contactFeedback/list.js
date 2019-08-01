'use strict';
/* Controllers */
// Teacher controller
app.controller('ContactFeedbackListCtrl', ['$scope', '$http', 'ContactFeedbackService', '$filter', 'toaster',
function ($scope, $http, ContactFeedbackService, $filter, toaster) {

    $scope.entity = { Name: "Contact & Feedback" };
    $scope.vmSearchModel = null;
    $scope.isLoading = true;
    var myTimeUnformatted = new Date();
    var date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
    $scope.isLoading = false;
    ContactFeedbackService.list({ startDateModel: date, endDateModel: date }, function (result) {
        $scope.vmSearchModel = result;
        date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
        $scope.vmSearchModel.startDateModel = date;
        $scope.vmSearchModel.endDateModel = date;
        $scope.isLoading = false;
    });

    $scope.GetFilterResult = function () {
        $scope.isLoading = true;
        ContactFeedbackService.list($scope.vmSearchModel, function (list) {
            $scope.vmSearchModel = list;
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