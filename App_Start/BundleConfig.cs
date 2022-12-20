using System.Web;
using System.Web.Optimization;

namespace ConsolidationSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(

                        "~/Scripts/angular.js",
                        "~/Scripts/angular-resource.min.js",
                         "~/Scripts/angular-animate.min.js",
                         "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                         "~/Scripts/ng-file-upload-shim.min.js",
                         "~/Scripts/ng-file-upload.min.js"));

            bundles.Add(new StyleBundle("~/Content/styles")
                  .Include("~/Content/jquery-ui-1.10.4.custom.min.css")
                  .Include("~/Content/font-awesome.min.css")
                  .Include("~/Content/bootstrap.min.css")
                  .Include("~/Content/animate.css")
                  .Include("~/Content/all.css")
                  .Include("~/Content/main.css")
                  .Include("~/Content/style-responsive.css")
                  .Include("~/Content/bootstrap-datepicker.min.css")
                  .Include("~/Content/iconoir.css")
                  );

            bundles.Add(new ScriptBundle("~/bundles/common")
                   .Include("~/Scripts/html5shiv.js")
                   .Include("~/Scripts/respond.min.js")
                   .Include("~/Scripts/icheck.min.js")
                   .Include("~/Scripts/holder.js")
                   .Include("~/Scripts/responsive-tabs.js")
                   .Include("~/Scripts/moment.min.js")
                   .Include("~/Scripts/jquery.table2excel.js")
                   .Include("~/Scripts/ xlsx.core.min.js")
                    .Include("~/Scripts/alasql.min.js")
                   
                   .Include("~/Scripts/main.js"));



        }
    }
}
