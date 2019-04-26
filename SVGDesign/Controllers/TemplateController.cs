using System.IO;
using System.Web.Mvc;
using System.Xml;

namespace SVGDesign.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取组件列表
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public ContentResult GetProject(string projectName)
        {
            var svgContent = "";
            var rootPath = Server.MapPath("~/projects/");
            if (Directory.Exists(rootPath + projectName))
            {
                var filePath = rootPath + projectName + "/" + projectName + ".svg";
                var fileExist = System.IO.File.Exists(filePath);
                if (fileExist)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filePath);
                    var nodes = doc.ChildNodes;
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Name.ToLower() == "svg")
                        {
                            svgContent = node.InnerXml;
                        }
                    }
                }
            }

            return Content(svgContent);
        }
    }
}