﻿using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;
using System.Web.Optimization;

namespace pnsms.landingTest
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

            var cssBundle = new StyleBundle("~/bundles/css");
            cssBundle.Include("~/assets/less/styles.less"
                , "~/assets/less/Site.less");
            cssBundle.Transforms.Add(cssTransformer);
            cssBundle.Orderer = nullOrderer;
            bundles.Add(cssBundle);

            var jqueryBundle = new ScriptBundle("~/bundles/jquery");
            jqueryBundle.Include("~/Scripts/jquery-{version}.js");
            jqueryBundle.Transforms.Add(jsTransformer);
            jqueryBundle.Orderer = nullOrderer;
            bundles.Add(jqueryBundle);

            //var jqueryvalBundle = new ScriptBundle("~/bundles/jqueryval");
            //jqueryvalBundle.Include("~/assets/js/jquery.validate*");
            //jqueryvalBundle.Transforms.Add(jsTransformer);
            //jqueryvalBundle.Orderer = nullOrderer;
            //bundles.Add(jqueryvalBundle);

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            var modernizrBundle = new ScriptBundle("~/bundles/modernizr");
            modernizrBundle.Include("~/assets/js/modernizr-*");
            modernizrBundle.Transforms.Add(jsTransformer);
            modernizrBundle.Orderer = nullOrderer;
            bundles.Add(modernizrBundle);

            var bootstrapBundle = new ScriptBundle("~/bundles/bootstrap");
            //bootstrapBundle.Include("~/assets/js/bootstrap.min.js", "~/assets/js/respond.js");
            bootstrapBundle.Include("~/Scripts/bootstrap.js"
                , "~/assets/js/respond.js"
                , "~/assets/js/jquery.nicescroll.min.js"
                , "~/assets/js/menu.js"
                );
            bootstrapBundle.Transforms.Add(jsTransformer);
            bootstrapBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapBundle);

            BundleTable.EnableOptimizations = true;
        }
    }
}