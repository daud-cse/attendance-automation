app.controller('CertificatePrintListCtrl', ['$scope', '$state', '$http', 'CertificatePrintService', '$filter', 'toaster',
    function ($scope, $state, $http, CertificatePrintService, $filter, toaster) {

        $scope.entity = { Name: "Certificate Print" };
        $scope.certificatePrintWithListModel = null;
        $scope.isLoading = true;
        CertificatePrintService.list(function (result) {
            $scope.certificatePrintWithListModel = result;
            $scope.isLoading = false;
        });

        $scope.GetFilterResult = function () {
            $scope.isLoading = true;
            CertificatePrintService.list($scope.certificatePrintWithListModel, function (allList) {
                $scope.certificatePrintWithListModel = allList;
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