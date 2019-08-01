/*Start template*/
var Exam = {
    url: '/Exam',
    template: '<div ui-view class="fade-in-up"></div>'
};

var ExamList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/Exam/list.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/Exam/list.js',
                                   urlPrefix + 'js/services/Exam/ExamService.js'
                     ]);
                 }
             );

          }]
    }

};

var ExamCreate = {
    url: '/create',
    templateUrl: urlPrefix + 'tpl/Exam/create.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/Exam/create.js',
                                   urlPrefix + 'js/services/Exam/ExamService.js'
                     ]);
                 }
             );

          }]
    }

};

var ExamEdit = {
    url: '/edit/{ExamId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/Exam/edit.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/Exam/edit.js',
                                   urlPrefix + 'js/services/Exam/ExamService.js'
                     ]);
                 }
             );

          }]
    }

};


var ExamDetails = {
    url: '/details/{ExamId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/Exam/details.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/Exam/details.js',
                                   urlPrefix + 'js/services/Exam/ExamService.js'
                     ]);
                 }
             );

          }]
    }

};

