using Svg;
using SVGDesignService;
using SVGDesignService.common;
using SVGDesignService.model;
using System;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Xml;

namespace SVGDesign.Controllers
{
    public class BaiduMapController : Controller
    {
        private BaiduMapService baidumapService;
        public BaiduMapController()
        {
            baidumapService = new BaiduMapService();
        }
        // GET: BaiduMap
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetLEDs()
        {
            LEDModel model = new LEDModel();

            var result = HttpRuntime.Cache.Get("leds_file");
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\files\LEDInfo.json");
            if (result == null)
            {
                result = model = JsonFile.JsonToEntities<LEDModel>(path);
                CacheDependency fileDep = new CacheDependency(path);
                HttpRuntime.Cache.Insert("leds_file", model, fileDep, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取路灯情况
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTrafficLines(TrafficAround trafficAround)
        {
            trafficAround.Road_grade = 0;
            var traffic = baidumapService.GetTrafficAroundUrl(trafficAround);
            return Json(traffic, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveTemplate(int ledId,string contents)
        {
            if (!string.IsNullOrEmpty(contents))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(contents);
                //var svgDoc = SvgDocument.Open(doc);
                doc.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\template\"+ ledId + ".xml"));
                var svgDoc = SvgDocument.Open(doc);
                svgDoc.Draw().Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\template\"+ ledId + ".bmp"));
            }
            return Json("");
        }
    }
}