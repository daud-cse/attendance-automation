'use strict';
/* Controllers */
app.controller('ChangePasswordCtrl', ['$scope', '$state', '$http', 'ChangePasswordService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, ChangePasswordService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Password" };
        $scope.isLoading = true;
        $scope.changePasswordModel = null;

        $scope.ChangePassword = function () {

            ChangePasswordService.passwordchange($scope.changePasswordModel, function (result) {
                $state.go('app.dashboard');
                toaster.pop("success", "Success", $scope.entity.Name + " Updated.");
            }, function (err) {
                if (err.status == 409) {
                    var modalOptions = {
                        closeButtonText: '',
                        actionButtonText: 'Ok',
                        headerText: err.statusText,
                        bodyText: err.ReasonPhrase,
                        type: 'danger'
                    };
                    modalService.showModal({}, modalOptions);
                }
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