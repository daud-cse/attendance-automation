'use strict';
/* Controllers */
// Student controller
app.controller('EventEditCtrl', ['$scope', '$state', '$http', '$stateParams', 'EventService', 'FileUploader', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams, EventService, FileUploader, modalService, $filter, toaster) {

        $scope.entity = { Name: "Event" };
        $scope.isLoading = true;
        $scope.eventModel = null;

        $scope.GetSingleEvent = function () {
            EventService.getsingle({ id: $stateParams.eventId }, function (result) {
                $scope.eventModel = result;
                $scope.eventModel.IsActive = (result.IsActive == true) ? 1 : 0;
                $scope.uploader.prequeue = (result.ImageList != null) ? result.ImageList : [];

                $scope.isLoading = true;
            });
        }
        $scope.GetSingleEvent();

        var imageApi = apiUrlPrefix + "api/image/multiple";

        var uploader = $scope.uploader = new FileUploader({
            url: imageApi,
            autoUpload: false,
        });

        //-----------------------------

        $scope.UpdateEvent = function () {


            var length = $scope.uploader.queue.length;

            $scope.eventModel.ExtImageIdList = [];
            if ($scope.uploader.prequeue.length != 0) {
                $.each($scope.uploader.prequeue, function (i, v) {
                    $scope.eventModel.ExtImageIdList.push(angular.copy($scope.uploader.prequeue[i].Id));
                });
            }

            $scope.eventModel.IsActive = ($scope.eventModel.IsActive == 1) ? true : false;

            if (length != 0) {
                uploader.uploadAll();
                $scope.uploader.onCompleteAll = function () {
                    EventService.save($scope.eventModel, function (result) {
                        $scope.uploader = null;
                        $state.go('app.event.eventList');
                        toaster.pop("success", "Success", $scope.entity.Name + " updated.");
                    }, function (err) {
                        showErrors(toaster, err);
                    }
                   );
                }
            } else {
                EventService.save($scope.eventModel, function (result) {
                    $scope.uploader = null;
                    $state.go('app.event.eventList');
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