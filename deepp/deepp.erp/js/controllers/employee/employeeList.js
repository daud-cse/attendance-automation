'use strict';
/* Controllers */
// Employee controller
app.controller('EmployeeListCtrl', ['$scope', '$http', 'EmployeeService', '$filter', 'toaster',

    function ($scope, $http, EmployeeService, $filter, toaster) {

        // init
        $scope.entity = { Name: "Employee" };
        $scope.isLoading = true;
        $scope.isSearchLoading = true;
        ///---- paging and search/sort
        $scope.sortingOrder = "EmployeeId";
        $scope.reverse = true;
        $scope.filteredItems = [];
        $scope.groupedItems = [];
        $scope.itemsPerPage = 10;
        $scope.pagedItems = [];
        $scope.currentPage = 0;
        $scope.items = [];

        //inline add/edit
        $scope.newField = {};
        $scope.editing = {};
        $scope.isNew = false;
        $scope.selected = {};

        // apiUrlPrefix
        $scope.apiUrlPrefix = apiUrlPrefix;

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
                if (searchMatch(item.Name, $scope.query))
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
            EmployeeService.query(function (employees) {
                $scope.employees = employees;
                $scope.items = employees;
                $scope.query = "";
                $scope.search();
                $scope.isLoading = false;
            });
        };
        $scope.GetEmployeeSearchModel = function () {
            EmployeeService.NewEmployee(function (employee) {
                var id = employee.AcademicBranchList[0].Key;
                $scope.VmEmployee = employee;
                $scope.VmEmployee.AcademicBranchId = id;
                $scope.isSearchLoading = false;
            });
        };
        $scope.GetEmployeeSearchModel();
        $scope.GetAll();

        $scope.SearchEmployee = function () {
            $scope.isLoading = true;
            
            EmployeeService.query({ searchText: $scope.searchText, branchId: $scope.VmEmployee.AcademicBranchId }, function (employees) {
                $scope.employees = employees;  
               
                $scope.items = employees;
                $scope.query = "";
                $scope.search();
                $scope.isLoading = false;
            });
        };
        $scope.getTemplate = function (item) {

            if (item.EmployeeId === $scope.selected.EmployeeId) return "display";
            else

                return 'display';
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