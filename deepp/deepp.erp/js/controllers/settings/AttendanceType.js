'use strict';
/* Controllers */
// AttendanceType controller
app.controller('SettingsAttendanceTypeCtrl', ['$scope', '$http', 'AttendanceTypeService', '$filter', 'toaster', function ($scope, $http, AttendanceTypeService, $filter, toaster) {

    // init
    $scope.entity = { Name: "AttendanceType" };
    $scope.isLoading = true;
    ///---- paging and search/sort
    $scope.sortingOrder = "Id";
    $scope.reverse = true;
    $scope.filteredItems = [];
    $scope.groupedItems = [];
    $scope.itemsPerPage = 50;
    $scope.pagedItems = [];
    $scope.currentPage = 0;
    $scope.items = [];

    //inline add/edit
    $scope.newField = {};
    $scope.editing = {};
    $scope.isNew = false;
    $scope.isEdit = false;
    $scope.isLoading = true;
    $scope.isprogressbar = true;
    
     
    ////alert("dddd");
    //$scope.New = function () {
    //   // alert("dddd");
       

    //};

    AttendanceTypeService.New(function (AttendanceTypes) {
        $scope.newAttendanceType = AttendanceTypes;
        //$scope.isNew = AttendanceTypes;

    }, function (err) { showErrors(toaster, err); });
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
        AttendanceTypeService.query(function (AttendanceTypes) {
            $scope.AttendanceTypes = AttendanceTypes; // AttendanceType.query();
            $scope.items = AttendanceTypes;

            console.log(resolveReferences(AttendanceTypes));
            //console.log("----------");
            //console.log(JSON.decycle(AttendanceTypes));
            //console.log("----------");
            //console.log(JSON.retrocycle(AttendanceTypes));

            // $scope.isNew = AttendanceTypes;
            $scope.isNew = false;
            $scope.query = "";
            $scope.search();
           // $scope.isLoading = false;
            $scope.isLoading = true;
            $scope.isprogressbar = false;
        });
    };

    // inline Add /Edit
    $scope.GetAll();
    $scope.AttendanceType = GetNewModel();
    $scope.Add = function (item) {
        
        AttendanceTypeService.save(item, function (result) {

            $scope.AttendanceType = GetNewModel();
            $scope.isNew = false;
            $scope.GetAll();
            toaster.pop("success", "Success", $scope.entity.Name + " created.");
        }, function (err) {
            showErrors(toaster, err);
        }
        );
    };
    $scope.Edit = function (item) {
        $scope.editing = $scope.pagedItems[$scope.currentPage].indexOf(item);
        $scope.isEdit = true;
        $scope.isNew = false;
        $scope.isLoading = false;
        $scope.selected = angular.copy(item);
    };

    $scope.Update = function (item) {
        item.Colour = null;
        AttendanceTypeService.update({ id: item.Id }, item, function (result) {
            toaster.pop("success", "Success", $scope.entity.Name + " updated.");
            $scope.pagedItems[$scope.currentPage][$scope.editing] = angular.copy($scope.selected);
            $scope.selected = {};
            $scope.isEdit = false;
            $scope.isNew = false;
            $scope.isLoading = true;
        }, function (err) { showErrors(toaster, err); });

    };

    ///
    // gets the template to ng-include for a table row / item
    $scope.getTemplate = function (item) {
      //alert("fd");
        if (item.Id === $scope.selected.Id) return "display";
        else

            return 'display';
    };

    $scope.reset = function () {
        $scope.pagedItems[$scope.currentPage][$scope.editing] = angular.copy($scope.selected);
        $scope.selected = {};
    };

}]);

function resolveReferences(json) {
    if (typeof json === 'string')
        json = JSON.parse(json);

    var byid = {}, // all objects by id
        refs = []; // references to objects that could not be resolved
    json = (function recurse(obj, prop, parent) {
        if (typeof obj !== 'object' || !obj) // a primitive value
            return obj;
        if (Object.prototype.toString.call(obj) === '[object Array]') {
            for (var i = 0; i < obj.length; i++)
                // check also if the array element is not a primitive value
                if (typeof obj[i] !== 'object' || !obj[i]) // a primitive value
                    return obj[i];
                else if ("$ref" in obj[i])
                    obj[i] = recurse(obj[i], i, obj);
                else
                    obj[i] = recurse(obj[i], prop, obj);
            return obj;
        }
        if ("$ref" in obj) { // a reference
            var ref = obj.$ref;
            if (ref in byid)
                return byid[ref];
            // else we have to make it lazy:
            refs.push([parent, prop, ref]);
            return;
        } else if ("$id" in obj) {
            var id = obj.$id;
            delete obj.$id;
            if ("$values" in obj) // an array
                obj = obj.$values.map(recurse);
            else // a plain object
                for (var prop in obj)
                    obj[prop] = recurse(obj[prop], prop, obj);
            byid[id] = obj;
        }
        return obj;
    })(json); // run it!

    for (var i = 0; i < refs.length; i++) { // resolve previously unknown references
        var ref = refs[i];
        ref[0][ref[1]] = byid[ref[2]];
        // Notice that this throws if you put in a reference at top-level
    }
    return json;
}
function GetNewModel() {
    return { Id: 0, IsActive: true, IsUsedForStudent: true, IsUsedForEmployee: true, Name: "", Flag: "", ColourId: 0, Description: "", InstituteId: 1 };

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