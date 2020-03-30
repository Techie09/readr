using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Readr.Assets.Scripts.Controllers
{
    [System.Serializable]
    public class TagData
    {
        public string name;
        public float confidence;
    }

    [System.Serializable]
    public class AnalysedObject
    {
        public TagData[] tags;
        public string requestId;
        public object metadata;
    }

    public class AzureController
    {
        public async Task<T> PostVisionAnalysisAsync<T>(string filePath)
        {
            try
            {
                var requestUrl = $"{AppSession.Current.ApiServerPath}/api/azure/addscan/" + $"{AppSession.Current.UserSession.Id}";
                return await PostStreamAsync<T>(requestUrl, HttpMethod.Post, filePath);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="https://johnthiriet.com/efficient-post-calls/"/>
        /// <param name="requestUrl"></param>
        /// <param name="httpMethod"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private static async Task PostStreamAsync(string requestUrl, HttpMethod httpMethod, object content)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(httpMethod, requestUrl))
            using (var httpContent = CreateJsonHttpContent(content, HttpHeaderMediaType.Application_JSON_Type))
            {
                request.Content = httpContent;

                using (var response = await client
                    .SendAsync(request)
                    .ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        private static async Task<T> PostStreamAsync<T>(string requestUrl, HttpMethod httpMethod, string filePath)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(httpMethod, requestUrl))
            using (var httpContent = CreateImageHttpContent(filePath, HttpHeaderMediaType.Application_Octect_Stream_Type))
            {
                request.Content = httpContent;

                    var response = client.SendAsync(request).Result;
                    //response.EnsureSuccessStatusCode();
                    return await response.GetData<T>();
            }
        }

        private static HttpContent CreateJsonHttpContent(object content, MediaTypeHeaderValue mediaTypeHeaderValue)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                if (mediaTypeHeaderValue.MediaType == HttpHeaderMediaType.Application_JSON)
                {
                    using (var ms = new MemoryStream())
                    {
                        SerializeJsonIntoStream(content, ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        httpContent = new StreamContent(ms);
                    }
                }

                if (httpContent != null)
                {
                    //httpContent.Headers.ContentType = mediaTypeHeaderValue;
                    //httpContent.Headers.Add(ocpApimSubscriptionKeyHeader, apiKey);
                }
            }

            return httpContent;
        }

        private static HttpContent CreateImageHttpContent(string filePath, MediaTypeHeaderValue mediaTypeHeaderValue)
        {
            HttpContent httpContent = null;

            if (!String.IsNullOrWhiteSpace(filePath))
            {
                if (mediaTypeHeaderValue.MediaType == HttpHeaderMediaType.Application_Octet_Stream)
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        BinaryReader binaryReader = new BinaryReader(fileStream);
                        var imageBytes = binaryReader.ReadBytes((int)fileStream.Length);
                        httpContent = new ByteArrayContent(imageBytes);
                    }
                }

                if (httpContent != null)
                {
                   // httpContent.Headers.ContentType = mediaTypeHeaderValue;
                    //httpContent.Headers.Add(ocpApimSubscriptionKeyHeader, apiKey);
                }
            }

            return httpContent;
        }

        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }
    }

    public class HttpHeaderMediaType
    {
        public static string Application_Atom_XML => "application/atom+xml";
        public static string Application_Form_UrlEncoded => "application/x-www-form-urlencoded";
        public static string Application_JSON => "application/json";
        public static string Application_Octet_Stream => "application/octet-stream";
        public static string Application_SVG_XML => "application/svg+xml";
        public static string Application_XHTML_XML => "application/xhtml+xml";
        public static string Application_XML => "application/xml";
        public static string Multipart_Form_Data => "multipart/form-data";
        public static string Text_Html => "text/html";
        public static string Text_Plain => "text/plain";
        public static string Text_XML => "text/xml";
        public static string Wildcard => "*/*";

        /// <summary>
        /// For use when sending JSON formatted content
        /// </summary>
        public static MediaTypeHeaderValue Application_JSON_Type => new MediaTypeHeaderValue(Application_JSON);
        /// <summary>
        /// For use when sending byte data content (i.e. an image)
        /// </summary>
        public static MediaTypeHeaderValue Application_Octect_Stream_Type => new MediaTypeHeaderValue(Application_Octet_Stream);


    }
}
