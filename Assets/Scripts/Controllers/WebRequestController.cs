using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public abstract class WebRequestController
{
    private string _areaPath = String.Empty;

    public WebRequestController()
    {
        //_rootPath = AppSession.Current.ApiServerPath;
    }

    public WebRequestController(string apiAreaName = "")
    {
        _areaPath = apiAreaName;
    }

    public string GetPath()
    {
        var path = AppSession.Current.ApiServerPath;
        return (String.IsNullOrWhiteSpace(path) ? "http://localhost:5000" : path) + $"/{_areaPath}/";
    }

    public async Task<HttpResponseMessage> Post(string uri)
    {
        return await Post(uri, null);
    }

    public async Task<HttpResponseMessage> Post(string uri, string content)
    {
        try
        {
            var path = GetPath();
            Debug.Log($"[HttpPost]{path}{uri}{(content != null ? (" With content: " + Environment.NewLine) : (String.Empty))}{content}");

            var requestUrl = $"{path}{uri}";
            HttpClient client = new HttpClient();
            content = String.IsNullOrWhiteSpace(content) ? string.Empty : content;
            var response = await client.PostAsync(requestUrl, new StringContent(content));
            return response;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }

    public async Task PostAsync(string uri, object content)
    {
        try
        {
            var path = GetPath();
            Debug.Log($"[HttpPost]{path}{uri}{(content != null ? (" With content: " + Environment.NewLine) : (String.Empty))}{content}");

            var requestUrl = $"{path}{uri}";
            HttpClient client = new HttpClient();
            await PostStreamAsync(requestUrl, HttpMethod.Post, content);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }

    public async Task<T> PostAsync<T>(string uri, T content)
    {
        try
        {
            var path = GetPath();
            Debug.Log($"[HttpPost]{path}{uri}{(content != null ? (" With content: " + Environment.NewLine) : (String.Empty))}{content}");

            var requestUrl = $"{path}{uri}";
            HttpClient client = new HttpClient();
            return await PostStreamAsync(requestUrl, HttpMethod.Post, content);
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
        using (var httpContent = CreateHttpContent(content))
        {
            request.Content = httpContent;

            using (var response = await client
                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
            }
        }
    }

    private static async Task<T> PostStreamAsync<T>(string requestUrl, HttpMethod httpMethod, T content)
    {
        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage(httpMethod, requestUrl))
        using (var httpContent = CreateHttpContent(content))
        {
            request.Content = httpContent;

            using (var response = await client
                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                return await response.GetData<T>();
            }
        }
    }

    //private static async Task PostStreamAsync(object content, CancellationToken cancellationToken)
    //{
    //    using (var client = new HttpClient())
    //    using (var request = new HttpRequestMessage(HttpMethod.Post, Url))
    //    using (var httpContent = CreateHttpContent(content))
    //    {
    //        request.Content = httpContent;

    //        using (var response = await client
    //            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
    //            .ConfigureAwait(false))
    //        {
    //            response.EnsureSuccessStatusCode();
    //        }
    //    }
    //}

    public async Task<HttpResponseMessage> Put(string uri)
    {
        return await Put(uri, null);
    }

    public async Task<HttpResponseMessage> Put(string uri, HttpContent content)
    {
        try
        {
            var path = GetPath();
            Debug.Log($"[HttpPut]{path}{uri}");

            var requestUrl = $"{path}{uri}";
            HttpClient client = new HttpClient();
            var response = await client.PutAsync(requestUrl, content ?? new StringContent(""));
            return response;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }

    public async Task<HttpResponseMessage> Get(string uri)
    {
        try
        {
            var path = GetPath();
            Debug.Log($"[HttpGet]{path}{uri}");

            var requestUrl = $"{path}{uri}";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(requestUrl);
            return response;

        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
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

    private static HttpContent CreateHttpContent(object content)
    {
        HttpContent httpContent = null;

        if (content != null)
        {
            var ms = new MemoryStream();
            SerializeJsonIntoStream(content, ms);
            ms.Seek(0, SeekOrigin.Begin);
            httpContent = new StreamContent(ms);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        return httpContent;
    }
}

public static class ExtensionMethods
{
    public static async Task<T> GetData<T>(this HttpResponseMessage response)
    {
        try
        {
            Debug.Log($"Getting Data for {typeof(T)} from {response.Content}");

            var json = await response.Content.ReadAsStringAsync();
            Debug.Log($"from http response {response.RequestMessage.RequestUri} {Environment.NewLine} {response.StatusCode} {Environment.NewLine} result: {Environment.NewLine}{json}");
            if(!String.IsNullOrWhiteSpace(json))
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            return default;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }
}
