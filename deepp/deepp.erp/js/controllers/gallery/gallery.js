'use strict';
/* Controllers */
// Gallery controller
app.controller('GalleryCtrl', ['$scope', '$http', 'GalleryService', '$filter', 'toaster', 'FileUploader', '$location',

    function ($scope, $http, GalleryService, $filter, toaster, FileUploader, $location) {

        // init
        $scope.entity = { Name: "Gallery" };
        $scope.isLoading = true;
        $scope.VmGallery = null;

        // apiUrlPrefix
        $scope.apiUrlPrefix = apiUrlPrefix;

        //--------------- Gallery ---------------
        $scope.GetGallery = function () {
            GalleryService.New(function (gallery) {
                $scope.VmGallery = gallery;
                $scope.isLoading = false;
              
            });

        };
        $scope.GetGallery();
        $scope.AddGallery = function () {
            if ($scope.uploader.queue.length > 0) {
                uploader.uploadAll();
            } else {
                var gallery = angular.copy($scope.VmGallery);

                GalleryService.save(gallery, function (result) {
                    toaster.pop("success", "Success", $scope.entity.Name + " created.");
                    $location.path("/app/gallery/edit/" + result.Id );
                }, function (err) {
                    showErrors(toaster, err);
                }
               );
            }
        };
        
        var uploader = $scope.uploader = new FileUploader({
            url: 'erp/api/image/multiple'
        });

        uploader.filters.push({
            name: 'customFilter',
            fn: function (item /*{File|FileLikeObject}*/, options) {
                return this.queue.length < 10;
            }
        });

        // CALLBACKS

        uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
            console.info('onWhenAddingFileFailed', item, filter, options);
        };
        uploader.onAfterAddingFile = function (fileItem) {
            console.info('onAfterAddingFile', fileItem);
        };
        uploader.onAfterAddingAll = function (addedFileItems) {
            console.info('onAfterAddingAll', addedFileItems);
        };
        uploader.onBeforeUploadItem = function (item) {
            console.info('onBeforeUploadItem', item);
        };
        uploader.onProgressItem = function (fileItem, progress) {
            console.info('onProgressItem', fileItem, progress);
        };
        uploader.onProgressAll = function (progress) {
            console.info('onProgressAll', progress);
        };
        uploader.onSuccessItem = function (fileItem, response, status, headers) {
            console.info('onSuccessItem', fileItem, response, status, headers);
        };
        uploader.onErrorItem = function (fileItem, response, status, headers) {
            console.info('onErrorItem', fileItem, response, status, headers);
        };
        uploader.onCancelItem = function (fileItem, response, status, headers) {
            console.info('onCancelItem', fileItem, response, status, headers);
        };
        uploader.onCompleteItem = function (fileItem, response, status, headers) {
            console.info('onCompleteItem', fileItem, response, status, headers);
        };
        uploader.onCompleteAll = function () {
            console.info('onCompleteAll');
            var gallery = angular.copy($scope.VmGallery);

            GalleryService.save(gallery, function (result) {
                toaster.pop("success", "Success", $scope.entity.Name + " created.");
                $location.path("/app/gallery/edit/" + result.Id );
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