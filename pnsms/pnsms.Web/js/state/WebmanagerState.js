/*Start sms template*/
// webmanager Settings
var webmanager = {
    abstract: true,
    url: '/webmanager',
    templateUrl: urlPrefix + 'tpl/settings/webmanager/webmanager.html',
    resolve: {
        deps: [
            'uiLoad',
            function(uiLoad) {
                return uiLoad.load([urlPrefix + 'js/app/settings/settings.js']);
            }
        ]
    }
};

var webmanagerWelcometext = {
    url: '/{infoText}',
    templateUrl: urlPrefix + 'tpl/settings/webmanager/welcometext.html',
    // use resolve to load other dependences

    resolve: {
        deps: [
            '$ocLazyLoad',
            function($ocLazyLoad) {
                return $ocLazyLoad.load(['toaster', 'textAngular', 'angularFileUpload']).then(
                    function() {
                        return $ocLazyLoad.load([
                            urlPrefix + 'js/controllers/settings/Webmanager/Webmanager.js',
                            urlPrefix + 'js/services/settings/InstituteService.js'
                        ]);
                    }
                );

            }
        ]
    }
};
 