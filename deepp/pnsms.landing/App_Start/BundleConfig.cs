using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;
using System.Web.Optimization;

namespace pnsms.landing
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            var cssTransformer = new StyleTransformer();
            var jsTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            var cssblueBundle = new StyleBundle("~/bundles/css");
            cssblueBundle.Include("~/Content/css/bootstrap.min.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/animate.css",
                "~/Content/css/main.css",
                 "~/Content/Site.less"

                );
            cssblueBundle.Transforms.Add(cssTransformer);
            cssblueBundle.Orderer = nullOrderer;
            bundles.Add(cssblueBundle);

            var cssDatepickerBundle = new StyleBundle("~/bundles/datepicker");
            cssDatepickerBundle.Include("~/Content/bootstrap-datepicker3.css");
            //cssBundle.Transforms.Add(cssTransformer);
            cssDatepickerBundle.Orderer = nullOrderer;
            bundles.Add(cssDatepickerBundle);


            var jqueryBundle = new ScriptBundle("~/bundles/jquery");
            jqueryBundle.Include("~/Scripts/jquery-{version}.js"
                , "~/Scripts/modernizr-*");
            jqueryBundle.Transforms.Add(jsTransformer);
            jqueryBundle.Orderer = nullOrderer;
            bundles.Add(jqueryBundle);

            var jqueryvalBundle = new ScriptBundle("~/bundles/jqueryval");
            jqueryvalBundle.Include("~/Scripts/jquery.validate*");
            jqueryvalBundle.Transforms.Add(jsTransformer);
            jqueryvalBundle.Orderer = nullOrderer;
            bundles.Add(jqueryvalBundle);

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            /*
            var modernizrBundle = new ScriptBundle("~/bundles/modernizr");
            modernizrBundle.Include("~/Scripts/modernizr-*");
            modernizrBundle.Transforms.Add(jsTransformer);
            modernizrBundle.Orderer = nullOrderer;
            bundles.Add(modernizrBundle);*/

            var bootstrapBundle = new ScriptBundle("~/bundles/bootstrap");
            bootstrapBundle.Include("~/Scripts/bootstrap.js",
                "~/Scripts/plugins.min.js",
                "~/Scripts/custom.min.js"
                , "~/Scripts/jquery.nicescroll.min.js"
                );
            bootstrapBundle.Transforms.Add(jsTransformer);
            bootstrapBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapBundle);

            BundleTable.EnableOptimizations = true;

           
        }
    }
}