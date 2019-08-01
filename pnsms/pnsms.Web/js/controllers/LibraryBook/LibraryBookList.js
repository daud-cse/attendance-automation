'use strict';
/* Controllers */
app.filter('propsFilter', function() {
    return function(items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function(item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});
app.controller('LibraryBookCtrl', ['$scope', '$http', 'LibraryBookService', '$filter', 'toaster', 'LibraryBookAuthoreService', '$modal',
    function ($scope, $http, LibraryBookService, $filter, toaster, LibraryBookAuthoreService, $modal) {

    // init
    $scope.entity = { Name: "Library Book" };
    $scope.isLoading = true;
    $scope.authorLoading = true;
    ///---- paging and search/sort
    $scope.sortingOrder = "Id";
    $scope.reverse = true;
    $scope.filteredItems = [];
    $scope.groupedItems = [];
    $scope.itemsPerPage = 20;
    $scope.pagedItems = [];
    $scope.currentPage = 0;
    $scope.items = [];

    //inline add/edit
    $scope.newField = {};
    $scope.editing = {};
    $scope.isNew = false;
    $scope.selected = {};

    var searchMatch = function (haystack, needle) {
        if (!needle) {
            return true;
        }
        return haystack.toLowerCase().indexOf(needle.toLowerCase()) !== -1;
    };

    // init the filtered items
    $scope.search = function () {
        //console.log($scope.items);
        $scope.filteredItems = $filter('filter')($scope.items, function (item) {

            // Todo: implement multiple filter.
            ///for (var attr in item) {
            if (searchMatch(item["Name"], $scope.query))
                return true;
            ///}
            return false;
        });
        // take care of the sorting order
        if ($scope.sortingOrder !== '') {
            $scope.filteredItems = $filter('orderBy')($scope.filteredItems, $scope.sortingOrder, $scope.reverse);
        }
        $scope.currentPage = 0;
        // now group by pages
        $scope.groupToPages();
    };

    // calculate page in place
    $scope.groupToPages = function () {
        $scope.pagedItems = [];
        if ($scope.filteredItems) {
            for (var i = 0; i < $scope.filteredItems.length; i++) {
                if (i % $scope.itemsPerPage === 0) {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)] = [$scope.filteredItems[i]];
                } else {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)].push($scope.filteredItems[i]);
                }
            }
        }

    };

    $scope.range = function (start, end) {
        var ret = [];
        if (!end) {
            end = start;
            start = 0;
        }
        for (var i = start; i < end; i++) {
            ret.push(i);
        }
        return ret;
    };

    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pagedItems.length - 1) {
            $scope.currentPage++;
        }
    };

    $scope.setPage = function () {
        $scope.currentPage = this.n;
    };

    // functions have been describe process the data for display
    // $scope.search();

    // change sorting order
    $scope.sort_by = function (newSortingOrder) {
        if ($scope.sortingOrder == newSortingOrder)
            $scope.reverse = !$scope.reverse;

        $scope.sortingOrder = newSortingOrder;

        // icon setup
        $('tr span i').each(function () {
            // icon reset
            $(this).removeClass().addClass('fa fa-sort');
        });
        if ($scope.reverse)
            $('span.' + newSortingOrder + ' i').removeClass().addClass('fa fa-sort-asc');
        else
            $('span.' + newSortingOrder + ' i').removeClass().addClass('fa fa-sort-desc');
    };

    ///---
    // Get all data
    $scope.GetAll = function () {
        
        LibraryBookService.query(function (libraryBooks) {
            $scope.LibraryBooks = libraryBooks;  
            $scope.items = libraryBooks;
            $scope.query = "";
            $scope.search();
            $scope.isLoading = false;
        });
    };
    // Get all  Authores
    $scope.GetAllAuthores = function () {
        $scope.authorLoading = true;
        LibraryBookAuthoreService.query({isActive:true},function (libraryBookAuthores) {
            $scope.LibraryBookAuthores = libraryBookAuthores;
            $scope.multipleDemo.selectedPeopleWithGroupBy = [];
            $scope.authorLoading = false;
        });
    };
    // inline Add /Edit
    $scope.GetAll();
    $scope.LibraryBookAuthores = [];
    $scope.GetAllAuthores();
    $scope.LibraryBook = GetNewModel();
    $scope.AddNew = function () {
        $scope.LibraryBook = GetNewModel();
       
        $scope.multipleDemo.selectedPeopleWithGroupBy = [];
        $scope.isNew = true;
    };

    $scope.Add = function (item) {
        $scope.isLoading = true;
        if (item.Id > 0) {

            $scope.Update(item);
        } else {
            //$scope.MapRightsOfLibraryBooks();
            item.LibraryBookAuthoreList = $scope.multipleDemo.selectedPeopleWithGroupBy;//$scope.RightsOfLibraryBooks;

            LibraryBookService.save(item, function (result) {

                $scope.LibraryBook = GetNewModel();
                $scope.isNew = false;
                $scope.GetAll();
                toaster.pop("success", "Success", $scope.entity.Name + " created.");
            }, function (err) {
                showErrors(toaster, err);
            }
       );
        }

    };
    $scope.Edit = function (item) {
        $scope.isLoading = true;
     
        LibraryBookService.get({ id: item.Id }, function (book) {
            $scope.multipleDemo.selectedPeopleWithGroupBy = [];
            var rightsList = angular.copy(book.LibraryBookAuthoreList);
           angular.forEach(rightsList, function (obj) {
               var selectedRights = $filter('filter')($scope.LibraryBookAuthores, { Id: obj.Id })[0];
                if (selectedRights != null) {
                    $scope.multipleDemo.selectedPeopleWithGroupBy.push(selectedRights);
                }
                
            });
           // $scope.multipleDemo.selectedPeopleWithGroupBy = angular.copy(role.RightsList);
           $scope.LibraryBook = book;
           $scope.isLoading = false;
           $scope.isNew = true;
        });
       
        $scope.editing = $scope.pagedItems[$scope.currentPage].indexOf(item);
        $scope.selected = angular.copy(item);
    };
    $scope.Update = function (item) {
        item.LibraryBookAuthoreList = $scope.multipleDemo.selectedPeopleWithGroupBy;//$scope.RightsOfRoles;
        LibraryBookService.update({ id: item.Id }, item, function (result) {
            toaster.pop("success", "Success", $scope.entity.Name + " updated.");
          
            $scope.isNew = false;
            $scope.isLoading = false;
            $scope.LibraryBook = GetNewModel();
            $scope.pagedItems[$scope.currentPage][$scope.editing] = item;
            var iedit = $scope.items.indexOf($scope.selected);
            $scope.items[iedit] = item;
            $scope.selected = {};
        }, function (err) { showErrors(toaster, err); });

    };

    ///
    // gets the template to ng-include for a table row / item
    $scope.getTemplate = function (item) {

        if (item.Id === $scope.selected.Id) return "edit";
        else

            return 'display';
    };

    $scope.reset = function () {
        $scope.pagedItems[$scope.currentPage][$scope.editing] = angular.copy($scope.selected);
        $scope.selected = {};
    };
     
    $scope.multipleDemo = {};
    $scope.multipleDemo.selectedPeopleWithGroupBy = [$scope.LibraryBookAuthores[0]];

    $scope.selectedRights = [];
    $scope.RightsOfRoles = [];
    $scope.MapRightsOfRoles = function () {
        $scope.RightsOfRoles = [];
        //var length = $scope.multipleDemo.selectedPeopleWithGroupBy.length;
        //for (var i = 0; i < length; i++) {
        //    $scope.RightsOfRoles.push({ RightId: $scope.multipleDemo.selectedPeopleWithGroupBy[i].Id });
        //}
        
        angular.forEach($scope.multipleDemo.selectedPeopleWithGroupBy, function (obj) {
            $scope.RightsOfRoles.push({ RightId: obj.Id });
        });
    };

        // Modal
    $scope.AuthorModel = GetNewAuthorModel();
    $scope.items = ['item1', 'item2', 'item3'];
    $scope.selectedImage = null;
    $scope.AddNewAuthor = function () {
        $scope.AuthorModel = GetNewAuthorModel();
        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            size: 'sm',
            resolve: {
                items: function () {
                    return $scope.items;
                },
                Author: function () {
                    return $scope.AuthorModel;
                }
            }
        });

        modalInstance.result.then(function (data) {

            $scope.AuthorModel = data.Author;
            LibraryBookAuthoreService.save($scope.AuthorModel, function (result) {
                $scope.GetAllAuthores();
                 
                toaster.pop("success", "Success",  "Author created.");
            }, function (err) {
                showErrors(toaster, err);
            }
       );
            if (data.IsRemove == true) {
                //var index = $scope.VmGallery.Images.indexOf($scope.selectedImage);
                //$scope.VmGallery.Images.splice(index, 1);
            }
            console.log($scope.AuthorModel);
        }, function () {
            console.log(" modal");
        });
    };
}]);
app.controller('ModalInstanceCtrl', [
    '$scope', '$modalInstance', 'items', 'Author', 'modalService', function ($scope, $modalInstance, items, Author, modalService) {
        $scope.items = items;
        $scope.selectedAuthor = Author;


        $scope.ok = function () {
            //console.log($scope.selectedImage);
            var data = { Author: $scope.selectedAuthor, IsRemove: false };
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
function GetNewModel() {
    return { Id: 0, IsActive: true, Name: "", Quantity: 1,ISBN:"" };

}
function GetNewAuthorModel() {
    return { Id: 0, IsActive: true, Name: ""   };

}
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