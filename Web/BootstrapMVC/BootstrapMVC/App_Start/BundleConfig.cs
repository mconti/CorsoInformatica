using System.Web;
using System.Web.Optimization;

namespace BootstrapMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Registrazione dei bundle Janux

            // Scripts
            bundles.Add(new ScriptBundle("~/JanuxBundles/jquery").Include(
                "~/TemiBootStrap/janux/js/jquery-1.9.1.min.js",
                "~/TemiBootStrap/janux/js/jquery-migrate-1.0.0.min.js",
                "~/TemiBootStrap/janux/js/jquery-ui-1.10.0.custom.min.js",
                "~/TemiBootStrap/janux/js/jquery.ui.touch-punch.js",
                "~/TemiBootStrap/janux/js/modernizr.js",
                "~/TemiBootStrap/janux/js/bootstrap.min.js",
                "~/TemiBootStrap/janux/js/jquery.cookie.js",
                "~/TemiBootStrap/janux/js/fullcalendar.min.js",
                "~/TemiBootStrap/janux/js/jquery.dataTables.min.js",
                "~/TemiBootStrap/janux/js/excanvas.js",
                "~/TemiBootStrap/janux/js/jquery.flot.js",
                "~/TemiBootStrap/janux/js/jquery.flot.pie.js",
                "~/TemiBootStrap/janux/js/jquery.flot.stack.js",
                "~/TemiBootStrap/janux/js/jquery.flot.resize.min.js",
                "~/TemiBootStrap/janux/js/jquery.chosen.min.js",
                "~/TemiBootStrap/janux/js/jquery.uniform.min.js",
                "~/TemiBootStrap/janux/js/jquery.cleditor.min.js",
                "~/TemiBootStrap/janux/js/jquery.noty.js",
                "~/TemiBootStrap/janux/js/jquery.elfinder.min.js",
                "~/TemiBootStrap/janux/js/jquery.raty.min.js",
                "~/TemiBootStrap/janux/js/jquery.iphone.toggle.js",
                "~/TemiBootStrap/janux/js/jquery.uploadify-3.1.min.js",
                "~/TemiBootStrap/janux/js/jquery.gritter.min.js",
                "~/TemiBootStrap/janux/js/jquery.imagesloaded.js",
                "~/TemiBootStrap/janux/js/jquery.masonry.min.js",
                "~/TemiBootStrap/janux/js/jquery.knob.modified.js",
                "~/TemiBootStrap/janux/js/jquery.sparkline.min.js",
                "~/TemiBootStrap/janux/js/counter.js",
                "~/TemiBootStrap/janux/js/retina.js",
                "~/TemiBootStrap/janux/js/custom.js"
            ));

            // Styles
            bundles.Add(new StyleBundle("~/JanuxBundles/css").Include(
                "~/TemiBootStrap/janux/css/bootstrap.min.css",
                "~/TemiBootStrap/janux/css/bootstrap-responsive.min.css",
                "~/TemiBootStrap/janux/css/style.css",
                "~/TemiBootStrap/janux/css/style-responsive.css"
                //"http://fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800&subset=latin,cyrillic-ext,latin-ext"
            ));

            // Registrazione dei bundle Ciprass

            // Scripts
            bundles.Add(new ScriptBundle("~/CiprassBundles/jquery").Include(
                "~/TemiBootStrap/videopage/js/jquery-1.9.1.min.js",
                "~/TemiBootStrap/videopage/js/jquery.js",
                "~/TemiBootStrap/videopage/js/bootstrap.min.js",
                "~/TemiBootStrap/videopage/js/owl.carousel.min.js",
                "~/TemiBootStrap/videopage/js/jquery.isotope.js",
                "~/TemiBootStrap/videopage/js/jquery.prettyPhoto.js",
                "~/TemiBootStrap/videopage/js/smooth-scroll.js",
                "~/TemiBootStrap/videopage/js/jquery.fancybox.pack.js?v=2.1.5",
                "~/TemiBootStrap/videopage/js/jquery.counterup.min.js",
                "~/TemiBootStrap/videopage/js/waypoints.min.js",
                "~/TemiBootStrap/videopage/js/jquery.bxslider.min.js",
                "~/TemiBootStrap/videopage/js/jquery.scrollTo.js",
                "~/TemiBootStrap/videopage/js/jquery.easing.1.3.js",
                "~/TemiBootStrap/videopage/js/jquery.singlePageNav.js",
                "~/TemiBootStrap/videopage/js/wow.min.js",
                "~/TemiBootStrap/videopage/js/gmaps.js",
                "~/TemiBootStrap/videopage/js/custom.js"
             ));

            // Styles
            bundles.Add(new StyleBundle("~/CiprassBundles/css").Include(
                "~/TemiBootStrap/videopage/css/bootstrap.min.css",
                "~/TemiBootStrap/videopage/css/font-awesome.min.css",
                "~/TemiBootStrap/videopage/css/animate.css",
                "~/TemiBootStrap/videopage/css/owl.carousel.css",
                "~/TemiBootStrap/videopage/css/owl.theme.css",
                "~/TemiBootStrap/videopage/css/prettyPhoto.css",
                "~/TemiBootStrap/videopage/css/flexslider.css",
                "~/TemiBootStrap/videopage/css/red.css",
                "~/TemiBootStrap/videopage/css/custom.css",
                "~/TemiBootStrap/videopage/css/responsive.css"
             ));
        }
    }
}
