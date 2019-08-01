app.factory('feesAcademicClassService', function ($http) {
    var serviceBase = apiUrlPrefix;
    function save(feesAcademicClass) {
        return $http.post(serviceBase + "api/feesacademicclass/save", billhead);
    }
    function getfeesAcademicClass() {
        return $http.get(serviceBase + "api/feesacademicclass/get").then(function (result) {
            return result.data;
        });
    }
    function edit(feesAcademicClass) {
        return $http.post(serviceBase + "api/feesacademicclass/edit", feesAcademicClass).then(function (result) {
            return result.data;
        });
    }
    function getreport(selectedOption, feesAcademicClassId) {
        return $http.get(serviceBase + "api/feescollection/getReport" + "?p_selectedOption=" + selectedOption.Key + "&p_feesAcademicClassId=" + feesAcademicClassId).then(function (result) {
            return result.data;
        });
    }
    return {
        save: save,
        getfeesAcademicClass: getfeesAcademicClass,
        edit: edit,
        getreport: getreport
    }
});