'use strict';
/* Controllers */
app.controller('ResultPublishEditCtrl', ['$scope', '$state', '$http', '$stateParams', 'ResultPublishService', 'FileUploader', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams, ResultPublishService, FileUploader, modalService, $filter, toaster) {

        $scope.entity = { Name: "Result Publish" };
        $scope.isLoading = true;
        $scope.resultPublishModel = null;

        $scope.GetSingleResultPublish = function () {
            ResultPublishService.getsingle({ id: $stateParams.resultPublishId }, function (result) {
                $scope.resultPublishModel = result;
                $scope.resultPublishModel.IsActive = (result.IsActive == true) ? 1 : 0;
                $scope.uploader.prequeue = (result.ImageList != null) ? result.ImageList : [];

                $scope.isLoading = true;
            });
        }
        $scope.GetSingleResultPublish();

        var imageApi = apiUrlPrefix + "api/image/multiple";

        var uploader = $scope.uploader = new FileUploader({
            url: imageApi,
            autoUpload: false,
        });

        //-----------------------------

        $scope.UpdateResultPublish = function () {


            var length = $scope.uploader.queue.length;

            $scope.resultPublishModel.ExtImageIdList = [];
            if ($scope.uploader.prequeue.length !=0) {
                $.each($scope.uploader.prequeue, function (i, v) {
                    $scope.resultPublishModel.ExtImageIdList.push(angular.copy($scope.uploader.prequeue[i].Id));
                });
            }

            $scope.resultPublishModel.IsActive = ($scope.resultPublishModel.IsActive == 1) ? true : false;

            if (length != 0) {
                uploader.uploadAll();
                $scope.uploader.onCompleteAll = function () {
                    ResultPublishService.save($scope.resultPublishModel, function (result) {
                        $scope.uploader = null;
                        $state.go('app.resultPublish.list');
                        toaster.pop("success", "Success", $scope.entity.Name + " updated.");
                    }, function (err) {
                        showErrors(toaster, err);
                    }
                   );
                }
            } else {
                ResultPublishService.save($scope.resultPublishModel, function (result) {
                    $scope.uploader = null;
                    $state.go('app.resultPublish.list');
                    toaster.pop("success", "Success", $scope.entity.Name + " updated.");
                }, function (err) {
                    showErrors(toaster, err);
                });

            }
        };

        $scope.removeSingle = function (index) {
                    $scope.uploader.prequeue.splice(index, 1);
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