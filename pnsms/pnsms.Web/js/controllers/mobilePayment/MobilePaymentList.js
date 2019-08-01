app.controller('MobilePaymentListCtrl', ['$scope', '$state', '$http', 'MobilePaymentService', '$filter', 'toaster',
    function ($scope, $state, $http, MobilePaymentService, $filter, toaster) {

        $scope.entity = { Name: "Mobile Payment List" };
        $scope.mPaymentListModel = null;
        $scope.isLoading = true;
        var myTimeUnformatted = new Date();
        var date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
        MobilePaymentService.list({ startDateModel: date, endDateModel: date }, function (result) {
            $scope.mPaymentListModel = result;
            date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
            $scope.mPaymentListModel.startDateModel = date;
            $scope.mPaymentListModel.endDateModel = date;
            $scope.isLoading = false;
        });

        $scope.GetFilterResult = function () {
            $scope.isLoading = true;
            MobilePaymentService.list($scope.mPaymentListModel, function (mPaymentAllList) {
                $scope.mPaymentListModel = mPaymentAllList;
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