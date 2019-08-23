'use strict';
/* Controllers */
// Student controller
app.controller('NoticeEditCtrl', ['$scope', '$state', '$http', '$stateParams', 'NoticeService', 'FileUploader', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams, NoticeService, FileUploader, modalService, $filter, toaster) {

        $scope.entity = { Name: "Notice" };
        $scope.isLoading = true;
        $scope.noticeModel = null;

        $scope.GetSingleNotice = function () {
            NoticeService.getsingle({ id: $stateParams.noticeId }, function (result) {
                $scope.noticeModel = result;
                $scope.noticeModel.IsActive = (result.IsActive == true) ? 1 : 0;
                $scope.uploader.prequeue = (result.ImageList != null) ? result.ImageList : [];

                $scope.isLoading = true;
            });
        }
        $scope.GetSingleNotice();

        var imageApi = apiUrlPrefix + "api/image/multiple";

        var uploader = $scope.uploader = new FileUploader({
            url: imageApi,
            autoUpload: false,
        });

        //-----------------------------

        $scope.UpdateNotice = function () {


            var length = $scope.uploader.queue.length;

            $scope.noticeModel.ExtImageIdList = [];
            if ($scope.uploader.prequeue.length !=0) {
                $.each($scope.uploader.prequeue, function (i, v) {
                        $scope.noticeModel.ExtImageIdList.push(angular.copy($scope.uploader.prequeue[i].Id));
                });
            }

            $scope.noticeModel.IsActive = ($scope.noticeModel.IsActive == 1) ? true : false;

            if (length != 0) {
                uploader.uploadAll();
                $scope.uploader.onCompleteAll = function () {
                    NoticeService.save($scope.noticeModel, function (result) {
                        $scope.uploader = null;
                        $state.go('app.notice.noticeList');
                        toaster.pop("success", "Success", $scope.entity.Name + " updated.");
                    }, function (err) {
                        showErrors(toaster, err);
                    }
                   );
                }
            } else {
                NoticeService.save($scope.noticeModel, function (result) {
                    $scope.uploader = null;
                    $state.go('app.notice.noticeList');
                    toaster.pop("success", "Success", $scope.entity.Name + " updated.");
                }, function (err) {
                    showErrors(toaster, err);
                });

            }
        };

        $scope.removeSingle = function (index) {
            //var modalOptions = {
            //    closeButtonText: 'No',
            //    actionButtonText: 'Yes',
            //    headerText: 'Remove Image',
            //    bodyText: 'Are you sure you want to remove this image?',
            //    type: 'danger'
            //};
            //modalService.showModal({}, modalOptions).then(function (result) {
            //    if (result === 'ok') {
                    $scope.uploader.prequeue.splice(index, 1);
            //    } 
                
            //});
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