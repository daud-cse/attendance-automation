'use strict';
/* Controllers */
// Student controller
app.controller('VoucherEntryEditCtrl', ['$scope', '$state', '$http', '$stateParams', 'VoucherEntryService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams, VoucherEntryService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Vocher Entry" };
        $scope.isLoading = true;
        $scope.VmVoucher = null;

        $scope.GetVoucher = function () {
            VoucherEntryService.getsingle({ id: $stateParams.voucherEntryId }, function (result) {
                $scope.VmVoucher = result;
                $scope.VmVoucher.Voucher.IsActive = ($scope.VmVoucher.Voucher.IsActive == true) ? 1 : 0;
                $scope.isLoading = true;
            });
        }
        $scope.GetVoucher();

        //-----------------------------

        $scope.UpdateVoucher = function () {
            $scope.VmVoucher.Voucher.IsActive = ($scope.VmVoucher.Voucher.IsActive == 1) ? true : false;
            VoucherEntryService.update($scope.VmVoucher, function (result) {
                $state.go('app.voucherEntry.list');
                toaster.pop("success", "Success", $scope.entity.Name + " updated.");
            }, function (err) {
                showErrors(toaster, err);
            }
           );
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