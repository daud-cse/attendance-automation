/*Start fees Collection template feesSetup*/

var feesHeads = {
    url: '/feesHeads',
    templateUrl: urlPrefix + 'tpl/feesCollection/feesHeads.html',
    resolve: {
        deps: ['$ocLazyLoad',
            function ($ocLazyLoad) {
                return $ocLazyLoad.load('toaster').then(
                    function () {
                        return $ocLazyLoad.load([urlPrefix + 'js/controllers/feesCollection/feesHeads.js',
                            urlPrefix + 'js/services/feesCollection/feesHeadsService.js'
                        ]);
                    }
                );

            }]
    }
};
var feesType = {
    url: '/feesType',
    templateUrl: urlPrefix + 'tpl/feesCollection/FeesType.html',
    resolve: {
        deps: ['$ocLazyLoad',
            function ($ocLazyLoad) {
                return $ocLazyLoad.load('toaster').then(
                    function () {
                        return $ocLazyLoad.load([urlPrefix + 'js/controllers/feesCollection/feesType.js',
                            urlPrefix + 'js/services/feesCollection/feesTypeService.js'
                        ]);
                    }
                );

            }]
    }
};
var feesCollectionLedger = {
    url: '/ledger',
    templateUrl: urlPrefix + 'tpl/feesCollection/ledger.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'ui.select']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/feesCollection/ledger.js',
                                   urlPrefix + 'js/services/feesCollection/feesCollectionService.js'
                     ]);
                 }
             );

          }]
    }

};
var feesSetup = {
    url: '/feesSetup',
    templateUrl: urlPrefix + 'tpl/feesCollection/feesAcademicClass.html',
    resolve: {
        deps: ['$ocLazyLoad',
            function ($ocLazyLoad) {
                return $ocLazyLoad.load('toaster').then(
                    function () {
                        return $ocLazyLoad.load([urlPrefix + 'js/controllers/feesCollection/feesAcademicClass.js',
                            urlPrefix + 'js/services/feesCollection/feesAcademicClassService.js',
                            urlPrefix + 'js/services/feesCollection/feesTypeService.js'
                        ]);
                    }
                );

            }]
    }
};

var feesCollection = {
    url: '/feesCollection',
    template: '<div ui-view class="fade-in-up"></div>'
};

var feesGenerateNew = {
    url: '/feesGenerateNew',
    templateUrl: urlPrefix + 'tpl/feesCollection/feesGenerateNew.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/feesCollection/feesGenerateNew.js',
                                   urlPrefix + 'js/services/feesCollection/feesCollectionService.js'
                     ]);
                 }
             );

          }]
    }

};

var feesCollectionStudent = {
    url: '/feesCollectionStudent',
    templateUrl: urlPrefix + 'tpl/feesCollection/feesCollection.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/feesCollection/feesCollection.js',
                                   urlPrefix + 'js/services/feesCollection/feesCollectionService.js',
                                   urlPrefix + 'js/services/UserInfo/UserInfoService.js'
                     ]);
                 }
             );

          }]
    }

};