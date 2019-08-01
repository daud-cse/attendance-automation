/*Start fees Generate Cnfig template*/

var feesGenerateConfig = {
    url: '/feesGenerateConfig',
    template: '<div ui-view class="fade-in-up"></div>'
};

var feesGenerateConfigNew = {
    url: '/feesGenerateConfigNew',
    templateUrl: urlPrefix + 'tpl/feesGenerateConfig/panel.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/feesGenerateConfig/panel.js',
                                   urlPrefix + 'js/services/feesGenerateConfig/feesGenerateConfigService.js'
                     ]);
                 }
             );

          }]
    }

};
var feesAutoGenerateNew = {
    url: '/new',
    templateUrl: urlPrefix + 'tpl/FeesAutoGenerate/new.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'ui.select']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/FeesAutoGenerate/new.js',
                                   urlPrefix + 'js/services/FeesAutoGenerate/feesAutoGenerateService.js'
                     ]);
                 }
             );

          }]
    }

};
