'use strict';
/* Controllers */
app.controller('ResultPublishCreateCtrl', ['$scope', '$state', '$http', 'ResultPublishService', 'modalService', 'FileUploader', '$filter', 'toaster',
    function ($scope, $state, $http, ResultPublishService, modalService, FileUploader, $filter, toaster) {

        $scope.entity = { Name: "Result Publish" };
        $scope.isLoading = true;
        $scope.resultPublishModel = null;
        $scope.uploader = null;
        ResultPublishService.new(function (result) {
            $scope.resultPublishModel = result;
            $scope.resultPublishModel.IsActive = 1;
            $scope.isLoading = false;
        });

        //------------------------------

        var imageApi = apiUrlPrefix + "api/image/multiple";

        var uploader = $scope.uploader = new FileUploader({
            url: imageApi,
            autoUpload: false,
        });

        //-----------------------------

        $scope.AddResultPublish = function () {

            var length = $scope.uploader.queue.length;

            if (length != 0) {
                uploader.uploadAll();
                $scope.uploader.onCompleteAll = function () {
                    ResultPublishService.save($scope.resultPublishModel, function (result) {
                        $scope.uploader = null;
                        $state.go('app.resultPublish.list');
                        toaster.pop("success", "Success", $scope.entity.Name + " created.");
                    }, function (err) {
                        showErrors(toaster, err);
                    }
                   );
                }
            } else {

                ResultPublishService.save($scope.resultPublishModel, function (result) {
                    $scope.uploader = null;
                    $state.go('app.resultPublish.list');
                    toaster.pop("success", "Success", $scope.entity.Name + " created.");
                }, function (err) {
                    showErrors(toaster, err);
                });

            }
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
