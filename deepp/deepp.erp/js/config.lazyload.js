// lazyload config

angular.module('app')
    /**
   * jQuery plugin config use ui-jq directive , config the js and css files that required
   * key: function name of the jQuery plugin
   * value: array of the css js file located
   */
  .constant('JQ_CONFIG', {
      easyPieChart: [urlPrefix + 'vendor/jquery/charts/easypiechart/jquery.easy-pie-chart.js'],
      sparkline: [urlPrefix + 'vendor/jquery/charts/sparkline/jquery.sparkline.min.js'],
      plot: [urlPrefix + 'vendor/jquery/charts/flot/jquery.flot.min.js',
                          urlPrefix + 'vendor/jquery/charts/flot/jquery.flot.resize.js',
                          urlPrefix + 'vendor/jquery/charts/flot/jquery.flot.tooltip.min.js',
                          urlPrefix + 'vendor/jquery/charts/flot/jquery.flot.spline.js',
                          urlPrefix + 'vendor/jquery/charts/flot/jquery.flot.orderBars.js',
                          urlPrefix + 'vendor/jquery/charts/flot/jquery.flot.pie.min.js'],
      slimScroll: [urlPrefix + 'vendor/jquery/slimscroll/jquery.slimscroll.min.js'],
      sortable: [urlPrefix + 'vendor/jquery/sortable/jquery.sortable.js'],
      nestable: [urlPrefix + 'vendor/jquery/nestable/jquery.nestable.js',
                          urlPrefix + 'vendor/jquery/nestable/nestable.css'],
      filestyle: [urlPrefix + 'vendor/jquery/file/bootstrap-filestyle.min.js'],
      slider: [urlPrefix + 'vendor/jquery/slider/bootstrap-slider.js',
                          urlPrefix + 'vendor/jquery/slider/slider.css'],
      chosen: [urlPrefix + 'vendor/jquery/chosen/chosen.jquery.min.js',
                          urlPrefix + 'vendor/jquery/chosen/chosen.css'],
      TouchSpin: [urlPrefix + 'vendor/jquery/spinner/jquery.bootstrap-touchspin.min.js',
                          urlPrefix + 'vendor/jquery/spinner/jquery.bootstrap-touchspin.css'],
      wysiwyg: [urlPrefix + 'vendor/jquery/wysiwyg/bootstrap-wysiwyg.js',
                          urlPrefix + 'vendor/jquery/wysiwyg/jquery.hotkeys.js'],
      dataTable: [urlPrefix + 'vendor/jquery/datatables/jquery.dataTables.min.js',
                          urlPrefix + 'vendor/jquery/datatables/dataTables.bootstrap.js',
                          urlPrefix + 'vendor/jquery/datatables/dataTables.bootstrap.css'],
      vectorMap: [urlPrefix + 'vendor/jquery/jvectormap/jquery-jvectormap.min.js',
                          urlPrefix + 'vendor/jquery/jvectormap/jquery-jvectormap-world-mill-en.js',
                         urlPrefix + 'vendor/jquery/jvectormap/jquery-jvectormap-us-aea-en.js',
                          urlPrefix + 'vendor/jquery/jvectormap/jquery-jvectormap.css'],
      footable: [urlPrefix + 'vendor/jquery/footable/footable.all.min.js',
                          urlPrefix + 'vendor/jquery/footable/footable.core.css']
      }
  )
  // oclazyload config
  .config(['$ocLazyLoadProvider', function($ocLazyLoadProvider) {
      // We configure ocLazyLoad to use the lib script.js as the async loader
      $ocLazyLoadProvider.config({
          debug:  false,
          events: true,
          modules: [
              {
                  name: 'ngGrid',
                  files: [
                      urlPrefix + 'vendor/modules/ng-grid/ng-grid.min.js',
                      urlPrefix + 'vendor/modules/ng-grid/ng-grid.min.css',
                      urlPrefix + 'vendor/modules/ng-grid/theme.css'
                  ]
              },
              {
                  name: 'ui.select',
                  files: [
                      urlPrefix + 'vendor/modules/angular-ui-select/select.min.js',
                      urlPrefix + 'vendor/modules/angular-ui-select/select.min.css'
                  ]
              },
              {
                  name:'angularFileUpload',
                  files: [
                    urlPrefix + 'vendor/modules/angular-file-upload/angular-file-upload.js'
                  ]
              },
              {
                  name:'ui.calendar',
                  files: [urlPrefix + 'vendor/modules/angular-ui-calendar/calendar.js']
              },
              {
                  name: 'ngImgCrop',
                  files: [
                      urlPrefix + 'vendor/modules/ngImgCrop/ng-img-crop.js',
                      urlPrefix + 'vendor/modules/ngImgCrop/ng-img-crop.css'
                  ]
              },
              {
                  name: 'angularBootstrapNavTree',
                  files: [
                      urlPrefix + 'vendor/modules/angular-bootstrap-nav-tree/abn_tree_directive.js',
                      urlPrefix + 'vendor/modules/angular-bootstrap-nav-tree/abn_tree.css'
                  ]
              },
              {
                  name: 'toaster',
                  files: [
                      urlPrefix + 'vendor/modules/angularjs-toaster/toaster.js',
                      urlPrefix + 'vendor/modules/angularjs-toaster/toaster.css'
                  ]
              },
              {
                  name: 'textAngular',
                  files: [
                      urlPrefix + 'vendor/modules/textAngular/textAngular-sanitize.min.js',
                      urlPrefix + 'vendor/modules/textAngular/textAngular.min.js'
                  ]
              },
              {
                  name: 'vr.directives.slider',
                  files: [
                      urlPrefix + 'vendor/modules/angular-slider/angular-slider.min.js',
                      urlPrefix + 'vendor/modules/angular-slider/angular-slider.css'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular',
                  files: [
                      urlPrefix + 'vendor/modules/videogular/videogular.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.controls',
                  files: [
                      urlPrefix + 'vendor/modules/videogular/plugins/controls.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.buffering',
                  files: [
                      urlPrefix + 'vendor/modules/videogular/plugins/buffering.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.overlayplay',
                  files: [
                      urlPrefix + 'vendor/modules/videogular/plugins/overlay-play.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.poster',
                  files: [
                      urlPrefix + 'vendor/modules/videogular/plugins/poster.min.js'
                  ]
              },
              {
                  name: 'com.2fdevs.videogular.plugins.imaads',
                  files: [
                      urlPrefix + 'vendor/modules/videogular/plugins/ima-ads.min.js'
                  ]
              },
              {
                  name: 'angucomplete-alt',
                  files: [
                      urlPrefix + 'vendor/angular/angucomplete-alt.min.js'
                  ]
              },
              
          ]
      });
  }])
;