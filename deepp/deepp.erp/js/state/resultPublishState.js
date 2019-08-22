/*Start template*/
var resultPublish = {
    url: '/resultPublish',
    template: '<div ui-view class="fade-in-up"></div>'
};

var resultPublishList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/resultPublish/list.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/resultPublish/list.js',
                                   urlPrefix + 'js/services/resultPublish/resultPublishService.js'
                     ]);
                 }
             );

          }]
    }

};

var resultPublishCreate = {
    url: '/create',
    templateUrl: urlPrefix + 'tpl/resultPublish/create.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular', 'angularFileUpload']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/resultPublish/create.js',
                                   urlPrefix + 'js/services/resultPublish/resultPublishService.js'
                     ]);
                 }
             );

          }]
    }

};

var resultPublishEdit = {
    url: '/edit/{resultPublishId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/resultPublish/edit.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular', 'angularFileUpload']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/resultPublish/edit.js',
                                   urlPrefix + 'js/services/resultPublish/resultPublishService.js'
                     ]);
                 }
             );

          }]
    }

};

