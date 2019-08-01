/*Start sms template*/
var onlineAdmission = {
    url: '/onlineAdmission',
    template: '<div ui-view class="fade-in-up"></div>'
};

var onlineAdmissionList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/onlineAdmission/list.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/onlineAdmission/list.js',
                                   urlPrefix + 'js/services/onlineAdmission/onlineAdmissionService.js'
                     ]);
                 }
             );

          }]
    }

};

var onlineAdmissionEdit = {
    url: '/edit/{onlineAdmissionId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/onlineAdmission/edit.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/onlineAdmission/edit.js',
                                   urlPrefix + 'js/services/onlineAdmission/onlineAdmissionService.js'
                     ]);
                 }
             );

          }]
    }

};
var onlineAdmissionDetails = {
    url: '/details/{onlineAdmissionId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/onlineAdmission/details.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/onlineAdmission/details.js',
                                   urlPrefix + 'js/services/onlineAdmission/onlineAdmissionService.js'
                     ]);
                 }
             );

          }]
    }
};


