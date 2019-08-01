'use strict';
/* Controllers */
app.controller('CertificatePrintCreateCtrl', ['$scope', '$state', '$http', 'CertificatePrintService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, CertificatePrintService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Certificate" };
        $scope.isLoading = true;
        $scope.certificatePrintModel = null;
        CertificatePrintService.new($scope.certificatePrintModel, function (result) {
            $scope.certificatePrintModel = result;
            $scope.isLoading = false;
        });

        //-----------------------------

        $scope.AddCertificatePrint = function () {

            CertificatePrintService.save($scope.certificatePrintModel, function (result) {
                $state.go('app.certificatePrint.list');
                toaster.pop("success", "Success", $scope.entity.Name + " created.");
            }, function (err) {
                showErrors(toaster, err);
            }
           );
        };

        $scope.Print = function () {
            $(".ta-scroll-window.ng-scope.ta-text.ta-editor.form-control").jqprint();
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
