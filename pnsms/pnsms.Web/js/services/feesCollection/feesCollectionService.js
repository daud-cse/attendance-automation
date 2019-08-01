app.factory('feesCollectionService', function ($http) {
    var serviceBase = apiUrlPrefix;
    function getstudent(studentId, month) {
        return $http.get(serviceBase + "api/feescollection/monthlyFees" + "?p_studentId=" + studentId + "&p_month=" + month)
    }
    function save(studentFees) {
        //$http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";
        return $http.post(serviceBase + "api/feescollection/save", studentFees);
    }
    function getreport(selectedOption, feesAcademicClassId) {
        return $http.get(serviceBase + "api/feescollection/getReport" + "?p_selectedOption=" + selectedOption.Key + "&p_feesAcademicClassId=" + feesAcademicClassId).then(function (result) {
            return result.data;
        });
    }
    function getStuFeesLedger(selectedOption, feesAcademicClassId) {
        return $http.get(serviceBase + "api/feescollection/getStuFeesLedger" + "?p_selectedOption=" + selectedOption.Key + "&p_feesAcademicClassId=" + feesAcademicClassId).then(function (result) {
            return result.data;
        });
    }
 
    return {
        getstudent: getstudent,
        save: save,
        getreport: getreport
    }
});