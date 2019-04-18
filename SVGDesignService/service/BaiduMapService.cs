using SVGDesignService.common;
using SVGDesignService.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SVGDesignService
{
    public class BaiduMapService
    {
        public BaiduMapService()
        {
        }

        private string GetSN(TrafficAround trafficAround)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
           
            foreach (PropertyInfo pro in typeof(TrafficAround).GetProperties())
            {
                string value = pro.GetValue(trafficAround, null)?.ToString();
                string name = pro.Name.ToLower();
                if (string.IsNullOrEmpty(value)) continue;
                if (name == "sn") continue;
                keyValues.Add(name, value);
            }
            //keyValues.Add("ip", "39.91.109.120");
            keyValues.Add("ak", BaiduMapRoad.AK);

            return  AKSNCaculater.CaculateAKSN(BaiduMapRoad.SK, BaiduMapRoad.Path, keyValues);
        }

        public TrafficResult GetTrafficAroundUrl(TrafficAround trafficAround)
        {
            Dictionary<string, string> kv = new Dictionary<string, string>();
            //kv.Add("ip", "39.91.109.120");
            
            foreach (PropertyInfo pro in typeof(TrafficAround).GetProperties())
            {
                string value = pro.GetValue(trafficAround, null)?.ToString();
                string name = pro.Name.ToLower();
                if (string.IsNullOrEmpty(value)) continue;
                if (name == "sn") continue;
                kv.Add(name, value);
            }
            kv.Add("ak", BaiduMapRoad.AK);
            var sn = GetSN(trafficAround);
            string queryparams = kv.GetQueryString();
            var url = BaiduMapRoad.Url + "?" + queryparams+"&sn="+sn;

            TrafficResult res =  HttpMethods.HttpGet<TrafficResult>(url,Encoding.UTF8);
            return res;
        }
    }
}
