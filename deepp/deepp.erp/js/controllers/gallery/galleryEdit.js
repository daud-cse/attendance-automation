'use strict';
/* Controllers */
// Gallery controller
app.controller('GalleryEditCtrl', ['$scope', '$http', 'GalleryService', '$filter', 'toaster', '$stateParams', 'modalService', '$state', 'FileUploader', '$location', '$modal',

    function ($scope, $http, GalleryService, $filter, toaster, $stateParams, modalService, $state, FileUploader, $location, $modal) {

        // init
        $scope.entity = { Name: "Gallery" };
        $scope.isLoading = true;
        $scope.VmGallery = null;
        $scope.showUploadPanel = false;

        // apiUrlPrefix
        $scope.apiUrlPrefix = apiUrlPrefix;

        //--------------- Gallery  ---------------
        $scope.GetGallery = function () {
            GalleryService.get({ id: $stateParams.galleryId, 'foobar': new Date().getTime() }, function (VmGallery) {
                $scope.VmGallery = VmGallery; // Student.query();
 
                $scope.isLoading = false;
                $scope.showUploadPanel = false;
                
                $scope.defaultImage = apiUrlPrefix+"api/image/refcode?id=" + $scope.VmGallery.Id + "&refcode=Galley_Default&isThumbnail=true&foobar=" + new Date().getTime();
                
            });
        };

        $scope.GetGallery();
        $scope.AddGallery = function () {
            var modalOptions = {
                closeButtonText: 'No',
                actionButtonText: 'Yes',
                headerText: 'Save updates',
                bodyText: 'Are you sure you want to save changes?',
                type: 'warning'
            };
            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {

                    $scope.isLoading = true;
                    console.log($scope.uploader.queue.length);
                    if ($scope.uploader.queue.length > 0) {
                        uploader.uploadAll();
                    } else {
                        var gallery = angular.copy($scope.VmGallery);
                        gallery.Event = null;
                        //gallery.Images = null;

                        GalleryService.update({ id: gallery.Id }, gallery, function (result) {
                            toaster.pop("success", "Success", $scope.entity.Name + " updated.");
                            // $location.path("/app/gallery/" + gallery.Id);
                            $scope.GetGallery();

                        }, function (err) {
                            showErrors(toaster, err);
                        });
                    }



                }
            });



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

        // Upload CALLBACKS

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
            uploader.clearQueue();
            var gallery = angular.copy($scope.VmGallery);
            gallery.Event = null;
            gallery.Images = null;
            GalleryService.update({ id: gallery.Id }, gallery, function (result) {
                toaster.pop("success", "Success", $scope.entity.Name + " updated.");
                // $location.path("/app/gallery/" + gallery.Id);
                $scope.GetGallery();

            }, function (err) {
                showErrors(toaster, err);
            });
        };
        // Modal
        $scope.items = ['item1', 'item2', 'item3'];
        $scope.selectedImage = null;
        $scope.open = function (image) {
            $scope.selectedImage = image;
            var modalInstance = $modal.open({
                templateUrl: 'myModalContent.html',
                controller: 'ModalInstanceCtrl',
                size: 'lg',
                resolve: {
                    items: function () {
                        return $scope.items;
                    },
                    Image: function () {
                        return $scope.selectedImage;
                    }
                }
            });

            modalInstance.result.then(function (data) {
                
                $scope.selectedImage = data.Image;
                if (data.IsRemove == true) {
                    var index = $scope.VmGallery.Images.indexOf($scope.selectedImage);
                    $scope.VmGallery.Images.splice(index, 1);
                }
                
            }, function () {
                console.log(" modal");
            });
        };
        //--  check box required
        $scope.checkBoxChange = function (img) {
            $scope.VmGallery.Image = img;
            //  return $('input[name=cbBranch]:checked').length;
        };
    }]);

app.controller('ModalInstanceCtrl', [
    '$scope', '$modalInstance', 'items', 'Image', 'modalService', function($scope, $modalInstance, items, Image, modalService) {
        $scope.items = items;
        $scope.selectedImage = Image;
        

        $scope.ok = function() {
            //console.log($scope.selectedImage);
            var data = { Image: $scope.selectedImage, IsRemove: false };
            $modalInstance.close(data);
        };
        $scope.remove = function() {
            // console.log($scope.selectedImage);
            var modalOptions = {
                closeButtonText: 'No',
                actionButtonText: 'Yes',
                headerText: 'Remove Image',
                bodyText: 'Are you sure you want to remove this image?',
                type: 'danger'
            };
            modalService.showModal({}, modalOptions).then(function(result) {
                if (result === 'ok') {

                    var data = { Image: $scope.selectedImage, IsRemove: true };
                    $modalInstance.close(data);


                }
            });

        };
        $scope.cancel = function() {
            $modalInstance.dismiss('cancel');
        };
    }
]);

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