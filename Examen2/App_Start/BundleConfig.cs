using System.Web;
using System.Web.Optimization;

namespace Examen2
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862


        public static void RegisterBundles(BundleCollection bundles)
        {
            string angularPath = "~/Scripts/AngularJs/Angular/";
            string angularPathApp = "~/Scripts/AngularJs/App/";

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


            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include(angularPath + "angular.js")
                .Include(angularPath + "angular-route.min.js")
                .Include(angularPath + "dirPagination.js")
                .Include(angularPath + "Chart.min.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/angularApp")
                .Include(angularPathApp + "Module.js")
                .Include(angularPathApp + "Estudiante.js")

                );

        }
    }
}
