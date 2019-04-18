using System.Web;
using System.Web.Optimization;

namespace SVGDesign
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui/jquery-ui.js"));
            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/svg").Include(
                "~/Scripts/svg/svg.js",
                "~/Scripts/svg/svg.draggable.js",
                "~/Script/svg/svg.panzoom.js",
                "~/Scripts/svg/svg.select.js",
                "~/Scripts/svg/svg.resize.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap.css",
                      "~/Content/svg/svg.select.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/content/jquerycss").Include(
                      "~/Scripts/jquery-ui/jquery-ui.css"));


        }
    }
}
