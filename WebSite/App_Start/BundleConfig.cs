using System.Web;
using System.Web.Optimization;

namespace WebSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                      "~/Scripts/validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/TextSpelling").Include(
                      "~/Scripts/TextSpelling.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/Game").Include(
                      "~/Scripts/Game/GuessWord.js",
                      "~/Scripts/Game/ScrambleWord.js"));

            bundles.Add(new ScriptBundle("~/bundles/resource").Include(
                      "~/Scripts/resource.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                      "~/Scripts/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/navbar.css",
                      "~/Content/main.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/jquery-ui").Include(
                      "~/Content/jquery-ui.css",
                      "~/Content/smoothness-jquery-ui.css",
                      "~/Content/jquery-ui.theme.css",
                      "~/Content/jquery-ui.structure.css"));
        }
    }
}
