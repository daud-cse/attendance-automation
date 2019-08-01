app.controller('VoucherEntryListCtrl', ['$scope', '$state', '$http', 'VoucherEntryService', '$filter', 'toaster',
    function ($scope, $state, $http, VoucherEntryService, $filter, toaster) {

        $scope.entity = { Name: "Voucher Entry List" };
        $scope.voucherListModel = null;
        $scope.isLoading = true;
        var myTimeUnformatted = new Date();
        var date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
        VoucherEntryService.list({ startDateModel: date, endDateModel: date }, function (result) {
            $scope.voucherListModel = result;
            date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
            $scope.voucherListModel.startDateModel = date;
            $scope.voucherListModel.endDateModel = date;
            $scope.isLoading = false;
        });

        $scope.GetFilterResult = function () {
            $scope.isLoading = true;
            VoucherEntryService.list($scope.voucherListModel, function (allList) {
                $scope.voucherListModel = allList;
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