using System;
using System.Collections.Generic;
using System.Net;
using System.Data;
using System.Web.Script.Serialization;
using System.IO;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace ConectaLoja.Utils
{
    public class Json
    {
        private static string DownloadString(string url)
        {
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                return client.DownloadString(url);
            }
        }

        public static JObject LoadJsonYouTube(string url)
        {
            string pageSource = DownloadString(url);

            var dataRegex = new Regex(@"ytplayer\.config\s*=\s*(\{.+?\});", RegexOptions.Multiline);

            string extractedJson = dataRegex.Match(pageSource).Result("$1");

            return JObject.Parse(extractedJson);
        }

        public static string GetJSONString(string url, NameValueCollection parametros)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] responseArray = client.UploadValues(url, parametros);
                    string sret = System.Text.Encoding.ASCII.GetString(responseArray);
                    //JObject result = JObject.Parse(sret);
                    return sret;
                }
                catch (WebException ex)
                {
                    string responseText;

                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        responseText = reader.ReadToEnd();
                    }

                    return responseText;
                }
            }
        }

        public static string GetJSONStrings(string url, NameValueCollection parametros)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] responseArray = client.UploadValues(url, parametros);
                    string sret = System.Text.Encoding.ASCII.GetString(responseArray);
                    //JObject result = JObject.Parse(sret);
                    return sret;
                }
                catch (WebException ex)
                {
                    return ex.Message;
                }
            }
        }

        public static string GetJSONString(string url, NameValueCollection parametros, bool MetodoGet)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string get = "";
                    foreach (string key in parametros)
                    {
                        var value = parametros[key];
                        get += key + "=" + value + "&";
                    }

                    get = get.Substring(0, get.Length - 1);
                    url = url + "?" + get;

                    string sret = client.DownloadString(url);
                    return sret;
                }
                catch (WebException ex)
                {
                    return ex.Message;
                }
            }
        }

        public static string GetJSONFile(string strCaminho)
        {
            if (File.Exists(strCaminho))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(strCaminho))
                    {
                        return sr.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return " O arquivo " + strCaminho + "não foi localizado !";
            }
        }

        public static string ConvertDataTabletoString(DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 500000000;
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
    }
}
