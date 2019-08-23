/*Start sms template*/
var contents = {
    url: '/contents',
    template: '<div ui-view class="fade-in-up"></div>'
};

var contentsList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/contents/list.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/contents/list.js',
                                   urlPrefix + 'js/services/contents/contentsService.js'
                     ]);
                 }
             );

          }]
    }

};

var contentsCreate = {
    url: '/create',
    templateUrl: urlPrefix + 'tpl/contents/create.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/contents/create.js',
                                   urlPrefix + 'js/services/contents/contentsService.js'
                     ]);
                 }
             );

          }]
    }

};

var contentsEdit = {
    url: '/edit/{contentsId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/contents/edit.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/contents/edit.js',
                                   urlPrefix + 'js/services/contents/contentsService.js'
                     ]);
                 }
             );

          }]
    }

};

var contentsDetails = {
    url: '/details/{contentsId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/contents/details.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/contents/details.js',
                                   urlPrefix + 'js/services/contents/contentsService.js'
                     ]);
                 }
             );

          }]
    }

};