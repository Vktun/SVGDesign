using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace SVGDesign.Controllers
{
    public class ComponentsController : Controller
    {
        // GET: Components
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取组件列表
        /// </summary>
        /// <param name="parentName"></param>
        /// <returns></returns>
        public JsonResult GetComponents(string parentName)
        {
            List<object> paths = new List<object>();
            var rootPath = Server.MapPath("~/Component/");
            if (Directory.Exists(rootPath + parentName))
            {
                var files = Directory.GetFiles(rootPath + parentName);
                foreach (var item in files)
                {
                    var url = item.Substring(item.IndexOf("\\Component\\" + parentName));
                    var le = url.Length;
                    var name = url.Substring(url.LastIndexOf("\\")+1, url.LastIndexOf(".")- url.LastIndexOf("\\")-1);
                   
                    paths.Add(new { url = url, name = name });
                }
            }

            return Json(paths, JsonRequestBehavior.AllowGet);
        }

    }
}