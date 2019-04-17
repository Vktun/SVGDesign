using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGDesignService.common
{
    public static class JsonFile
    {
        public static T JsonToEntities<T>(string jsonPath)
        {
            string jsonText = "";
            using (FileStream fs = new FileStream(jsonPath, FileMode.Open))
            {
                byte[] barray = new byte[fs.Length];
                fs.Read(barray, 0, barray.Length);
                fs.Close();
                jsonText = Encoding.UTF8.GetString(barray);
            }
            try
            {
                jsonText = jsonText.Substring(1, jsonText.Length - 1);
                var temp = JsonConvert.DeserializeObject<T>(jsonText);
                return temp;
            }
            catch (Exception ex)
            {
                var logErro = jsonPath.Substring(0, jsonPath.LastIndexOf("\\")) + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
                FileStream fst = new FileStream(logErro, FileMode.Create);

                byte[] byteArray = new byte[ex.Message.Length];
                byteArray = Encoding.UTF8.GetBytes(ex.Message);
                fst.WriteAsync(byteArray, 2, byteArray.Length);

                return default(T);
            }
        }
    }
}
