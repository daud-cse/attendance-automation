/*Start sms template*/
var techerAttendance = {
    url: '/techerAttendance',
    template: '<div ui-view class="fade-in-up"></div>'
};

var techerAttendanceList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/techerAttendance/list.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/techerAttendance/list.js',
                                   urlPrefix + 'js/services/techerAttendance/techerAttendanceService.js'
                     ]);
                 }
             );

          }]
    }

};

var techerAttendanceCreate = {
    url: '/create',
    templateUrl: urlPrefix + 'tpl/techerAttendance/create.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/techerAttendance/create.js',
                                   urlPrefix + 'js/services/techerAttendance/techerAttendanceService.js'
                     ]);
                 }
             );

          }]
    }

};

var techerAttendanceEdit = {
    url: '/edit/{teacherattendanceId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/techerAttendance/edit.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/techerAttendance/edit.js',
                                   urlPrefix + 'js/services/techerAttendance/techerAttendanceService.js'
                     ]);
                 }
             );

          }]
    }

};

var techerAttendanceDetails = {
    url: '/details/{teacherattendanceId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/techerAttendance/details.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/techerAttendance/details.js',
                                   urlPrefix + 'js/services/techerAttendance/techerAttendanceService.js'
                     ]);
                 }
             );

          }]
    }

};
