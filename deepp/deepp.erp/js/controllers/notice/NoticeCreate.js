'use strict';
/* Controllers */
// Notice controller
app.controller('NoticeCreateCtrl', ['$scope', '$state', '$http', 'NoticeService', 'modalService', 'FileUploader', '$filter', 'toaster',
    function ($scope, $state, $http, NoticeService, modalService, FileUploader, $filter, toaster) {

        $scope.entity = { Name: "Notice" };
        $scope.isLoading = true;
        $scope.noticeModel = null;
        $scope.uploader = null;
        NoticeService.new(function (result) {
            $scope.noticeModel = result;
            $scope.noticeModel.IsActive = 1;
            $scope.isLoading = false;
        });

        //------------------------------

        var imageApi = apiUrlPrefix + "api/image/multiple";

        var uploader = $scope.uploader = new FileUploader({
            url: imageApi,
            autoUpload: false,
        });

        //-----------------------------

        $scope.AddNotice = function () {

            var length = $scope.uploader.queue.length;

            if (length != 0) {
                uploader.uploadAll();
                $scope.uploader.onCompleteAll = function () {
                    NoticeService.save($scope.noticeModel, function (result) {
                        $scope.uploader = null;
                        $state.go('app.notice.noticeList');
                        toaster.pop("success", "Success", $scope.entity.Name + " created.");
                    }, function (err) {
                        showErrors(toaster, err);
                    }
                   );
                }
            } else {

                NoticeService.save($scope.noticeModel, function (result) {
                    $scope.uploader = null;
                    $state.go('app.notice.noticeList');
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
