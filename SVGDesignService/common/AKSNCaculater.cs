using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SVGDesignService.common
{

    public class AKSNCaculater
    {
        private static string MD5(string password)
        {
            //byte[] textBytes = Encoding.UTF8.GetBytes(password);
            //try
            //{
            //    System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
            //    cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
            //    byte[] hash = cryptHandler.ComputeHash(textBytes);
            //    string ret = "";
            //    foreach (byte a in hash)
            //    {
            //        ret += a.ToString("x2");
            //    }
            //    return ret;
            //}
            //catch
            //{
            //    throw;
            //}
            try
            {
                System.Security.Cryptography.HashAlgorithm hash = System.Security.Cryptography.MD5.Create();
                byte[] hash_out = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                var md5_str = BitConverter.ToString(hash_out).Replace("-", "");
                return md5_str.ToLower();

            }
            catch
            {

                throw;
            }
        }

        private static string UrlEncode(string str)
        {
            str = HttpUtility.UrlEncode(str);
            byte[] buf = Encoding.ASCII.GetBytes(str);//等同于Encoding.ASCII.GetBytes(str)
            for (int i = 0; i < buf.Length; i++)
                if (buf[i] == '%')
                {
                    if (buf[i + 1] >= 'a') buf[i + 1] -= 32;
                    if (buf[i + 2] >= 'a') buf[i + 2] -= 32;
                    i += 2;
                }
            return Encoding.ASCII.GetString(buf);//同上，等同于Encoding.ASCII.GetString(buf)
        }

        public static string HttpBuildQuery(IDictionary<string, string> querystring_arrays)
        {

            StringBuilder sb = new StringBuilder();
            foreach (var item in querystring_arrays)
            {
                sb.Append(UrlEncode(item.Key));
                sb.Append("=");
                sb.Append(item.Value);//坑爹的百度，这个破sdk这地方 逗号 编码之后 traffic 路况的sn总是校验失败
                sb.Append("&");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string CaculateAKSN(string sk, string url, IDictionary<string, string> querystring_arrays)
        {
            var queryString = HttpBuildQuery(querystring_arrays);

            var str = UrlEncode(url + "?" + queryString + sk);

            return MD5(str);
        }
    }
}
