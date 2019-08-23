 
app.factory('modalService', ['$modal', function ($modal) {
    var modalDefaults = {
        backdrop: true,
        keyboard: true,
        modalFade: true,
        templateUrl: apiUrlPrefix+'tpl/modal.html',
        windowClass: 'Css-Center-Modal'
    };

    var modalOptions = {
        closeButtonText: 'Close',
        actionButtonText: 'OK',
        headerText: 'Proceed?',
        bodyText: 'Perform this action?',
        type: 'info'

    };

    //this.showModal = function (customModalDefaults, customModalOptions) {
    //    if (!customModalDefaults) customModalDefaults = {};
    //    customModalDefaults.backdrop = 'static';
    //    return this.show(customModalDefaults, customModalOptions);
    //};
    //---------------------- get error message ------------------------
    this.getErrMessage = function (error) {

        switch (error.code) {
            case 'ER_DUP_ENTRY':
                return 'Duplicate entry, this data already exists.';
                break;
            case 'ER_ROW_IS_REFERENCED_':
                return ('Cannot delete or update a parent row: a foreign key constraint fails.')
                break;
            case 'ER_NO_REFERENCED_ROW_':
                return ('Cannot add or update a child row: a foreign key constraint fails');
                break;
            default:
                return ('Error: ' + error.code + ", Status:" + error.errno);
        }
    };
    return {
        showModal : function (customModalDefaults, customModalOptions) {
            if (!customModalDefaults) customModalDefaults = {};
            customModalDefaults.backdrop = 'static';
            return this.show(customModalDefaults, customModalOptions);
        },
        show : function (customModalDefaults, customModalOptions) {
            //Create temp objects to work with since we're in a singleton service
            var tempModalDefaults = {};
            var tempModalOptions = {};

            //Map angular-ui modal custom defaults to modal defaults defined in this service
            angular.extend(tempModalDefaults, modalDefaults, customModalDefaults);

            //Map modal.html $scope custom properties to defaults defined in this service
            angular.extend(tempModalOptions, modalOptions, customModalOptions);

            if (!tempModalDefaults.controller) {
                tempModalDefaults.controller = function ($scope, $modalInstance) {
                    $scope.modalOptions = tempModalOptions;
                    $scope.modalOptions.ok = function (result) {
                        $modalInstance.close('ok');
                    };
                    $scope.modalOptions.close = function (result) {
                        $modalInstance.close('cancel');
                    };
                };

                tempModalDefaults.controller.$inject = ['$scope', '$modalInstance'];
            }

            return $modal.open(tempModalDefaults).result;
        }
    };
    
}]);

    var modalService = function ($modal) {
        
        
    };
    
   

 
