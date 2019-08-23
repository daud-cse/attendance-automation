/*Start template*/
var certificatePrint = {
    url: '/certificatePrint',
    template: '<div ui-view class="fade-in-up"></div>'
};

var certificatePrintList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/certificatePrint/list.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/certificatePrint/list.js',
                                   urlPrefix + 'js/services/certificatePrint/certificatePrintService.js'
                     ]);
                 }
             );

          }]
    }

};

var certificatePrintCreate = {
    url: '/create',
    templateUrl: urlPrefix + 'tpl/certificatePrint/create.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/certificatePrint/create.js',
                                   urlPrefix + 'js/services/certificatePrint/certificatePrintService.js'
                     ]);
                 }
             );

          }]
    }

};

var certificatePrintEdit = {
    url: '/edit/{certificateprintId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/certificatePrint/edit.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/certificatePrint/edit.js',
                                   urlPrefix + 'js/services/certificatePrint/certificatePrintService.js'
                     ]);
                 }
             );

          }]
    }

};

