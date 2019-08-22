/*Start sms template*/
var employeeAttendance = {
    url: '/employeeAttendance',
    template: '<div ui-view class="fade-in-up"></div>'
};

var employeeAttendanceList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/employeeAttendance/list.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/employeeAttendance/list.js',
                                   urlPrefix + 'js/services/employeeAttendance/employeeAttendanceService.js'
                     ]);
                 }
             );

          }]
    }

};

var employeeAttendanceCreate = {
    url: '/create',
    templateUrl: urlPrefix + 'tpl/employeeAttendance/create.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/employeeAttendance/create.js',
                                   urlPrefix + 'js/services/employeeAttendance/employeeAttendanceService.js'
                     ]);
                 }
             );

          }]
    }

};

var employeeAttendanceEdit = {
    url: '/edit/{employeeattendanceId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/employeeAttendance/edit.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/employeeAttendance/edit.js',
                                   urlPrefix + 'js/services/employeeAttendance/employeeAttendanceService.js'
                     ]);
                 }
             );

          }]
    }

};

var employeeAttendanceDetails = {
    url: '/details/{employeeattendanceId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/employeeAttendance/details.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/employeeAttendance/details.js',
                                   urlPrefix + 'js/services/employeeAttendance/employeeAttendanceService.js'
                     ]);
                 }
             );

          }]
    }

};
