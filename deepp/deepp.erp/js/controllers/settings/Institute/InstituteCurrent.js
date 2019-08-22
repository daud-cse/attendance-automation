'use strict';
/* Controllers */
// Institute controller
app.controller('SettingsInstituteCurrentCtrl', ['$scope', '$http', 'InstituteService', '$filter', 'toaster', '$stateParams', 'FileUploader', '$modal',
    function ($scope, $http, InstituteService, $filter, toaster, $stateParams, FileUploader,$modal) {

    // init
    $scope.entity = { Name: "Institute" };
    $scope.isLoading = true;
    $scope.showUploadPanel = true;

        // apiUrlPrefix
    $scope.apiUrlPrefix = apiUrlPrefix;

    ///---
    // Get all data
    $scope.GetNew = function () {

        InstituteService.Current( function (institute) {
            $scope.Institute = institute; // Student.query();
            $scope.isLoading = false;
         
            $scope.showUploadPanel = false;
             
        });
    };

    // inline Add /Edit
    $scope.GetNew();
   
    $scope.Update = function () {

        if ($scope.uploader.queue.length > 0) {
            uploader.uploadAll();
        } else if ($scope.uploaderBanner.queue.length > 0) {
            uploaderBanner.uploadAll();
        } else {
            updateInstitute();
        }
        

    };
    $scope.UpdateSliderImage = function () {
       
       // alert($scope.uploaderSlider.queue.length)
        if ($scope.uploaderSlider.queue.length > 0) {
            uploaderSlider.uploadAll();
        }  else {
            updateInstituteSliderImage();
        }


    };
    function updateInstitute() {
        $scope.isLoading = true;

        var institute=  angular.copy($scope.Institute);
        institute.GlobalCountry = null;
        institute.GlobalDistrict = null;
        institute.GlobalDivision = null;
        //if (institute.GlobalDivisionId == null) {
        //    institute.GlobalDivision = null;
        //} else {
        //    institute.GlobalDivision.GlobalDistricts = null;
        //    institute.GlobalDivision.GlobalCountry = null;
        //}

        institute.GlobalInstituteType = null;
        institute.GlobalSubDistrict = null;
        institute.Package = null;

        InstituteService.updateCurrent({ id: $scope.Institute.Id }, institute, function (result) {
            toaster.pop("success", "Success", $scope.entity.Name + " updated.");
            $scope.GetNew();
        }, function (err) { showErrors(toaster, err); });
    }

    function updateInstituteSliderImage() {
        $scope.isLoading = true;
        var institute = angular.copy($scope.Institute);
        institute.GlobalCountry = null;
        institute.GlobalDistrict = null;
        institute.GlobalDivision = {};
        if (institute.GlobalDivisionId == null) {
            institute.GlobalDivision = null;
        } else {
            institute.GlobalDivision.GlobalDistricts = null;
            institute.GlobalDivision.GlobalCountry = null;
        }
    
        institute.GlobalDivision = null;
        institute.GlobalInstituteType = null;
        institute.GlobalSubDistrict = null;
        institute.Package = null;
        InstituteService.updateSlider({ id: $scope.Institute.Id }, institute, function (result) {
            toaster.pop("success", "Success", $scope.entity.Name + " updated.");
            $scope.GetNew();
        }, function (err) { showErrors(toaster, err); });
        };
        // logo image
    var uploader = $scope.uploader = new FileUploader({
        url: apiUrlPrefix+'api/image/institute',
        formData: {'title':"logo"}
    });

    uploader.filters.push({
        name: 'customFilter',
        fn: function (item /*{File|FileLikeObject}*/, options) {
            
            return this.queue.length < 1;
        }
    });
    uploader.onCompleteAll = function () {
        if ($scope.uploaderBanner.queue.length > 0) {
            uploaderBanner.uploadAll();
        } else {
            updateInstitute();
        }
    };
    // banner image
    var uploaderBanner = $scope.uploaderBanner = new FileUploader({
        url: apiUrlPrefix+'api/image/institute',
        formData: { 'title': "banner" }
    });

    uploaderBanner.filters.push({
        name: 'customFilter',
        fn: function (item /*{File|FileLikeObject}*/, options) {

            return this.queue.length < 1;
        }
    });
    uploaderBanner.onCompleteAll = function () {
        updateInstitute();
    };
        // slider image
    var uploaderSlider = $scope.uploaderSlider = new FileUploader({
        url: apiUrlPrefix+'api/image/multiple?resize=false'
        
    });

    uploaderSlider.filters.push({
        name: 'customFilter',
        fn: function (item /*{File|FileLikeObject}*/, options) {

            return this.queue.length < 10;
        }
    });
    uploaderSlider.onCompleteAll = function () {
         
        updateInstituteSliderImage();
        uploaderSlider.clearQueue();
        $scope.showUploadPanel = false;
        
    };
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
                var index = $scope.Institute.ImagesList.indexOf($scope.selectedImage);
                $scope.Institute.ImagesList.splice(index, 1);
            }

        }, function () {
            console.log(" modal");
        });
    };

    }]);

app.controller('ModalInstanceCtrl', [
    '$scope', '$modalInstance', 'items', 'Image', 'modalService', function ($scope, $modalInstance, items, Image, modalService) {
        $scope.items = items;
        $scope.selectedImage = Image;

        // apiUrlPrefix
        $scope.apiUrlPrefix = apiUrlPrefix;

        $scope.ok = function () {
            //console.log($scope.selectedImage);
            var data = { Image: $scope.selectedImage, IsRemove: false };
            $modalInstance.close(data);
        };
        $scope.remove = function () {
            // console.log($scope.selectedImage);
            var modalOptions = {
                closeButtonText: 'No',
                actionButtonText: 'Yes',
                headerText: 'Remove Image',
                bodyText: 'Are you sure you want to remove this image?',
                type: 'danger'
            };
            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {

                    var data = { Image: $scope.selectedImage, IsRemove: true };
                    $modalInstance.close(data);


                }
            });

        };
        $scope.cancel = function () {
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