'use strict';
/* Controllers */
app.controller('VoucherEntryDetailsCtrl', ['$scope', '$http', 'VoucherEntryService', '$filter', 'toaster', '$stateParams', 'modalService', '$state',
    function ($scope, $http, VoucherEntryService, $filter, toaster, $stateParams, modalService, $state) {

        $scope.entity = { Name: "Voucher" };
        $scope.VoucherModel = null;

        $scope.GetSingle = function () {
            VoucherEntryService.getsingle({ id: $stateParams.voucherEntryId }, function (result) {
                $scope.Status = (result.Voucher.IsActive == true) ? "Active" : "InActive";
                $scope.VoucherModel = result;
            });
        }
        $scope.GetSingle();

        $scope.Print = function () {
            $('#myDiv').jqprint();
        }

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




