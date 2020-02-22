using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public abstract class WebRequestController
{
    private string _rootPath = String.Empty;

    public WebRequestController()
    {
        _rootPath = AppSession.Current.ApiServerPath;
    }

    public WebRequestController(string apiAreaName = "")
    {
        _rootPath = AppSession.Current.ApiServerPath + $"/{apiAreaName}";
    }

    public async Task<HttpResponseMessage> Post(string uri)
    {
        return await Post(uri, null);
    }

    public async Task<HttpResponseMessage> Post(string uri, HttpContent content)
    {
        try
        {
            Debug.Log($"Post to { _rootPath}{uri}");

            var requestUrl = $"{_rootPath}{uri}";
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(requestUrl, content ?? new StringContent(""));
            return response;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }
    }
    public async Task<HttpResponseMessage> Put(string uri)
    {
        return await Put(uri, null);
    }

    public async Task<HttpResponseMessage> Put(string uri, HttpContent content)
    {
        try
        {
            Debug.Log($"Put to {_rootPath}{uri}");

            var requestUrl = $"{_rootPath}{uri}";
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
            Debug.Log($"Put to {_rootPath}{uri}");

            var requestUrl = $"{_rootPath}{uri}";
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
}

public static class ExtensionMethods
{
    public static async Task<T> GetData<T>(this HttpResponseMessage response)
    {
        try
        {
            Debug.Log($"Getting Data for {typeof(T)} from {response.Content}");

            var json = await response.Content.ReadAsStringAsync();
            Debug.Log($"from http response {response.RequestMessage.RequestUri} and produced result: {Environment.NewLine}{json}");
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
