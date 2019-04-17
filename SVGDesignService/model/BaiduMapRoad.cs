using System.Collections.Generic;

namespace SVGDesignService.model
{
    public static class BaiduMapRoad
    {
        public static string AK
        {
            get
            {
                return "IGXqb5bsEELcffTCB4UGPb19fLBVmaqY";
            }
        }

        public static string SK { get { return "XFSa4dk1SndC5oXFYBxOnUDkdwDFBzBf"; } }

        public static string Url { get { return "http://api.map.baidu.com/traffic/v1/around"; } }

       
    }
    public class TrafficAround
    {
        public string Center { get; set; }

        public int Radius { get; set; }

        public int Road_grade { get; set; }

        public string coord_type_input { get; set; }

        public string Coord_type_output { get; set; }

        public string Sn { get; set; }
    }
}
