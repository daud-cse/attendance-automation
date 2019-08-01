'use strict';
/* Controllers */
app.controller('VoucherEntryNewCtrl', ['$scope', '$state', '$http', 'VoucherEntryService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, VoucherEntryService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Vocher Entry" };
        $scope.isLoading = true;
        $scope.VmVoucher = null;
        $scope.uploader = null;
        VoucherEntryService.new(function (result) {
            $scope.VmVoucher = result;
            $scope.VmVoucher.Voucher.IsActive = ($scope.VmVoucher.Voucher.IsActive == true) ? 1 : 0;
            $scope.isLoading = false;
        });

        //------------------------------
        $scope.AddVoucher = function () {
            //$scope.VmVoucher.Voucher.TotalAmount = $scope.total;
            $scope.VmVoucher.Voucher.IsActive = ($scope.VmVoucher.Voucher.IsActive == 1) ? true : false;
            VoucherEntryService.save($scope.VmVoucher, function (result) {
                $state.go('app.voucherEntry.list');
                    toaster.pop("success", "Success", $scope.entity.Name + " created.");
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
