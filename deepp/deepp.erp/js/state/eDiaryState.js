/*Start fees Collection template*/
var eDiary = {
    url: '/eDiary',
    template: '<div ui-view class="fade-in-up"></div>'
};

var eDiaryNew = {
    url: '/new',
    templateUrl: urlPrefix + 'tpl/eDiary/New.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/eDiary/New.js?v=1',
                                   urlPrefix + 'js/services/eDiary/eDiaryService.js'
                     ]);
                 }
             );

          }]
    }

};
var eDiaryList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/eDiary/List.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/eDiary/List.js',
                                  urlPrefix + 'js/services/eDiary/eDiaryService.js'
                     ]);
                 }
             );

          }]
    }

};