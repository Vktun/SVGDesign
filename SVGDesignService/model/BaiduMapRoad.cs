namespace SVGDesignService.model
{
    public static class BaiduMapRoad
    {
        public static string AK
        {
            get
            {
                return "";
            }
        }

        public static string SK { get { return "XFSa4dk1SndC5oXFYBxOnUDkdwDFBzBf"; } }

        public static string Url
        {
            get { return "http://api.map.baidu.com/traffic/v1/around"; }
            //get { return "http://api.map.baidu.com/location/ip"; }
        }
        public static string Path
        {
            get { return "/traffic/v1/around"; }
            //get { return "/location/ip"; }
        }

    }
    public class TrafficAround
    {
        public string Center { get; set; }

        public int Radius { get; set; }

        public int Road_grade { get; set; }

        //public string Coord_type_input { get; set; } = "bd09ll";

        //public string Coord_type_output { get; set; } = "bd09ll";

        public string Sn { get; set; }
    }
}
