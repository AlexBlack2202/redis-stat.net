// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="">
//   
// </copyright>
//  <summary>
//   BundleConfig.cs
// </summary>
// <author>amd989</author>
// --------------------------------------------------------------------------------------------------------------------
namespace redis_stat.net
{
    using System.Web.Optimization;

    /// <summary>The bundle config.</summary>
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        #region Public Methods and Operators

        /// <summary>The register bundles.</summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            
            bundles.Add(new ScriptBundle("~/bundles/site").Include("~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include("~/Scripts/jquery.signalR-{version}.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/jqplot").Include(
                    "~/Scripts/jqPlot/jquery.jqplot.min.js", 
                    "~/Scripts/jqPlot/plugins/jqplot.canvasTextRenderer.min.js", 
                    "~/Scripts/jqPlot/plugins/jqplot.canvasAxisLabelRenderer.min.js", 
                    "~/Scripts/jqPlot/plugins/jqplot.canvasAxisTickRenderer.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include("~/Scripts/select2.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css"));
            
            bundles.Add(new StyleBundle("~/Content/jqplot").Include("~/Scripts/jqPlot/jquery.jqplot.css"));
            
            bundles.Add(new StyleBundle("~/Content/select2").Include("~/Content/select2-bootstrap.css"));
        }

        #endregion
    }
}